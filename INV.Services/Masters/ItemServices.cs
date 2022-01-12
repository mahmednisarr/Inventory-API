﻿using INV.Core;
using INV.Dto;
using INV.Dto.Masters;
using INV.Dto.Request;
using INV.Helper;
using INV.Helper.helper;
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
    public class ItemServices : IItemServices
    {


        #region ClassMembers

        private DataManager _dataManager;
        private DataWare _dataWare;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private TResponse _response = new();
        private List<ViewParam> _list = new();
        
        #endregion

        #region Constructor

        public ItemServices(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _dataManager = new DataManager(config, httpContextAccessor);
            _dataWare = new DataWare(config, httpContextAccessor);
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        public async Task<TResponse> DeleteItem(int ID, int usrID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "userID", Value = usrID });
                _list.Add(new ViewParam() { Name = "id", Value = ID });
                DataSet _dataSet = await _dataWare.GetDataSet("I_Item_Delete", _list);

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


        public async Task<TResponse> GetAllItems(int LocID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "LocID", Value = LocID });
                _list.Add(new ViewParam() { Name = "flag", Value = 'L' });
                DataSet _dataSet = await _dataWare.GetDataSet("I_Item_get", _list);
                
                if (_dataSet != null && _dataSet.Tables.Count > 0)
                {
                    _response.ResponseCode = StatusCodes.Status200OK;
                    _response.ResponseStatus = true;
                    _response.ResponseMessage = ResponseMessage.Success;
                    _response.ResponsePacket = _dataSet.Tables[0].DataTableToList<ItemDto>();
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


        public async Task<TResponse> SaveOrUpdateItem(ItemDto reqResponseDto, int usrID, int LocID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "id", Value = reqResponseDto.ID });
                _list.Add(new ViewParam() { Name = "name", Value = reqResponseDto.Name });
                _list.Add(new ViewParam() { Name = "status", Value = reqResponseDto.Status });
                if (reqResponseDto.Code != null && reqResponseDto.Code != "")
                    _list.Add(new ViewParam() { Name = "code", Value = reqResponseDto.Code });
                _list.Add(new ViewParam() { Name = "locid", Value = LocID });
                _list.Add(new ViewParam() { Name = "usrId", Value = usrID });
                _list.Add(new ViewParam() { Name = "hsncode", Value = reqResponseDto.HSNCode });
                _list.Add(new ViewParam() { Name = "grpid", Value = reqResponseDto.GrpID });
                _list.Add(new ViewParam() { Name = "unittypeid", Value = reqResponseDto.UnitTypeid });
                DataSet _dataSet = await _dataWare.GetDataSet("I_Item_Save", _list);
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
                _dataManager.ErrorSave(ex, _dataManager, null);
                _response.ResponseCode = StatusCodes.Status500InternalServerError;
                _response.ResponseMessage = ResponseMessage.Error;
                _response.ResponseStatus = false;
                return _response;
            }
        }
    }
}