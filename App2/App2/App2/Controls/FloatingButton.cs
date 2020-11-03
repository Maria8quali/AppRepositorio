using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App2.Controls
{
    public class FloatingButton:Button
    {
        public FloatingButton()
        {
            BackgroundColor = Color.FromHex("597FAC");
            CornerRadius = 100;
            HeightRequest = 60;
            WidthRequest = 60;
            
            FontSize = 20;
            TextColor = Color.White;
            FontAttributes = FontAttributes.Bold;
        }
    }
}
