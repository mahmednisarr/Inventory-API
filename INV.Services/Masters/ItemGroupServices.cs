using INV.Core;
using INV.Dto;
using INV.Dto.Masters;
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
    public class ItemGroupServices : IItemGroupServices
    {


        #region ClassMembers

        private DataManager _dataManager;
        private DataWare _dataWare;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private TResponse _response = new();
        private List<ViewParam> _list = new();
        #endregion

        #region Constructor

        public ItemGroupServices(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _dataManager = new DataManager(config, httpContextAccessor);
            _dataWare = new DataWare(config, httpContextAccessor);
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion



        public async Task<TResponse> DeleteItemGroup(int ID, int usrID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "userID", Value = usrID });
                _list.Add(new ViewParam() { Name = "id", Value = ID });
              DataSet  _dataSet = await _dataWare.GetDataSet("I_ItemGrp_Delete", _list);

                if (_dataSet != null && _dataSet.Tables.Count > 0 && _dataSet.Tables[0].Rows.Count > 0 && _dataSet.Tables[0].Rows[0][0].ToString() != "-1")
                {
                    _response.ResponseCode = StatusCodes.Status200OK;
                    if (_dataSet.Tables[0].Rows[0][0].ToString()  == "1")
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

        public async Task<TResponse> GetAllItemGroup(int LocID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "LocID", Value = LocID });

                DataSet _dataSet = await _dataWare.GetDataSet("I_ItemGrp_get", _list);

                if (_dataSet != null && _dataSet.Tables.Count > 0)
                {

                    _response.ResponseCode = StatusCodes.Status200OK;
                    _response.ResponseStatus = true;
                    _response.ResponseMessage = ResponseMessage.Success;
                    _response.ResponsePacket = _dataSet.Tables[0].DataTableToList<ItemGroupDto>();


                }
                else
                {
                    _response.ResponseCode = StatusCodes.Status500InternalServerError;
                    _response.ResponseStatus = false;
                    _response.ResponseMessage = ResponseMessage.Error;
                }
                //dynamic dataset = await _dataManager.ExecuteReaderWithSP("I_ItemGrp_get", _list);
                //if (dataset != null && dataset.Count > 0)
                //{
                //    _response.ResponsePacket = Maper.MapList<ItemGroupDto>(dataset[0]);
                //    _response.ResponseCode = StatusCodes.Status200OK;
                //    _response.ResponseMessage = ResponseMessage.Success;
                //    _response.ResponseStatus = true;
                //}
                //else
                //{
                //    _response.ResponseCode = StatusCodes.Status500InternalServerError;
                //    _response.ResponseMessage = ResponseMessage.Error;
                //    _response.ResponseStatus = true;
                //}
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

        public async Task<TResponse> GetItemGroups(int LocID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "LocID", Value = LocID });
                _list.Add(new ViewParam() { Name = "flag", Value = 'L' });
                DataSet _dataSet = await _dataWare.GetDataSet("I_ItemGrp_get", _list);

                if (_dataSet != null && _dataSet.Tables.Count > 0)
                {

                    _response.ResponseCode = StatusCodes.Status200OK;
                    _response.ResponseStatus = true;
                    _response.ResponseMessage = ResponseMessage.Success;
                    _response.ResponsePacket = _dataSet.Tables[0].DataTableToList<ItemGroupDto>();


                }
                else
                {
                    _response.ResponseCode = StatusCodes.Status500InternalServerError;
                    _response.ResponseStatus = false;
                    _response.ResponseMessage = ResponseMessage.Error;
                }
                //dynamic dataset = await _dataManager.ExecuteReaderWithSP("I_ItemGrp_get", _list);

                //if (dataset != null && dataset.Count > 0 && dataset[0].Count > 0)
                //{
                //    _response.ResponsePacket = Maper.MapList<ItemGroupDto>(dataset[0]);
                //    _response.ResponseCode = StatusCodes.Status200OK;
                //    _response.ResponseMessage = ResponseMessage.Success;
                //    _response.ResponseStatus = true;
                //}
                //else
                //{
                //    _response.ResponseCode = StatusCodes.Status200OK;
                //    _response.ResponseStatus = false;
                //    _response.ResponseMessage = ResponseMessage.Error;
                //}
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

        public async Task<TResponse> SaveOrUpdateItemGroup(ItemGroupDto reqResponseDto, int usrID, int LocID)
        {
            try
            {
                _list.Add(new ViewParam() { Name = "id", Value = reqResponseDto.ID });
                _list.Add(new ViewParam() { Name = "name", Value = reqResponseDto.Name });
                _list.Add(new ViewParam() { Name = "status", Value = reqResponseDto.Status });
                if (reqResponseDto.GroupId != 0)
                    _list.Add(new ViewParam() { Name = "grpId", Value = reqResponseDto.GroupId });
                if (reqResponseDto.Code != null && reqResponseDto.Code != "")
                    _list.Add(new ViewParam() { Name = "code", Value = reqResponseDto.Code });
                _list.Add(new ViewParam() { Name = "LocId", Value = LocID });
                _list.Add(new ViewParam() { Name = "usrId", Value = usrID });
              DataSet  _dataSet = await _dataWare.GetDataSet("I_ItemGrp_Save", _list);
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
