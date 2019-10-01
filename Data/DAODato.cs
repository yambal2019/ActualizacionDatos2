using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ActualizacionDatosCampaña.Models;

namespace ActualizacionDatosCampaña.Data
{
    public class DAODato : BaseData
    {
        public static bool Update(DatoModel TBDato)
        {
            SqlConnection connection = new SqlConnection(StaticConnectionString);

            string updateProcedure = "TBDato_Update";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);

            updateCommand.CommandType = CommandType.StoredProcedure;

            updateCommand.Parameters.AddWithValue("@idDato", TBDato.idDato);


            if (TBDato.bitConfirmadoSMS.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@bitConfirmadoSMS", TBDato.bitConfirmadoSMS);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@bitConfirmadoSMS", DBNull.Value);
            }

            if (TBDato.dtmFechaConfirmadoSMS.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@dtmFechaConfirmadoSMS", TBDato.dtmFechaConfirmadoSMS);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@dtmFechaConfirmadoSMS", DBNull.Value);
            }
            //==========================================================================================
            if (TBDato.bitConfirmadoEmail.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@bitConfirmadoEmail", TBDato.bitConfirmadoEmail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@bitConfirmadoEmail", DBNull.Value);
            }

            if (TBDato.dtmFechaConfirmadoEmail.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@dtmFechaConfirmadoEmail", TBDato.dtmFechaConfirmadoEmail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@dtmFechaConfirmadoEmail", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@vchEmail", TBDato.vchEmail);
            //==========================================================================================

            updateCommand.Parameters.AddWithValue("@vchEstado", TBDato.vchEstado);

            updateCommand.Parameters.AddWithValue("@idTipo", TBDato.TipoEnvio);

            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                updateCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Add(ref DatoModel TBDato)
        {
            SqlConnection connection = new SqlConnection(StaticConnectionString);

            string insertProcedure = "TBDato_Insert";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.AddWithValue("@vchTelefono", TBDato.vchTelefono);
            insertCommand.Parameters.AddWithValue("@bitEnviadoSMS", TBDato.bitEnviadoSMS);
            insertCommand.Parameters.AddWithValue("@dtmFechaEnvioSMS", TBDato.dtmFechaEnvioSMS);

            insertCommand.Parameters.AddWithValue("@vchCodConsultora", TBDato.vchCodConsultora);
            insertCommand.Parameters.AddWithValue("@vchEstado", TBDato.vchEstado);
            insertCommand.Parameters.AddWithValue("@idPromocion", TBDato.idPromocion);
            insertCommand.Parameters.AddWithValue("@vchTipoCanal", 1);/*TBDato.vchTipoCanal*/
            insertCommand.Parameters.AddWithValue("@bitTerminosCondiciones", TBDato.bitTerminosCondiciones);

            insertCommand.Parameters.AddWithValue("@vchTelefonoOld", TBDato.vchTelefonoAntiguo);
            insertCommand.Parameters.AddWithValue("@vchEmailOld", TBDato.vchEmailAntiguo);
            insertCommand.Parameters.AddWithValue("@vchTipoDocumento", TBDato.idTipoDocumento);
            insertCommand.Parameters.AddWithValue("@vchDato", TBDato.vchDato);

            insertCommand.Parameters.Add("@NewIdDato", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@NewIdDato"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {

                    TBDato.idDato = Convert.ToInt32(insertCommand.Parameters["@NewIdDato"].Value);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            finally
            {
                insertCommand.Dispose();
                connection.Close();
            }
        }

        public static void AddLink(DatoModel objDatoModel, int Tipo)
        {
            SqlConnection connection = new SqlConnection(StaticConnectionString);
            SqlCommand updateCommand = new SqlCommand("TBDato_UpdateLink", connection);
            updateCommand.CommandType = CommandType.StoredProcedure;

            if (Tipo == 1)
            {
                updateCommand.Parameters.AddWithValue("@vchlink", objDatoModel.vchLink);
            }
            if (Tipo == 2)
            {
                updateCommand.Parameters.AddWithValue("@vchlink", objDatoModel.vchLink);
            }
            updateCommand.Parameters.AddWithValue("@intTipo", Tipo);
            updateCommand.Parameters.AddWithValue("@idDato", objDatoModel.idDato);

            try
            {
                connection.Open();
                updateCommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {

            }
            finally
            {
                connection.Close();
            }

        }


        public static List<DatoModel> BusquedaConsultora(DatosViewModel objDatosViewModel)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBDatoPorCodConsultora";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("tb");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);

            List<DatoModel> objList = new List<DatoModel>();

            try
            {
                command.Parameters.AddWithValue("@vchDato", objDatosViewModel.vchDato);               
                command.Parameters.AddWithValue("@vchTipoDocumento", objDatosViewModel.TipoDocumentoId);


                staticConnection.Open();
                sqlAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DatoModel obj = new DatoModel();

                        obj.idDato = (Int32)row["idDato"];
                        obj.vchTipoDocumento = Convert.IsDBNull(row["vchTipoDocumento"]) ? null : (string)row["vchTipoDocumento"];
                        obj.vchDato = Convert.IsDBNull(row["vchDato"]) ? null : (string)row["vchDato"];

                        obj.vchTipoCanal = Convert.IsDBNull(row["vchTipoCanal"]) ? null : (string)row["vchTipoCanal"];
                        obj.vchEmail = Convert.IsDBNull(row["vchEmail"]) ? null : (string)row["vchEmail"];
                        obj.vchTelefono = Convert.IsDBNull(row["vchTelefono"]) ? null : (string)row["vchTelefono"];
                        obj.bitEnviadoSMS = Convert.IsDBNull(row["bitEnviadoSMS"]) ? false : (bool)row["bitEnviadoSMS"];

                        if (!Convert.IsDBNull(row["dtmFechaEnvioSMS"]))
                        {
                            obj.dtmFechaEnvioSMS = (DateTime)row["dtmFechaEnvioSMS"];
                        }

                        obj.bitConfirmadoSMS = Convert.IsDBNull(row["bitConfirmadoSMS"]) ? false : (bool)row["bitConfirmadoSMS"];

                        if (!Convert.IsDBNull(row["dtmFechaConfirmadoSMS"]))
                        {
                            obj.dtmFechaConfirmadoSMS = (DateTime)row["dtmFechaConfirmadoSMS"];
                        }
                        obj.vchLinkSMS = Convert.IsDBNull(row["vchLinkSMS"]) ? null : (string)row["vchLinkSMS"];
                        obj.bitEnviadoEmail = Convert.IsDBNull(row["bitEnviadoEmail"]) ? false : (bool)row["bitEnviadoEmail"];
                        if (!Convert.IsDBNull(row["dtmFechaEnviadoEmail"]))
                        {
                            obj.dtmFechaEnviadoEmail = (DateTime)row["dtmFechaEnviadoEmail"];
                        }
                        obj.bitConfirmadoEmail = Convert.IsDBNull(row["bitConfirmadoEmail"]) ? false : (bool)row["bitConfirmadoEmail"];
                        if (!Convert.IsDBNull(row["dtmFechaConfirmadoEmail"]))
                        {
                            obj.dtmFechaConfirmadoEmail = (DateTime)row["dtmFechaConfirmadoEmail"];
                        }

                        obj.vchLinkEmail = Convert.IsDBNull(row["vchLinkEmail"]) ? null : (string)row["vchLinkEmail"];

                        if (!Convert.IsDBNull(row["vchCodConsultora"]))
                        {
                            obj.vchCodConsultora = row["vchCodConsultora"].ToString();
                        }
                        obj.vchEstado = Convert.IsDBNull(row["vchEstado"]) ? null : (string)row["vchEstado"];
                        obj.bitTerminosCondiciones = Convert.IsDBNull(row["bitTerminosCondiciones"]) ? false : (bool)row["bitTerminosCondiciones"];
                        obj.vchEmailAntiguo = Convert.IsDBNull(row["vchEmailOld"]) ? null : (string)row["vchEmailOld"];
                        obj.vchTelefonoAntiguo = Convert.IsDBNull(row["vchTelefonoOld"]) ? null : (string)row["vchTelefonoOld"];

                        objList.Add(obj);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }
            return objList;
        }

        public static DataTable DescargaDatosRangofecha(DateTime dtmFechaInicio, DateTime dtmFechaFin)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "TBDatos_PorRangoFecha";
            command.CommandType = CommandType.StoredProcedure;
            SqlConnection staticConnection = StaticSqlConnection;
            command.Connection = staticConnection;

            DataTable dt = new DataTable("tb");
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);

            List<DatoModel> objList = new List<DatoModel>();

            try
            {
                command.Parameters.AddWithValue("@dtmFechaInicio", dtmFechaInicio);
                command.Parameters.AddWithValue("@dtmFechaFin", dtmFechaFin);


                staticConnection.Open();
                sqlAdapter.Fill(dt);


            }
            catch (Exception ex)
            {

            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }
            return dt;
        }
    }
}