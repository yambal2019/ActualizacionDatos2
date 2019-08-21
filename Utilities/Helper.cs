using ActualizacionDatosCampaña.Data;
using ActualizacionDatosCampaña.Models;
using Infobip.Api.Client;
using Infobip.Api.Config;
using Infobip.Api.Model;
using Infobip.Api.Model.Sms.Mt.Send;
using Infobip.Api.Model.Sms.Mt.Send.Textual;
//using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Net.Http;
//using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;


namespace ActualizacionDatosCampaña.Utilities
{
    public class Helper
    {
        //static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
        static string key { get; set; } = "123456789abcdefg";

        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string TextoBD(string campo)
        {

            string valor = ConfigurationManager.AppSettings[campo];
            ParametroModel objParametroModel = DAOParametro.ParametroUnValor(valor);

            return objParametroModel.vchValor;

        }

        public static List<ParametroModel> ListaBD(string campo)
        {

            string valor = ConfigurationManager.AppSettings[campo];
             List <ParametroModel> objListaParametroModel = DAOParametro.ParametroLista(valor);

            return objListaParametroModel;

        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }


        public static Int32 EnvioEmail(DatoModel objModel)
        {

            //try
            //{

            List<ParametroModel> objListaParametro = ListaBD("EMAIL_CONFIGURACION");
           
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(objModel.vchRuta))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{fechaRegistro}", DateTime.Now.ToString());
            body = body.Replace("{Mensaje_Cuerpo_Email}", objListaParametro[4].vchValor);
          
            body = body.Replace("{Email}", objModel.vchEmail);
            body = body.Replace("{Link}", Helper.TextoBD("EMAIL_LINK") + objModel.vchEncriptadoEmail);



            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(objListaParametro[0].vchValor);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", objListaParametro[1].vchValor);
            var request = new MultipartFormDataContent();

            request.Add(new StringContent(objListaParametro[2].vchValor), "from");
            request.Add(new StringContent(objModel.vchEmail), "to");
            request.Add(new StringContent(objListaParametro[3].vchValor), "subject");
           // request.Add(new StringContent("Rich HTML message body."), "text");
            //request.Add(new StringContent("<h1>Html body</h1><p>Rich HTML message body.</p>" + "www.unique.com/datos/consultora?" + objModel.vchEncriptadoEmail), "html");
            request.Add(new StringContent(body), "html");



            var response = client.PostAsync("email/1/send", request).Result;
            if (response.IsSuccessStatusCode)
            {
                return 1;
                //var responseContent = response.Content;
                //string responseString = responseContent.ReadAsStringAsync().Result;
                ////Console.WriteLine(responseString);
            }
            else
            {
                return 0;
            }

            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

            //return 0;
        }


        public static async System.Threading.Tasks.Task EnvioSMSAsync(DatoModel objModel)
        {
            List<ParametroModel> objListaParametro = ListaBD("SMS_CONFIGURACION");

            //try
            //{
            string BASE_URL = objListaParametro[0].vchValor;

            string USERNAME = objListaParametro[1].vchValor;
            string PASSWORD = objListaParametro[2].vchValor;

            BasicAuthConfiguration BASIC_AUTH_CONFIGURATION = new BasicAuthConfiguration(BASE_URL, USERNAME, PASSWORD);

            string FROM = "InfoSMS";
            string TO = "+51" + objModel.vchTelefono;
            List<string> TO_LIST = new List<string>(1) { "PHONE" };

            string MESSAGE_TEXT = objListaParametro[3].vchValor + "  " + DateTime.Now.ToString() + " " + Helper.TextoBD("SMS_LINK") + objModel.vchEncriptadoSMS;

            SendMultipleTextualSmsAdvanced smsClient = new SendMultipleTextualSmsAdvanced(BASIC_AUTH_CONFIGURATION);

            Destination destination = new Destination
            {
                To = TO
            };

            Message message = new Message
            {
                From = FROM,
                Destinations = new List<Destination>(1) { destination },
                Text = MESSAGE_TEXT
            };

            SMSAdvancedTextualRequest request = new SMSAdvancedTextualRequest
            {
                Messages = new List<Message>(1) { message }
            };



            SMSResponse smsResponse = await smsClient.ExecuteAsync(request);
            SMSResponseDetails sentMessageInfo = smsResponse.Messages[0];

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            //try
            //{
            //    var client = new RestClient("https://mrxqw.api.infobip.com/sms/2/text/single");

            //    var request = new RestRequest(Method.POST);
            //    request.AddHeader("accept", "application/json");
            //    request.AddHeader("content-type", "application/json");
            //    request.AddHeader("authorization", "Basic VU5JUVVFX1lBTkJBTDpVbmlxdWUuMjAxOQ==");
            //    request.AddParameter("application/json", "{\"from\":\"InfoSMS\", \"to\":\"+51929867768\",\"text\":\"Test SMS.\"}", ParameterType.RequestBody);

            //    IRestResponse response = client.Execute(request);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}



            //}
            //catch (Exception ex)
            //{


            //}


        }




    //    public static void EnvioSMSAsync(ConsultoraModel objModel)
    //    {
    //        List<ParametroModel> objListaParametro = ListaBD("SMS_CONFIGURACION");

    //        //try
    //        //{
    //            string BASE_URL = objListaParametro[0].vchValor;

    //            string USERNAME = objListaParametro[1].vchValor;
    //            string PASSWORD = objListaParametro[2].vchValor;
    //             string FROM = "InfoSMS";

    //        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    //        try
    //        {
    //            var client = new RestClient("https://mrxqw.api.infobip.com/sms/2/text/single");

              
    //            string TO = "+51" + objModel.vchTelefono;
            
    //            string MESSAGE_TEXT = objListaParametro[3].vchValor + "  " + DateTime.Now.ToString() + " " + Helper.TextoBD("SMS_LINK") + objModel.vchEncriptadoSMS;

    //            var model = new Envio
    //            {
    //                from= FROM,
    //                to = TO,
    //                text = MESSAGE_TEXT
    //            };


    //            var request = new RestRequest(Method.POST);
    //            request.AddHeader("accept", "application/json");
    //            request.AddHeader("content-type", "application/json");
    //            request.AddHeader("authorization", "Basic VU5JUVVFX1lBTkJBTDpVbmlxdWUuMjAxOQ==");
    //            request.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(model), ParameterType.RequestBody);

    //            IRestResponse response = client.Execute(request);
    //        }
    //        catch (Exception ex)
    //        {

    //            throw;
    //        }
            


    //        //}
    //        //catch (Exception ex)
    //        //{


    //        //}


    //    }
    //}
    //public class Envio
    //{
    //    public string from { get; set; }
    //    public string to { get; set; }
    //    public string text { get; set; }
        
    }

}