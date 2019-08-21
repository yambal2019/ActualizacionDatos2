using ActualizacionDatosCampaña.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ActualizacionDatosCampaña.Data
{
    public class DAOParametro : BaseData
    {


        public static List<ParametroModel> ParametroLista(String Valor)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBParametro_SelectPorTipo";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@intTipo", Convert.ToInt32(Valor));

            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("TBParametro");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
            try
            {

                staticConnection.Open();
                sqlAdapter.Fill(dt);

                List<ParametroModel> objList = new List<ParametroModel>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ParametroModel TBParametro = new ParametroModel();
                        TBParametro.idParametro = System.Convert.ToInt32(row["idParametro"]);
                        TBParametro.intTipo = row["intTipo"] is DBNull ? null : (Int32?)row["intTipo"];
                        TBParametro.intOrden = row["intOrden"] is DBNull ? null : (Int32?)row["intOrden"];
                        TBParametro.vchCampo = row["vchCampo"] is DBNull ? null : row["vchCampo"].ToString();
                        TBParametro.vchValor = row["vchValor"] is DBNull ? null : row["vchValor"].ToString();
                        TBParametro.vchDescripcion = row["vchDescripcion"] is DBNull ? null : row["vchDescripcion"].ToString();

                        objList.Add(TBParametro);
                    }
                }
                return objList;
            }
            catch
            {
                throw;
            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }
        }



        public static ParametroModel ParametroUnValor(String Valor)
        {


            ParametroModel TBParametro = new ParametroModel();
            TBParametro.intTipo = Convert.ToInt32(Valor);

            SqlConnection connection = new SqlConnection(StaticConnectionString);


            string selectProcedure = "TBParametro_SelectOnePorTipo";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@intTipo", TBParametro.intTipo);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    TBParametro.idParametro = System.Convert.ToInt32(reader["idParametro"]);
                    TBParametro.intTipo = reader["intTipo"] is DBNull ? null : (Int32?)reader["intTipo"];
                    TBParametro.intOrden = reader["intOrden"] is DBNull ? null : (Int32?)reader["intOrden"];
                    TBParametro.vchCampo = reader["vchCampo"] is DBNull ? null : reader["vchCampo"].ToString();
                    TBParametro.vchValor = reader["vchValor"] is DBNull ? null : reader["vchValor"].ToString();
                    TBParametro.vchDescripcion = reader["vchDescripcion"] is DBNull ? null : reader["vchDescripcion"].ToString();
                    TBParametro.bitEstado = reader["bitEstado"] is DBNull ? null : (Boolean?)reader["bitEstado"];


                }
                else
                {
                    TBParametro = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return TBParametro;
            }
            finally
            {
                connection.Close();
            }
            return TBParametro;
        }
    }
}