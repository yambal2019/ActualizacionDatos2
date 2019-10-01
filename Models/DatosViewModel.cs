using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ActualizacionDatosCampaña.Models
{
    public class DatosViewModel: DatoModel
    {
     
        public Int32 TipoDocumentoId { get; set; }

        public SelectList ListaTipoDocumento { get; set; }

    }
}