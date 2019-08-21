using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualizacionDatosCampaña.Models
{
    public class ConsultoraModel
    {
        public Int32 intDato { get; set; }
        public Int32 idPromocion { get; set; }

        public String vchCodConsultora { get; set; }
        public String vchDato { get; set; }
        public String vchNombre { get; set; }
        public String vchEmail { get; set; }
        public String vchTelefono { get; set; }
        public Boolean bitTerminosCondiciones { get; set; }
        //++++++++++++++++++++++++++++++++++++++++++++++++++
        public String vchEncriptadoSMS { get; set; }
        public String vchEncriptadoEmail { get; set; }
        public String vchDesencriptado { get; set; }
        public String vchDesencriptadoSMS { get; set; }
        //++++++++++++++++++++++++++++++++++++++++++++++++++
        public String texto { get; set; }
        public Int32 Exito { get; set; }
        //++++++++++++++++++++++++++++++++++++++++++++++++++

    }
}