using System;
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

        public static void AddLink( DatoModel objDatoModel, int Tipo)
        {
            SqlConnection connection = new SqlConnection(StaticConnectionString);
            SqlCommand updateCommand = new SqlCommand("TBDato_UpdateLink", connection);
            updateCommand.CommandType = CommandType.StoredProcedure;

            if (Tipo==1)
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
    }
}