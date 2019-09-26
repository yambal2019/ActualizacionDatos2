using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActualizacionDatosCampaña.Models
{
    public class UserModel
    {
        public Int32 UserId { get; set; }
        public String UserName { get; set; }
        
        [Required(ErrorMessage = "Ingrese su Contraseña.")]
        public String Password { get; set; }
        [Required(ErrorMessage = "Ingrese su Usuario.")]
        public String User { get; set; }
   
    }
}