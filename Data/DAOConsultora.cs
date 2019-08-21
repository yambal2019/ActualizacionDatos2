using ActualizacionDatosCampaña.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActualizacionDatosCampaña.Data
{
    public class DAOConsultora : BaseData
    {
        public static void Busqueda_Consultora(ref ConsultoraModel objModel)
        {

            string storedProcName = "[TBConsultora_Busqueda]";
            objModel.Exito = 0;
            using (SqlConnection connection = new SqlConnection(StaticConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@vchDNI", objModel.vchDato);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                objModel.vchCodConsultora = dt.Rows[0]["vchCodConsultora"].ToString();
                                objModel.idPromocion = Convert.ToInt32(dt.Rows[0]["idPromocion"]);
                                objModel.vchNombre = dt.Rows[0]["vchNombreCompleto"].ToString();
                                objModel.Exito = 1;
                                //objModel.vchEncriptadoSMS = Helper.Encrypt(objModel.vchCodConsultora + "," +objModel.idPromocion + "," + "1");
                                //objModel.vchEncriptadoEmail = Helper.Encrypt(objModel.vchCodConsultora + "," + objModel.idPromocion + "," + "2");
                            }
                        }
                    }
                }
            }


        }

        //public static Int32 InsertDato(ConsultoraModel consultoraModel)
        //{
        //    Int32 Error = 0;

        //    SqlCommand command = new SqlCommand();
        //    command.CommandText = "TBDato_General";
        //    command.CommandType = CommandType.StoredProcedure;
        //    SqlConnection objConnection = new SqlConnection(StaticConnectionString);
        //    command.Connection = objConnection;

        //    objConnection.Open();

        //    try
        //    {
        //        command.Parameters.AddWithValue("@idDato", 0);
        //        command.Parameters.AddWithValue("@idTipoDocumento", consultoraModel.TipoDocumentoId);
        //        command.Parameters.AddWithValue("@vchDato", consultoraModel.vchDato);
        //        command.Parameters.AddWithValue("@vchEmail", consultoraModel.vchEmail);
        //        command.Parameters.AddWithValue("@vchTelefono", consultoraModel.vchTelefono);
        //        command.Parameters.AddWithValue("@bitEnviado", consultoraModel.bitEnviado);
        //        command.Parameters.AddWithValue("@bitConfirmado", consultoraModel.bitConfirmado);
        //        command.Parameters.AddWithValue("@dtmFechaRegistro", consultoraModel.dtmFechaRegistro);
        //        command.Parameters.AddWithValue("@dtmFechaConfirmado", consultoraModel.dtmFechaConfirmado);
        //        command.Parameters.AddWithValue("@dtmFechaEnvio", consultoraModel.dtmFechaEnvio);
        //        command.Parameters.AddWithValue("@idConsultora", consultoraModel.idConsultora);




        //        SqlParameter ret = command.Parameters.Add("@intError", SqlDbType.Int);
        //        ret.Direction = ParameterDirection.Output;
        //        ret.Value = 0;

        //        command.ExecuteNonQuery();

        //        Error = Convert.ToInt32(command.Parameters["@intError"].Value);

        //    }
        //    catch (Exception ex)
        //    {
        //        Error = 0;
        //    }
        //    finally
        //    {
        //        command.Dispose();
        //    }


        //    return Error;
        //}
    }
}