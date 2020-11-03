using App2.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2.Services
{
    public class PageService : IPageService
    {
        public Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            throw new NotImplementedException();
        }

        public Task DisplayAlert(string title, string message, string ok)
        {
            throw new NotImplementedException();
        }

        public async Task<Page> PopAsync()
        {

            throw new NotImplementedException();
        }

        public Task PushAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }
    }
}
