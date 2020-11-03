using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{
  public  class UserKey
    {
        public async Task<int> chaveUsuario(string email)
        {


            try
            {
                string URI = "http://homologacao.8quali.com.br/api/chaveapi?login=" + email;
                HttpClient client = new HttpClient();



                Uri uri = new Uri(string.Format(URI, string.Empty));



                HttpResponseMessage response = null;
                response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    return Convert.ToInt32(content);

                }

            }
            catch (Exception e)
            {

            }
            return 0;
        }
    }
}
