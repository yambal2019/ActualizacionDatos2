using ActualizacionDatosCampaña.Data;
using ActualizacionDatosCampaña.Models;
using ActualizacionDatosCampaña.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualizacionDatosCampaña.Controllers
{
    public class DatosEmailController : Controller
    {
        [HttpGet]
        public ActionResult Index(string id)
        {

            try
            {



                string parametro = Convert.ToString(id);

                parametro = parametro.Substring(0, parametro.Length - 1);

                String Clave = Encriptacion.Base64Decode(parametro);
                string[] lista = Clave.Split(',');
                DatoModel objModel = new DatoModel();
                objModel.idDato = Convert.ToInt32(lista[0]);

                //objModel.vchCodConsultora = Convert.ToString(lista[2]);
                //objModel.idPromocion = Convert.ToInt32(lista[3]);

                if (lista[1] == "1") //SMS
                {
                    objModel.TipoEnvio = 1;
                    objModel.bitConfirmadoSMS = true;
                    objModel.dtmFechaConfirmadoSMS = DateTime.Now;
                    objModel.vchEstado = "2";

                    DAODato.Update(objModel);
    
                }

                HttpCookie cookie = new HttpCookie("Cookie");
                cookie.Value = objModel.idDato.ToString();

                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            }
            catch (Exception ex)
            {

                // throw ex;
                return View("Error", new HandleErrorInfo(ex, "DatosEmail", "Index"));
            }

            return View();
        }
        [HttpPost]
        public ActionResult Index()
        {


            return View("Email");
        }

        [HttpGet]
        public ActionResult Email()
        {
            DatoModel objModel = new DatoModel();

            return View(objModel);
        }
        [HttpPost]
        public ActionResult Email(DatoModel objModel)
        {


            if (objModel.vchEmail != null)
            {
              

                if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie"))
                {
                    objModel.idDato = Convert.ToInt32(this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value);
                    
                }

                objModel.TipoEnvio = 2;
                objModel.bitConfirmadoEmail = true;
                objModel.dtmFechaConfirmadoEmail = DateTime.Now;
                objModel.vchEncriptadoEmail = Encriptacion.Base64Encode(objModel.idDato + "," + "2" ) + "a";
                objModel.vchEstado = "3";
                DAODato.Update(objModel);


                objModel.vchRuta = Server.MapPath("~/Views/HtmlTemplates/HPRespuestaPedido.html");
                Helper.EnvioEmail(objModel);
            }

            return View("Final");
        }
        [HttpGet]
        public ActionResult Final()
        {
            
            return View();
        }
    }
}