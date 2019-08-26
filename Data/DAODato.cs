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

            updateCommand.Parameters.AddWithValue("@idDato", TBDato.intDato);


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

    //vchDato NVARCHAR(50),
    //vchEmail NVARCHAR(50),
    //vchTelefono NCHAR(10),
    //bitEnviadoSMS BIT,
    //dtmFechaEnvioSMS DATETIME,
    //bitConfirmadoSMS BIT,
    //dtmFechaConfirmadoSMS DATETIME,

    //bitEnviadoEmail BIT,
    //dtmFechaEnviadoEmail DATETIME,
    //bitConfirmadoEmail BIT,
    //dtmFechaConfirmadoEmail DATETIME,
    //vchCodConsultora INT,
    //vchEstado NVARCHAR(1),
    //idPromocion INT,
    //ReturnValue INT OUTPUT,
    //NewIdDato INT OUTPUT


             //insertCommand.Parameters.AddWithValue("@vchDato", TBDato.vchDato);
            //insertCommand.Parameters.AddWithValue("@vchEmail", TBDato.vchEmail);
            insertCommand.Parameters.AddWithValue("@vchTelefono", TBDato.vchTelefono);
            insertCommand.Parameters.AddWithValue("@bitEnviadoSMS", TBDato.bitEnviadoSMS);
            insertCommand.Parameters.AddWithValue("@dtmFechaEnvioSMS", TBDato.dtmFechaEnvioSMS);
            //insertCommand.Parameters.AddWithValue("@bitConfirmadoSMS", TBDato.bitConfirmadoSMS);
            //insertCommand.Parameters.AddWithValue("@dtmFechaConfirmadoSMS", TBDato.dtmFechaConfirmadoSMS);

            //insertCommand.Parameters.AddWithValue("@bitEnviadoEmail", TBDato.bitEnviadoEmail);
            //insertCommand.Parameters.AddWithValue("@dtmFechaEnviadoEmail", TBDato.dtmFechaEnviadoEmail);
            //insertCommand.Parameters.AddWithValue("@bitConfirmadoEmail", TBDato.bitConfirmadoEmail);
            insertCommand.Parameters.AddWithValue("@vchCodConsultora", TBDato.vchCodConsultora);
            insertCommand.Parameters.AddWithValue("@vchEstado", TBDato.vchEstado);
            insertCommand.Parameters.AddWithValue("@idPromocion", TBDato.idPromocion);

       

            
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