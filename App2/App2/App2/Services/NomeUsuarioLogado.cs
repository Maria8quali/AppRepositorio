using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{
    public class NomeUsuarioLogado
    {

        public async Task<string> GetName(string emailuser)
        {
            try
            {
                string URI =  "http://homologacao.8quali.com.br/api/NomeUsuarioByLogin?login="+ emailuser;
                HttpClient client = new HttpClient();



                Uri uri = new Uri(string.Format(URI, string.Empty));



                HttpResponseMessage response = null;
                response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    string nomeUser = await response.Content.ReadAsStringAsync();

                    return nomeUser;

                }

            }
            catch (Exception e)
            {

            }
            return "";
        }
    }
}
