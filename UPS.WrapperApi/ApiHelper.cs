using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UPS.WrapperApi
{
    public class ApiHelper
    {
        public  HttpResponseMessage GetAPI(string apiUrl)
        {
           

            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //request.Headers.Host = ConfigurationManager.AppSettings["host"];
                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                var client = new HttpClient(handler);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.Timeout = TimeSpan.FromSeconds(35);
              
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                var response =  client.Send(request);
                HttpStatusCode statusCode;
                statusCode = response.StatusCode;


                return response;


            }
            catch (Exception ex)
            {

                return null;
            }

        }

      
        public HttpResponseMessage PostAPI(string apiUrl, string contentData)
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Headers.Host = ConfigurationManager.AppSettings["host"];
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(contentData, Encoding.UTF8, "application/json");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                var client = new HttpClient(handler);
               
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                var response =  client.Send(request);
                HttpStatusCode statusCode;
                statusCode = response.StatusCode;

                return response;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public HttpResponseMessage DeleteUser(string apiUrl, int id)
        {
            try
            {

                string deletUri = apiUrl + id;

                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                var client = new HttpClient(handler);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                var response = client.DeleteAsync(deletUri);
                HttpStatusCode statusCode;
                statusCode = response.Result.StatusCode;

                return response.Result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public HttpResponseMessage UpdateEmployee(string apiUrl, string content)
        {
            try
            {

                string PutUri = apiUrl;

                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                var client = new HttpClient(handler);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = client.PutAsync(PutUri, httpContent);
                HttpStatusCode statusCode;
                statusCode = response.Result.StatusCode;

                return response.Result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }

}
