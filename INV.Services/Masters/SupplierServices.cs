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
                _list.Add(new ViewParam() { Name = "flag", Value = 'L' });
                DataSet _dataSet = await _dataWare.GetDataSet("I_Supplier_get", _list);
                
                if (_dataSet != null && _dataSet.Tables.Count > 0)
                {
                    _response.ResponseCode = StatusCodes.Status200OK;
                    _response.ResponseStatus = true;
                    _response.ResponseMessage = ResponseMessage.Success;
                    _response.ResponsePacket = _dataSet.Tables[0].DataTableToList<SupplierDto>();
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
                _list.Add(new ViewParam() { Name = "code", Value = reqResponseDto.Code});
                _list.Add(new ViewParam() { Name = "usrId", Value = usrID });
                _list.Add(new ViewParam() { Name = "add1", Value = reqResponseDto.Add});
                _list.Add(new ViewParam() { Name = "contractorName", Value = reqResponseDto.Contname});
                _list.Add(new ViewParam() { Name = "phone", Value = reqResponseDto.Phone});
                _list.Add(new ViewParam() { Name = "contractorPhone", Value = reqResponseDto.ContPhone});
                _list.Add(new ViewParam() { Name = "designation", Value = reqResponseDto.Design});
                _list.Add(new ViewParam() { Name = "panNo", Value = reqResponseDto.ITPanNo});
                _list.Add(new ViewParam() { Name = "defaultOpb", Value = reqResponseDto.DefaultOpeningBal});
                _list.Add(new ViewParam() { Name = "tinNo", Value = reqResponseDto.TinNo});
                _list.Add(new ViewParam() { Name = "email", Value = reqResponseDto.Email});
                _list.Add(new ViewParam() { Name = "pwd", Value = reqResponseDto.PWD});
                _list.Add(new ViewParam() { Name = "gstIN", Value = reqResponseDto.GSTIN});
                _list.Add(new ViewParam() { Name = "crLimit", Value = reqResponseDto.CrLimit});
                _list.Add(new ViewParam() { Name = "tds", Value = reqResponseDto.TDS});
                _list.Add(new ViewParam() { Name = "LocID", Value = LocID });
              
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