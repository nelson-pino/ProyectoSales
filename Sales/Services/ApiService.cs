using Newtonsoft.Json;
using Plugin.Connectivity;
using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Services
{
    public class ApiService
    {
        public async Task<Response> CheckConnection() 
        {
            if (!CrossConnectivity.Current.IsConnected) 
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Modo Avion Activado o no tiene encendido el Internet en el setting de su dispositivo."
                };
            }
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable) 
            {
                return new Response
                {
                    IsSuccess = false,
                    Message ="No Hay Conexion a Internet."
                };
            }
            return new Response
            {
                IsSuccess = true
            };
        }
        public async Task<Response> GetList<T>(string UrlBase, string Prefix, string Controller) 
        {
            try
            {
                var Client = new HttpClient
                {
                    BaseAddress = new Uri(UrlBase)
                };
                var url = $"{Prefix}{Controller}";
                var response = await Client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer
                    };
                }
                var List = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = List
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };  
            }
        }
    }
}
