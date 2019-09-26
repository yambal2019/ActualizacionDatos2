using ActualizacionDatosCampaña.Data;
using ActualizacionDatosCampaña.Models;
using ActualizacionDatosCampaña.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ActualizacionDatosCampaña.Controllers
{
    public class DatosController : Controller
    {
       
        [HttpGet]
        public ActionResult Index()
        {
            ConsultoraModel obj = new ConsultoraModel();

            obj.ListaTipoDocumento = ListaTipoDocumento();
            return View(obj);
        }

        [NonAction]
        private static SelectList ListaTipoDocumento()
        {
            List<TipoDocumentoModel> objLista = new List<TipoDocumentoModel>();
            objLista.Add(new TipoDocumentoModel { intTipoDocumento = 1, vchDocumento = "Código de Consultora" });
            objLista.Add(new TipoDocumentoModel { intTipoDocumento = 2, vchDocumento = "DNI " });

            SelectList ListaTipoDocumento = new SelectList(objLista, "intTipoDocumento", "vchDocumento");

            return ListaTipoDocumento;
        }

        [HttpPost]
        public ActionResult Index(ConsultoraModel modelo)
        {
            try
            {
                modelo.ListaTipoDocumento = ListaTipoDocumento();

                if (modelo.TipoDocumentoId == 0)
                {
                    ModelState.AddModelError("TipoDocumentoId", "Seleccione el Tipo de Documento");
                    return View(modelo);
                }

                if (modelo.TipoDocumentoId == 1)
                {
                    if (modelo.vchDato == null || modelo.vchDato.Length == 0)
                    {
                        ModelState.AddModelError("vchDato", "Ingrese código de consultora");
                        return View(modelo);
                    }


                }
                if (modelo.TipoDocumentoId == 2)
                {
                    if (modelo.vchDato == null || modelo.vchDato.Length == 0)
                    {
                        ModelState.AddModelError("vchDato", "Ingrese DNI");
                        return View(modelo);
                    }

                }
               
             


                if (modelo.bitTerminosCondiciones == true)
                {
                    //modelo.vchDato = "1110001880";

                    WSConsultoraModel ws = DAOConsultora.LlamadaWebService(modelo);

                    if (ws != null && ws.vchCodConsultora != null)
                    {
                        //if (ws.Participacion=="0")
                        //{
                        //    ModelState.AddModelError("vchDato", "* No Puede Participar");
                        //    return View(modelo);
                        //}
                        modelo.vchEmailAntiguo = ws.email;
                        modelo.vchTelefonoAntiguo = ws.telefonoMovil;
                        modelo.vchCodConsultora = ws.vchCodConsultora;
                        modelo.vchNombre = ws.nombreCompleto;

                    }
                    else
                    {
                        if (modelo.TipoDocumentoId == 1)
                        {
                           
                                ModelState.AddModelError("vchDato", "código de consultora no existe");
                                return View(modelo);
                            


                        }
                        if (modelo.TipoDocumentoId == 2)
                        {
                           
                                ModelState.AddModelError("vchDato", "DNI no existe");
                                return View(modelo);
                           

                        }
                    }
                  
                    


                    DAOConsultora.Busqueda_Consultora(ref modelo);

                    if (modelo.idPromocion == 0)
                    {
                        ModelState.AddModelError("vchDato","No se encuentra ninguna Campaña");
                        return View(modelo);

                    }
                 
                    Session["Model"] = modelo;
                    return RedirectToAction("Consultora", "Datos");
                   
                }
                else
                {
                    ModelState.AddModelError("vchDato", "Debe aceptar las Bases del concurso");
                    return View(modelo);
                }
            }

            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Datos", "Index"));
            }


        }





        [HttpGet]
        public ActionResult Consultora()
        {
            
       
             ConsultoraModel objConsultoraModel = (ConsultoraModel)Session["Model"];
            if (objConsultoraModel == null)
            {
                return RedirectToAction("Index", "Datos");
            }


           // DatoModel obj = new DatoModel();
            //obj.vchNombre = objConsultoraModel.vchNombre;
            //obj.idPromocion = objConsultoraModel.idPromocion;
            //obj.vchTipoCanal = objConsultoraModel.vchTipoCanal;
            //obj.vchCodConsultora = objConsultoraModel.vchCodConsultora;
            //obj.bitTerminosCondiciones = objConsultoraModel.bitTerminosCondiciones;

            //obj.vchEmailAntiguo = objConsultoraModel.vchEmailAntiguo;
            //obj.vchTelefonoAntiguo = objConsultoraModel.vchTelefonoAntiguo;


            return View(objConsultoraModel);
           // return View();
        }


        [HttpPost]
        public async Task<ActionResult> Consultora(ConsultoraModel modelo)
        {

            if (modelo.vchTelefono == null)
            {
                ModelState.AddModelError("vchTelefono", "Ingrese un número válido ");
                return View(modelo);
            }


            string telefono = modelo.vchTelefono.Replace(@"_","");
             telefono = telefono.Replace(@"-", "");

            if (telefono.Length!=9)
            {
                ModelState.AddModelError("vchTelefono", "Ingrese un número válido");
                return View(modelo);
            }
            if (telefono.Substring(0, 1)!= "9")
            {
                ModelState.AddModelError("vchTelefono", "Ingrese un número válido");
                return View(modelo); 
            }


            if (modelo.vchTelefono != null && modelo.vchTelefono.Length == 11)
            {
                ConsultoraModel objConsultoraModel = (ConsultoraModel)Session["Model"];
              
                DatoModel objDatoModel = new DatoModel();
                try
                {
                    // ConsultoraModel objConsultoraModel = (ConsultoraModel)Session["Model"];

                    //*******************************************************************
                    objDatoModel.vchEmailAntiguo = objConsultoraModel.vchEmailAntiguo;
                    objDatoModel.vchTelefonoAntiguo = objConsultoraModel.vchTelefonoAntiguo;
                    objDatoModel.idPromocion = objConsultoraModel.idPromocion;
                    objDatoModel.vchTipoCanal = objConsultoraModel.vchTipoCanal;
                    objDatoModel.bitTerminosCondiciones = objConsultoraModel.bitTerminosCondiciones;
                    objDatoModel.vchCodConsultora = objConsultoraModel.vchCodConsultora;

                    //*******************************************************************
                    //objDatoModel.idTipoDocumento = 1;
                    //objDatoModel.vchDato = modelo.vchDato;
                    //// objDatoModel.vchEmail = modelo.vchEmail;
                    //objDatoModel.vchTelefono = modelo.vchTelefono;
                    //objDatoModel.bitEnviado = true;
                    //objDatoModel.dtmFechaEnvio = DateTime.Now;
                    //objDatoModel.vchCodConsultora = modelo.vchCodConsultora;
                    //objDatoModel.vchEstado = "1";
                    //objDatoModel.idPromocion = modelo.idPromocion;

                    //objDatoModel.vchDato = modelo.vchDato;
                    objDatoModel.vchEmail = string.Empty;
                    objDatoModel.vchTelefono = modelo.vchTelefono.Replace("-", ""); ;
                    objDatoModel.bitEnviadoSMS = true;
                    objDatoModel.dtmFechaEnvioSMS = DateTime.Now;
                    objDatoModel.idTipoDocumento = objConsultoraModel.TipoDocumentoId;
                    objDatoModel.bitConfirmadoSMS = false;
                    // objDatoModel.dtmFechaConfirmadoSMS= DBNull.Value;
                    // objDatoModel.bitEnviadoEmail= modelo.bitEnviadoEmail;
                    //objDatoModel.dtmFechaEnviadoEmail= modelo.dtmFechaEnviadoEmail;
                    //objDatoModel.bitConfirmadoEmail= modelo.bitConfirmadoEmail;
                    // objDatoModel.dtmFechaConfirmadoEmail= modelo.dtmFechaConfirmadoEmail;
                    //objDatoModel.vchCodConsultora = modelo.vchCodConsultora;
                    objDatoModel.vchEstado = "1";
                    //objDatoModel.idPromocion = modelo.idPromocion;
                    //objDatoModel.vchTipoCanal = modelo.vchTipoCanal;
                    //objDatoModel.bitTerminosCondiciones = modelo.bitTerminosCondiciones;


                     Boolean respuesta=   DAODato.Add(ref objDatoModel);

                    //*******************************************************************

                    if (respuesta)
                    {
                        objDatoModel.vchEncriptadoSMS = Encriptacion.Base64Encode(objDatoModel.idDato + "," + "1") +"a";
                        await Helper.EnvioSMSAsync(objDatoModel);

                        //*******************************************************************

                        return RedirectToAction("Confirmacion", "Datos");
                    }
                    else
                    {
                        return View();
                    }
                }


                catch (Exception ex)
                {
                    Log.Error(ex.StackTrace + "-----" + ex.Message);
                    return View();
                }
            }

            ModelState.AddModelError("vchTelefono", "Ingrese un número válido");
            return View(modelo);
        }


        [HttpGet]
        public ActionResult Confirmacion()
        {

            return View();
        }

        [HttpGet]
        public ActionResult prueba()
        {
            try
            {
                var n = 1;
                var p = 0;

                Int32 x = n / p;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "EmployeeInfo", "Create"));
            }

            
        }

    }
}