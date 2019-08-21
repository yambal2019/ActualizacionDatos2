using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualizacionDatosCampaña.Utilities
{
    public class Encriptacion
    {
        public static string Base64Encode(string word)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(word);
            return Convert.ToBase64String(byt);
        }
        public static string Base64Decode(string word)
        {
            byte[] b = Convert.FromBase64String(word);
            return System.Text.Encoding.UTF8.GetString(b);
        }

    }
}