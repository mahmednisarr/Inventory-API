using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using INV.Core;
using INV.Dto;
using INV.Dto.Auth;
using INV.Helper.helper;
using INV.Helper.JWT;
using INV.Services.Infrastructure.authentication;

namespace INV.Services.authentication
{
    public class AuthService : IAuthService
    {
        #region ClassMembers

        private readonly DataManager _dataManager;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly TResponse _response = new();
        private readonly List<ViewParam> _list = new();
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        public AuthService(IOptions<AppSettings> appSettings, IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = config;
            _appSettings = appSettings.Value;
            _dataManager = new DataManager(config, httpContextAccessor);
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Services

        public TResponse Index()
        {
            dynamic obj = new ExpandoObject();
            obj.CompHead = _appSettings.CompHead;
            obj.CompName = _appSettings.CompName;

            _response.ResponseCode = StatusCodes.Status200OK;
            _response.ResponseMessage = ResponseMessage.Success;
            _response.ResponseStatus = true;
            _response.ResponsePacket = obj;
            return _response;
        }

        public async Task<(AuthenticateResponse, int, string)> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            try
            {
                var constr = _configuration.GetConnectionString(model.CompName);
                var authenticateResponse = new AuthenticateResponse();
                var retVal = 0;
                var message = "";
                if (constr != null)
                {
                    const string query =
                        "SELECT [Usr_Kid],[Usr_Name],[Usr_Code],[Usr_Passkey],[Usr_Password],[Usr_mobNo],[Usr_Email],[Usr_SuccessLoginTime],[Usr_FailedAttempt],isnull( Usr_FailedAttemptdatetime, DATEADD(MINUTE,-30,GETDATE())) [Usr_FailedAttemptdatetime],[Usr_AuthKey],[Usr_SingleSession]  FROM [I_Usr] WHERE  [Usr_Status]=1 AND [Usr_Code]=@userCode";

                    _list.Add(new ViewParam() {Name = "userCode", Value = model.UserCode});
                    dynamic data = await _dataManager.ExecuteReaderWithQuery(query, _list, model.CompName);

                    if (data != null && data.Count > 0)
                    {
                        if (data[0].Count > 0)
                        {
                            var data1 = (IDictionary<string, object>) data[0][0];
                            var usrCode = data1["Usr_Code"].ToString();
                            var failedAttempt = Convert.ToInt32(data1["Usr_FailedAttempt"].ToString());
                            var failedTime = Convert.ToDateTime(data1["Usr_FailedAttemptdatetime"].ToString());
                            var id = data1["Usr_Kid"].ToString();
                            if (failedAttempt <= 3 || failedTime < DateTime.Now.AddMinutes(-15))
                            {
                                if (string.Equals(Cryptography.Encrypt(model.Password, data1["Usr_Passkey"].ToString()),
                                    data1["Usr_Password"].ToString()))
                                {
                                    var singleSession = Convert.ToBoolean(data1["Usr_SingleSession"].ToString());
                                    var authKey = data1["Usr_AuthKey"].ToString();
                                    var lastSuccessLogin = data1["Usr_SuccessLoginTime"].ToString() == ""
                                        ? DateTime.Now.AddHours(-9)
                                        : Convert.ToDateTime(data1["Usr_SuccessLoginTime"].ToString());
                                    if (singleSession != true || authKey == "" || model.LogIn == true ||
                                        lastSuccessLogin <= DateTime.Now.AddHours(-8))
                                    {
                                        authKey = Cryptography.GetKey();
                                        var lastLogin = data1["Usr_SuccessLoginTime"].ToString() == ""
                                            ? (DateTime?) null
                                            : Convert.ToDateTime(data1["Usr_SuccessLoginTime"].ToString());
                                        _list.Clear();
                                        _list.Add(new ViewParam() {Name = "usrId", Value = id});
                                        _list.Add(new ViewParam() {Name = "ipAddress", Value = ipAddress});
                                        _list.Add(new ViewParam() {Name = "authkey", Value = authKey});
                                        dynamic dataset = await _dataManager.ExecuteReaderWithSP("I_LoginUserSuccess",
                                            _list, model.CompName);
                                        if (dataset != null && dataset.Count > 0 && dataset[0].Count > 0)
                                        {
                                            var dataset1 = (IDictionary<string, object>) dataset[0][0];
                                            var roles = dataset1["Roles"].ToString();
                                            var LocIDs = dataset1["LocIDs"].ToString();
                                            var LocID = dataset1["LocID"].ToString();
                                            //  AuthenticateResponse authenticate = new AuthenticateResponse();
                                            authenticateResponse = Maper.MapList<AuthenticateResponse>(dataset[0])[0];
                                            if (authenticateResponse != null)
                                            {
                                                authenticateResponse.LastLogin = lastLogin;
                                                authenticateResponse.Token = GenerateJwtToken(id, authKey,
                                                    model.CompName, roles, LocIDs, LocID);
                                            }

                                            message = ResponseMessage.LoginSuccess;
                                            retVal = 1; //return 200 true --->   User login success;
                                        }
                                        else
                                        {
                                            message = ResponseMessage.Error;
                                            retVal = -1; //return 501  -->  Error occured
                                        }
                                    }
                                    else
                                    {
                                        message = ResponseMessage.LoginUnSuccess;
                                        retVal = 2; //Return 200-false ---->User already login  
                                    }
                                }
                                else
                                {
                                    _list.Clear();
                                    _list.Add(new ViewParam() {Name = "usrId", Value = id});
                                    _list.Add(new ViewParam() {Name = "ipAddress", Value = ipAddress});
                                    _list.Add(new ViewParam()
                                    {
                                        Name = "description",
                                        Value = "Incorrect password count " + (failedAttempt + 1).ToString() + "."
                                    });
                                    dynamic dataSet =
                                        await _dataManager.ExecuteReaderWithSP("I_LoginUserFailed", _list,
                                            model.CompName);
                                    if (dataSet == null || dataSet.Count <= 0)
                                        return (authenticateResponse, retVal, message);
                                    message = ResponseMessage.AuthenticationFail + " Remaining atempt = " +
                                              (4 - failedAttempt).ToString() + ".";
                                    retVal = 3; //Return 401-true ---->  User login failed 
                                }
                            }
                            else
                            {
                                _list.Clear();
                                _list.Add(new ViewParam() {Name = "usrId", Value = id});
                                _list.Add(new ViewParam() {Name = "ipAddress", Value = ipAddress});
                                _list.Add(new ViewParam()
                                {
                                    Name = "description",
                                    Value = "Incorrect password count " + (failedAttempt + 1).ToString() + "."
                                });
                                dynamic dataset =
                                    await _dataManager.ExecuteReaderWithSP("I_LoginUserFailed", _list, model.CompName);
                                if (dataset == null || dataset.Count <= 0)
                                    return (authenticateResponse, retVal, message);
                                message = ResponseMessage.MaxUnsuccessLogin +
                                          (failedTime.AddMinutes(15) - DateTime.Now).Minutes.ToString() + " minutes.";
                                retVal = 4; //Return 401-false -->  max unsuccessful login reached 
                            }
                        }
                        else
                        {
                            message = ResponseMessage.InvalidUser;
                            retVal = 0; //Return 403 User not found..
                        }
                    }
                    else
                    {
                        message = ResponseMessage.Error;
                        retVal = -1; //Return Error if any Response->500
                    }
                }
                else
                {
                    message = ResponseMessage.InvalidCompid;
                    retVal = 5; //Return 403 instute id not found..
                }

                return (authenticateResponse, retVal, message);
            }
            catch (Exception ex)
            {
                _dataManager.ErrorSave(ex, _dataManager, null, model.CompName);
                return (null, -2, "Something went wrong.");
            }
        }

        #endregion

        #region GetOrValidateCurrentUser

        public async Task<bool> ValidateLogin(string authKey, int userId, string compName)
        {
            const string query =
                "select count(Usr_kid) from  i_Usr where Usr_kid=@UserId and (Usr_AuthKey=@authKey or Usr_SingleSession=0) and Usr_Status=1";
            _list.Add(new ViewParam() {Name = "UserId", Value = userId});
            _list.Add(new ViewParam() {Name = "authKey", Value = authKey});
            var count = await _dataManager.ExecuteScalarWithQuery(query, _list, compName);

            return Convert.ToInt32(count) > 0;
        }

        public async Task<bool> ValidateRole(string roles, string path)
        {
            return true;
        }

        #endregion

        #region GenerateJwt

        private string GenerateJwtToken(string id, string authKey, string compName, string roles = "",
            string locIDs = "", string locId = "")
        {
            // generate token that is valid for 8 hrs only.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                IssuedAt = DateTime.Now,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", id), new Claim("roles", roles), new Claim("authKey", authKey),
                    new Claim("compName", compName), new Claim("LocIDs", locIDs), new Claim("LocID", locId)
                }),
                Expires = DateTime.UtcNow.AddHours(_appSettings.AccessTokenExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}