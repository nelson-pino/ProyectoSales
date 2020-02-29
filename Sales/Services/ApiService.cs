using Newtonsoft.Json;
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
