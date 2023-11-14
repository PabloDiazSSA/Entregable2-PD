using Newtonsoft.Json;
using Entregable2_PD.Models.Response;
using Entregable2_PD.Tools;
using System.Collections.ObjectModel;
using System.Data;
using Formatting = Newtonsoft.Json.Formatting;
using System.Data.SqlClient;

namespace Entregable2_PD.Data
{
    public class DB
    {
        private readonly IConfiguration _Config;

        public DB(IConfiguration configuration)
        {
            _Config = configuration;
        }
        public async Task<clsResponse<dynamic>> ExecuteSpAsync<T>(string sp, List<Param> lstParams)
        {
            clsResponse<dynamic> response = new clsResponse<dynamic>();
            using (var conn = new SqlConnection(_Config.GetConnectionString("DefaultConnection")))
            {
                using (var cmd = new SqlCommand(sp, conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (lstParams != null)
                        {
                            foreach (Param param in lstParams)
                            {
                                if (!param.Output)
                                {
                                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                                }
                                else
                                {
                                    cmd.Parameters.Add(param.Name, SqlDbType.VarChar, 400).Direction = ParameterDirection.Output;
                                }

                            }
                        }

                        DataTable dataTable = new DataTable();
                        await conn.OpenAsync();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dataTable);
                        }
                        await conn.CloseAsync();
                        if (lstParams != null)
                        {
                            for (int i = 0; i < lstParams.Count; i++)
                            {
                                if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                                {
                                    response.Data = string.IsNullOrEmpty(cmd.Parameters[i].Value.ToString()) ? string.Empty : cmd.Parameters[i].Value.ToString() + " | ";
                                }
                            }
                        }

                        var json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                        DataRow row = dataTable.Rows[dataTable.Rows.Count - 1];
                        if (dataTable.Rows.Count == 1)
                        {
                            if (dataTable.Columns.Contains("Error"))
                            {
                                response.Error = true;
                                response.Message = row[0].ToString();
                            }
                            else
                            {
                                response.Error = false;
                                response.Message = "Success";
                                var lstResult = JsonConvert.DeserializeObject<Collection<T>>(json);
                                response.Data = lstResult.FirstOrDefault();
                            }
                        }
                        else
                        {
                            if (dataTable.Rows.Count >= 1)
                            {
                                if (dataTable.Columns.Contains("Error"))
                                {
                                    response.Error = true;
                                    response.Message = row[0].ToString();
                                    response.Data = JsonConvert.DeserializeObject<T>(json);
                                }
                                else
                                {
                                    response.Error = false;
                                    response.Message = "Success";
                                    var lstResult = JsonConvert.DeserializeObject<Collection<T>>(json);
                                    response.Data = lstResult;
                                }
                            }
                            else
                            {
                                if (dataTable.Columns.Contains("Error"))
                                {
                                    response.Error = true;
                                    response.Message = "No data";
                                }
                                else
                                {
                                    response.Error = false;
                                    response.Message = "Success";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await conn.CloseAsync();
                        response.ErrorCode = 1;
                        response.Error = true;
                        string msg = "Cannot obtain the information, retry again.";
#if DEBUG
                        Console.WriteLine(ex.Message);
#endif
                        response.Message = msg;
                        response.ErrorMessage = msg;
                    }
                    finally { await conn.CloseAsync(); }
                    return response;
                }
            }
        }
        public async Task<DataTable> TableAsync(SqlCommand c)
        {
            DataTable dt = new DataTable("dt");
            using (var conn = new SqlConnection(_Config.GetConnectionString("DefaultConnection")))
            {
                using (var cmd = c)
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        await conn.OpenAsync();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        await conn.CloseAsync();
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        await conn.CloseAsync();
                        string msg = "Cannot obtain the information, retry again.";
#if DEBUG
                        Console.WriteLine(ex.Message);
#endif
                        throw new Exception(msg);
                    }
                }
            }
        }
    }
}