using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using INV.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using INV.Core;

namespace INV.Core
{
    public class DataWare
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        private string _compName;

        private CurrentUser currentUser;



        public DataWare(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            currentUser =(CurrentUser)_httpContextAccessor.HttpContext.Items["User"];
        }


        #region NewConnection

        private async Task<SqlConnection> New_connection(string compName = null)
        {
            if( compName != null)
            {
                _compName = compName;
            }
            else if(currentUser != null)
            {
                _compName = currentUser.CompName;
            }
            else
            {
                _compName = "ind";
            }

            var key = _config["Key"];
            var connStr = Cryptography.Decrypt(_config.GetConnectionString(_compName), key);
            var source = connStr + ";" + "Connect Timeout=0";
            var conn = await Task.Run(() => new SqlConnection(source));
            return conn;
        }

        #endregion


        #region Dataset

        public async Task<DataSet> GetDataSet(string spName, IEnumerable<ViewParam> par, string compName = null)
        {
            var ds = new DataSet();
            var cmd = new SqlCommand(spName, await New_connection(compName)) { CommandType = CommandType.StoredProcedure, CommandTimeout = 0 };
            foreach (var vp in par) cmd.Parameters.AddWithValue("@" + vp.Name.Trim(), vp.Value);
            var da = new SqlDataAdapter(cmd);
            await Task.Run(() => da.Fill(ds));

            return ds;
        }

        public async Task<DataTable> Get(string query, IEnumerable<ViewParam> par, string compName = null)
        {
            var dt = new DataTable();
            var cmd = new SqlCommand(query, await New_connection(compName)) { CommandType = CommandType.Text, CommandTimeout = 0 };
            foreach (var vp in par) cmd.Parameters.AddWithValue("@" + vp.Name.Trim(), vp.Value);
            var da = new SqlDataAdapter(cmd);
            await Task.Run(() => da.Fill(dt));


            return dt;
        }

        public async Task<DataTable> Get(string query, string compName = null)
        {
            var dt = new DataTable();
            var cmd = new SqlCommand(query, await New_connection(compName)) { CommandType = CommandType.Text, CommandTimeout = 0 };
            var da = new SqlDataAdapter(cmd);
            await Task.Run(() => da.Fill(dt));

            return dt;
        }

        public async Task<string> GetValue(string query, IEnumerable<ViewParam> par, string compName = null)
        {
            var dt = new DataTable();
            var cmd = new SqlCommand(query, await New_connection(compName)) { CommandType = CommandType.Text, CommandTimeout = 0 };
            foreach (var vp in par) cmd.Parameters.AddWithValue("@" + vp.Name.Trim(), vp.Value);
            var da = new SqlDataAdapter(cmd);
            await Task.Run(() => da.Fill(dt));

            return dt.Rows[0][0].ToString();
        }

        public async Task<string> GetValue(string query, string compName = null)
        {
            var dt = new DataTable();
            var cmd = new SqlCommand(query, await New_connection(compName)) { CommandType = CommandType.Text, CommandTimeout = 0 };

            var da = new SqlDataAdapter(cmd);
            await Task.Run(() => da.Fill(dt));

            return dt.Rows[0][0].ToString();
        }


        #endregion
    }
}