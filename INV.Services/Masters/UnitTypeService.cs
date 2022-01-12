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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Masters
{
    public class UnitTypeService : IUnitTypeServices
    {
        DataWare _dataWare;
        DataManager _DM;
        DataTable _DT = new DataTable();
        DataSet _dataSet = new DataSet();
        List<ViewParam> _list = new List<ViewParam>();
        TResponse _response = new TResponse();

        public UnitTypeService(IHttpContextAccessor httpContext, IConfiguration _config)
        {
            _dataWare = new DataWare(_config, httpContext);
            _DM = new DataManager(_config, httpContext);
        }


        public async Task<TResponse> GetAllUnitTypes()
        {
            DataSet _dataSet = await _dataWare.GetDataSet("I_UnitType_get", _list);

            if (_dataSet != null && _dataSet.Tables.Count > 0)
            {

                _response.ResponseCode = StatusCodes.Status200OK;
                _response.ResponseStatus = true;
                _response.ResponseMessage = ResponseMessage.Success;
                _response.ResponsePacket = _dataSet.Tables[0].DataTableToList<UnitTypeDto>();


            }
            else
            {
                _response.ResponseCode = StatusCodes.Status500InternalServerError;
                _response.ResponseStatus = false;
                _response.ResponseMessage = ResponseMessage.Error;
            }

            return _response;
        }

        public async Task<TResponse> DeleteUnitType(int ID, int usrID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "userID", Value = usrID });
                _list.Add(new ViewParam() { Name = "id", Value = ID });
                DataSet _dataSet = await _dataWare.GetDataSet("I_UnitType_Delete", _list);

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
                _DM.ErrorSave(ex, _DM, null);
                _response.ResponseCode = StatusCodes.Status500InternalServerError;
                _response.ResponseMessage = ResponseMessage.Error;
                _response.ResponseStatus = false;
                return _response;
            }

        }
        
        public async Task<TResponse> SaveOrUpdateUnitType(UnitTypeDto reqResponseDto, int usrID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "id", Value = reqResponseDto.ID });
                _list.Add(new ViewParam() { Name = "name", Value = reqResponseDto.Name });
                _list.Add(new ViewParam() { Name = "status", Value = reqResponseDto.Status });
                if (reqResponseDto.Code != null && reqResponseDto.Code != "")
                    _list.Add(new ViewParam() { Name = "code", Value = reqResponseDto.Code });
                _list.Add(new ViewParam() { Name = "SIName", Value = reqResponseDto.SIName });
                _list.Add(new ViewParam() { Name = "usrId", Value = usrID });
                _dataSet = await _dataWare.GetDataSet("I_UnitType_Save", _list);
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
                        {

                            _response.ResponseMessage = ResponseMessage.Save;
                        }
                        else if (_dataSet.Tables[0].Rows[0][0].ToString() == "2")
                        {

                            _response.ResponseMessage = ResponseMessage.Update;
                        }
                        else
                        {
                            _response.ResponseMessage = ResponseMessage.Duplicate;
                        }
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
                _DM.ErrorSave(ex, _DM, null);
                _response.ResponseCode = StatusCodes.Status500InternalServerError;
                _response.ResponseMessage = ResponseMessage.Error;
                _response.ResponseStatus = false;
                return _response;
            }
        }
    }
}
