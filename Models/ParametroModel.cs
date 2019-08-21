using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualizacionDatosCampaña.Models
{
    public class ParametroModel
    {

        public Int32 idParametro { get; set; }
        public Int32? intTipo { get; set; }
        public Int32? intOrden { get; set; }
        public String vchCampo { get; set; }
        public String vchValor { get; set; }
        public String vchDescripcion { get; set; }
        public Boolean? bitEstado { get; set; }
    }
}