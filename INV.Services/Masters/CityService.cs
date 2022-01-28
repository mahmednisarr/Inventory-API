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
    public class CityService : ICityService
    {
        DataWare _dataWare;
        DataManager _DM;
        DataTable _DT = new DataTable();
        DataSet _dataSet = new DataSet();
        List<ViewParam> _list = new List<ViewParam>();
        TResponse _response = new TResponse();

        public CityService(IHttpContextAccessor httpContext, IConfiguration _config)
        {
            _dataWare = new DataWare(_config, httpContext);
            _DM = new DataManager(_config, httpContext);
        }


        public async Task<TResponse> GetCityByPincode(int pincode)
        {
            _list.Clear();
            _list.Add(new ViewParam() { Name= "pincode", Value=pincode});
            DataSet _dataSet = await _dataWare.GetDataSet("I_city_get", _list);

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
    }
}
