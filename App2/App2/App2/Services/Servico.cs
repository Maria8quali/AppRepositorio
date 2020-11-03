using App2.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{
   public class Servico
    {

        public async Task SendAsync(Ocorrencias ocorrencias)
        {
            string URI = "http://homologacao.8quali.com.br/api/importacaoOcorrencia";
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format(URI, string.Empty));

            string json = JsonConvert.SerializeObject(ocorrencias);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = client.PostAsync(uri, content).Result;


            //response =  client.GetAsync(uri.ToString());
            if (response.IsSuccessStatusCode)
            {
                string content1 = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(@"\Ocorrencia successfully saved.");
            }
        }
    }
}
