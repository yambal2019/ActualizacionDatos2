using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualizacionDatosCampaña.Models
{
    public class DatoModel
    {
        public Int32 idDato
        {
            get; set;
        }

        public Int32? idTipoDocumento
        {
            get; set;
        }


        public Int32 TipoEnvio
        {
            get; set;
        }
        public string vchDato
        {
            get; set;
        }

        public string vchEmail
        {
            get; set;
        }

        public string vchTelefono
        {
            get; set;
        }

        public bool? bitEnviado
        {
            get; set;
        }

        public DateTime? dtmFechaEnvio
        {
            get; set;
        }

        public bool? bitConfirmadoSMS
        {
            get; set;
        }

        public DateTime? dtmFechaConfirmadoSMS
        {
            get; set;
        }

        public bool? bitConfirmadoEmail
        {
            get; set;
        }

        public DateTime? dtmFechaConfirmadoEmail { get; set; }
  
        public String vchCodConsultora { get; set; }
       
        public string vchEstado { get; set; }
       
        public Int32? idPromocion { get; set; }
        
        public string vchNombre { get; set; }
      
        public string vchEncriptadoSMS { get; set; }
        public string vchEncriptadoEmail { get; set; }
        
        public string vchRuta { get; set; }
        public Boolean bitEnviadoSMS { get; set; }
        public DateTime dtmFechaEnvioSMS { get; set; }
        public Boolean bitEnviadoEmail { get; set; }
        public DateTime dtmFechaEnviadoEmail { get; set; }
    }
}