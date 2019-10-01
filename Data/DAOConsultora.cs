using ActualizacionDatosCampaña.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ActualizacionDatosCampaña.WS;
using System.Collections.Generic;
using ActualizacionDatosCampaña.Utilities;

namespace ActualizacionDatosCampaña.Data
{
    public class DAOConsultora : BaseData
    {

    
      


        //public static void BusquedaConsultora(ConsultoraModel objModel)
        //{


        //    objModel.Exito = 0;

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "[TBConsultoraPorCodConsultora]";
        //    SqlConnection connection = new SqlConnection(StaticConnectionString);
        //    cmd.Connection = connection;

        //    connection.Open();

        //    try
        //    {
        //        cmd.Parameters.Add("@idPromocion", SqlDbType.NVarChar);
        //        cmd.Parameters["@idPromocion"].Direction = ParameterDirection.Output;


        //        int i = cmd.ExecuteNonQuery();

        //        objModel.idPromocion = Convert.ToInt32(cmd.Parameters["@idPromocion"].Value);
        //        objModel.Exito = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        objModel.Exito = 0;
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //        connection.Close();
        //    }
        //}


        public static void Busqueda_Consultora(ref ConsultoraModel objModel)
        {


            objModel.Exito = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[TBConsultora_Busqueda]";
            SqlConnection connection = new SqlConnection(StaticConnectionString);
            cmd.Connection = connection;

            connection.Open();

            try
            {
                cmd.Parameters.Add("@idPromocion", SqlDbType.Int);
                cmd.Parameters["@idPromocion"].Direction = ParameterDirection.Output;


                int i = cmd.ExecuteNonQuery();

                objModel.idPromocion = Convert.ToInt32(cmd.Parameters["@idPromocion"].Value);
                objModel.Exito = 1;
            }
            catch (Exception ex)
            {
                objModel.Exito = 0;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        public static WSConsultoraModel LlamadaWebService(ConsultoraModel modelo)
        {
            WSConsultoraModel obj = new WSConsultoraModel();

            List<ParametroModel> objListaParametro = Helper.ListaBD("WS_CONSULTORA");


            IntegracionWSReq WSReq = new IntegracionWSReq();

            Cabecera c = new Cabecera();
            c.codigoInterfaz = objListaParametro[0].vchValor; // "CCNSJSX";
            c.usuarioAplicacion = objListaParametro[1].vchValor; // "USRKPD";
            c.codigoAplicacion = objListaParametro[2].vchValor; //"KPD";
            c.codigoPais = objListaParametro[3].vchValor; //  "PE";

            List<CodigoPaisODType> lista = new List<CodigoPaisODType>();

            CodigoPaisODType paisOD = new CodigoPaisODType();
            paisOD.valor = objListaParametro[3].vchValor; //"PE";
            lista.Add(paisOD);

            c.codigosPaisOD = lista.ToArray();
            WSReq.cabecera = c;

            Detalle d = new Detalle();
            ConsultarConsultoraJSXEnvType pp = new ConsultarConsultoraJSXEnvType();

            if (modelo.TipoDocumentoId==1)
            {
                pp.tipoBusqueda = objListaParametro[4].vchValor; //"C";
            }
            if (modelo.TipoDocumentoId == 2)
            {
                pp.tipoBusqueda =  objListaParametro[5].vchValor; //"DNI";
            }
            
            
            pp.codBusqueda = modelo.vchDato; //"1113562144";  //----BO 141018
            d.consultoraReq = pp;

            WSReq.detalle = d;
            IntegracionWSResp p = new IntegracionWSResp();

            WS.WSConsultora_SXSDImplClient obj3 = new WSConsultora_SXSDImplClient();
            p = obj3.consultarCNSXCodigo(WSReq);

            ConsultoraJSXRtaType[] consultoraJSXRtaType;

            consultoraJSXRtaType = p.detalle.datos.consultoras;

            if (consultoraJSXRtaType != null)
            {
                if (consultoraJSXRtaType.Length > 0)
                {
                    obj.vchCodConsultora = p.detalle.datos.consultoras[0].codConsultora.ToString();
                    obj.telefonoMovil = p.detalle.datos.consultoras[0].telefonoMovil.ToString();
                    obj.email = p.detalle.datos.consultoras[0].email.ToString();
                    obj.nombreCompleto = p.detalle.datos.consultoras[0].nombres.ToString();

                    if (p.detalle.datos.consultoras[0].codEjecutivaServicios.Length==0)
                    {
                        obj.Participacion = "0";
                    }

                }
            }


            return obj;

        }
    }
}



//using (SqlConnection connection = new SqlConnection(StaticConnectionString))
//{
//    connection.Open();

//    using (SqlCommand command = new SqlCommand(storedProcName, connection))
//    {
//        command.CommandType = CommandType.StoredProcedure;
//        //command.Parameters.AddWithValue("@vchDNI", objModel.vchDato);
//        // command.Parameters.AddWithValue("@idPromocion", objModel.vchDato);
//        SqlParameter pa = new SqlParameter("@idPromocion", SqlDbType.Int);
//        pa.Direction = ParameterDirection.Output;
//        command.Parameters.Add(pa);

//        try
//        {

//            command.ExecuteNonQuery();
//            objModel.idPromocion = Convert.IsDBNull(command.Parameters["@idPromocion"].Value) ? 0: (Int32)command.Parameters["@iintPedido"].Value;


//        }
//        catch
//        {
//            throw;
//        }
//        finally
//        {
//            command.Dispose();
//        }

//        //using (SqlDataAdapter da = new SqlDataAdapter(command))
//        //{
//        //    DataTable dt = new DataTable();
//        //    da.Fill(dt);

//        //    if (dt != null)
//        //    {
//        //        if (dt.Rows.Count > 0)
//        //        {
//        //            objModel.vchCodConsultora = dt.Rows[0]["vchCodConsultora"].ToString();
//        //            objModel.idPromocion = Convert.ToInt32(dt.Rows[0]["idPromocion"]);
//        //            objModel.vchNombre = dt.Rows[0]["vchNombreCompleto"].ToString();
//        //            objModel.Exito = 1;

//        //        }
//        //    }
//        //}
//    }
//}




//public static void Busqueda_Consultora(ref ConsultoraModel objModel)
//{

//    string storedProcName = "[TBConsultora_Busqueda]";
//    objModel.Exito = 0;
//    using (SqlConnection connection = new SqlConnection(StaticConnectionString))
//    {
//        connection.Open();

//        using (SqlCommand command = new SqlCommand(storedProcName, connection))
//        {
//            command.CommandType = CommandType.StoredProcedure;
//            command.Parameters.AddWithValue("@vchDNI", objModel.vchDato);
//            using (SqlDataAdapter da = new SqlDataAdapter(command))
//            {
//                DataTable dt = new DataTable();
//                da.Fill(dt);

//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        objModel.vchCodConsultora = dt.Rows[0]["vchCodConsultora"].ToString();
//                        objModel.idPromocion = Convert.ToInt32(dt.Rows[0]["idPromocion"]);
//                        objModel.vchNombre = dt.Rows[0]["vchNombreCompleto"].ToString();
//                        objModel.Exito = 1;

//                    }
//                }
//            }
//        }
//    }


//}


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
