using INV.Core;
using INV.Dto;
using INV.Dto.Request;
using INV.Helper;
using INV.Services.Infrastructure.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace INV.Services.Masters
{
    public class SupplierServices : ISupplierServices
    {
        #region ClassMembers

        private DataManager _dataManager;
        private DataWare _dataWare;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private TResponse _response = new();
        private List<ViewParam> _list = new();
        #endregion

        #region Constructor

        public SupplierServices(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _dataManager = new DataManager(config, httpContextAccessor);
            _dataWare = new DataWare(config, httpContextAccessor);
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion



        public async Task<TResponse> DeleteSupplier(int ID, int usrID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "userID", Value = usrID });
                _list.Add(new ViewParam() { Name = "id", Value = ID });
                DataSet _dataSet = await _dataWare.GetDataSet("I_Supplier_Delete", _list);

                if (_dataSet != null && _dataSet.Tables.Count > 0 && _dataSet.Tables[0].Rows.Count > 0 && _dataSet.Tables[0].Rows[0][0].ToString() != "-1")
                {
                    _response.ResponseCode = StatusCodes.Status200OK;
                    if (_dataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        _response.ResponseStatus = true;
                        _response.ResponseMessage = ResponseMessage.Delete;
                    }
                    else
                    {
                        _response.ResponseStatus = false;
                        _response.ResponseMessage = ResponseMessage.DeleteFailed;
                    }
                }
                else
                {
                    _response.ResponseCode = StatusCodes.Status500InternalServerError;
                    _response.ResponseStatus = false;
                    _response.ResponseMessage = ResponseMessage.DeleteFailed;
                }
                return _response;
            }
            catch (Exception ex)
            {
                _dataManager.ErrorSave(ex, _dataManager, null);
                _response.ResponseCode = StatusCodes.Status500InternalServerError;
                _response.ResponseMessage = ResponseMessage.Error;
                _response.ResponseStatus = false;
                return _response;
            }

        }


        public async Task<TResponse> GetAllSupplier(int LocID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "LocID", Value = LocID });
                DataSet _dataSet = await _dataWare.GetDataSet("I_Supplier_get", _list);
                
                if (_dataSet != null && _dataSet.Tables.Count > 0)
                {
                    _response.ResponseCode = StatusCodes.Status200OK;
                    _response.ResponseStatus = true;
                    _response.ResponseMessage = ResponseMessage.Success;
                    _response.ResponsePacket = Common.DatatableToObject(_dataSet.Tables[0]);
                }
                else
                {
                    _response.ResponseCode = StatusCodes.Status500InternalServerError;
                    _response.ResponseStatus = false;
                    _response.ResponseMessage = ResponseMessage.Error;
                }
                return _response;
            }
            catch (Exception ex)
            {
                _dataManager.ErrorSave(ex, _dataManager, null);
                _response.ResponseCode = StatusCodes.Status500InternalServerError;
                _response.ResponseMessage = ResponseMessage.Error;
                _response.ResponseStatus = false;
                return _response;
            }
        }


        public async Task<TResponse> SaveOrUpdateSupplier(SupplierDto reqResponseDto, int usrID, int LocID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "id", Value = reqResponseDto.ID });
                _list.Add(new ViewParam() { Name = "name", Value = reqResponseDto.Name });
                _list.Add(new ViewParam() { Name = "status", Value = reqResponseDto.Status});
                _list.Add(new ViewParam() { Name = "areaId", Value = reqResponseDto.AreaID});
                _list.Add(new ViewParam() { Name = "bankBenifName", Value = reqResponseDto.BankBenifName});
                _list.Add(new ViewParam() { Name = "bankIfsc", Value = reqResponseDto.BankIfsc});
                _list.Add(new ViewParam() { Name = "bankName", Value = reqResponseDto.BankName});
                _list.Add(new ViewParam() { Name = "email", Value = reqResponseDto.Email});
                _list.Add(new ViewParam() { Name = "fax", Value = reqResponseDto.Fax});
                _list.Add(new ViewParam() { Name = "gstNo", Value = reqResponseDto.GstNo});
                _list.Add(new ViewParam() { Name = "license", Value = reqResponseDto.License});
                _list.Add(new ViewParam() { Name = "pocDesign", Value = reqResponseDto.PocDesign});
                _list.Add(new ViewParam() { Name = "pocEmail", Value = reqResponseDto.PocEmail});
                _list.Add(new ViewParam() { Name = "pocName", Value = reqResponseDto.PocName});
                _list.Add(new ViewParam() { Name = "pocPhone", Value = reqResponseDto.PocPhone});
                _list.Add(new ViewParam() { Name = "telno", Value = reqResponseDto.Telno});
                _list.Add(new ViewParam() { Name = "tinNo", Value = reqResponseDto.TinNo});
                _list.Add(new ViewParam() { Name = "vatNo", Value = reqResponseDto.VatNo});
                _list.Add(new ViewParam() { Name = "bankAcc", Value = reqResponseDto.BankAcc });
                _list.Add(new ViewParam() { Name = "website", Value = reqResponseDto.Website});
                _list.Add(new ViewParam() { Name = "LocID", Value = LocID });
                _list.Add(new ViewParam() { Name = "address", Value = reqResponseDto.Address });
                _list.Add(new ViewParam() { Name = "usrID", Value = usrID });
              
                DataSet _dataSet = await _dataWare.GetDataSet("I_Supplier_Save", _list);
                if (_dataSet != null && _dataSet.Tables.Count > 0 && _dataSet.Tables[0].Rows.Count > 0)
                {
                    if (_dataSet.Tables[0].Rows[0][0].ToString() == "-1")
                    {
                        _response.ResponseCode = StatusCodes.Status500InternalServerError;
                        _response.ResponseStatus = false;
                        _response.ResponseMessage = ResponseMessage.Error;
                    }
                    else
                    {
                        _response.ResponseCode = StatusCodes.Status200OK;
                        _response.ResponseStatus = true;
                        if (_dataSet.Tables[0].Rows[0][0].ToString() == "1")
                            _response.ResponseMessage = ResponseMessage.Save;
                        else if (_dataSet.Tables[0].Rows[0][0].ToString() == "2")
                            _response.ResponseMessage = ResponseMessage.Update;
                        else
                            _response.ResponseMessage = ResponseMessage.Duplicate;
                    }
                }
                else
                {
                    _response.ResponseCode = StatusCodes.Status500InternalServerError;
                    _response.ResponseStatus = false;
                    _response.ResponseMessage = ResponseMessage.Error;
                }
                return _response;
            }
            catch (Exception ex)
            {
                _dataManager.ErrorSave(ex, _dataManager, null);
                _response.ResponseCode = StatusCodes.Status500InternalServerError;
                _response.ResponseMessage = ResponseMessage.Error;
                _response.ResponseStatus = false;
                return _response;
            }
        }
    }
}