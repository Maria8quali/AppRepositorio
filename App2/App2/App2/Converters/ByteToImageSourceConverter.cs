using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace App2.Converters
{
    public class ByteToImageSourceConverter : IValueConverter
    {
        //convert to image stream
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource objImageSource;
            string filePath = (string)value;
            

            if (value != null)
            {
                byte[] byteImageData = GetPhoto(filePath);
                objImageSource = ImageSource.FromStream(() => new MemoryStream(byteImageData));
            }
            else
            {
                objImageSource = null;
            }
            return objImageSource;

        }
        public static byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
