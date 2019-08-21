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

            if (TBDato.vchCodConsultora != null)
            {
                updateCommand.Parameters.AddWithValue("@vchCodConsultora", TBDato.vchCodConsultora);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@vchCodConsultora", DBNull.Value);
            }


            if (TBDato.idPromocion.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@idPromocion", TBDato.idPromocion);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@idPromocion", DBNull.Value);
            }

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
            if (TBDato.idTipoDocumento.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@idTipoDocumento", TBDato.idTipoDocumento);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@idTipoDocumento", DBNull.Value);
            }
            if (TBDato.vchDato != null)
            {
                insertCommand.Parameters.AddWithValue("@vchDato", TBDato.vchDato);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@vchDato", DBNull.Value);
            }
            if (TBDato.vchEmail != null)
            {
                insertCommand.Parameters.AddWithValue("@vchEmail", TBDato.vchEmail);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@vchEmail", DBNull.Value);
            }
            if (TBDato.vchTelefono != null)
            {
                insertCommand.Parameters.AddWithValue("@vchTelefono", TBDato.vchTelefono);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@vchTelefono", DBNull.Value);
            }
            if (TBDato.bitEnviado.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@bitEnviado", TBDato.bitEnviado);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@bitEnviado", DBNull.Value);
            }
            if (TBDato.dtmFechaEnvio.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@dtmFechaEnvio", TBDato.dtmFechaEnvio);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@dtmFechaEnvio", DBNull.Value);
            }
            //if (TBDato.bitConfirmadoSMS.HasValue == true)
            //{
            //    insertCommand.Parameters.AddWithValue("@bitConfirmadoSMS", TBDato.bitConfirmadoSMS);
            //}
            //else
            //{
            //    insertCommand.Parameters.AddWithValue("@bitConfirmadoSMS", DBNull.Value);
            //}
            //if (TBDato.dtmFechaConfirmadoSMS.HasValue == true)
            //{
            //    insertCommand.Parameters.AddWithValue("@dtmFechaConfirmadoSMS", TBDato.dtmFechaConfirmadoSMS);
            //}
            //else
            //{
            //    insertCommand.Parameters.AddWithValue("@dtmFechaConfirmadoSMS", DBNull.Value);
            //}
            //if (TBDato.bitConfirmadoEmail.HasValue == true)
            //{
            //    insertCommand.Parameters.AddWithValue("@bitConfirmadoEmail", TBDato.bitConfirmadoEmail);
            //}
            //else
            //{
            //    insertCommand.Parameters.AddWithValue("@bitConfirmadoEmail", DBNull.Value);
            //}
            //if (TBDato.dtmFechaConfirmadoEmail.HasValue == true)
            //{
            //    insertCommand.Parameters.AddWithValue("@dtmFechaConfirmadoEmail", TBDato.dtmFechaConfirmadoEmail);
            //}
            //else
            //{
            //    insertCommand.Parameters.AddWithValue("@dtmFechaConfirmadoEmail", DBNull.Value);
            //}
            if (TBDato.vchCodConsultora != null)
            {
                insertCommand.Parameters.AddWithValue("@vchCodConsultora", TBDato.vchCodConsultora);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@vchCodConsultora", DBNull.Value);
            }
            if (TBDato.vchEstado != null)
            {
                insertCommand.Parameters.AddWithValue("@vchEstado", TBDato.vchEstado);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@vchEstado", DBNull.Value);
            }
            if (TBDato.idPromocion.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@idPromocion", TBDato.idPromocion);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@idPromocion", DBNull.Value);
            }
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
                connection.Close();
            }
        }


    }
}