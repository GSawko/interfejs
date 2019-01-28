using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Service
{
    public static class PhotoService
    {
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);

            return returnImage;
        }

        public static Image LoadFileToImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Otwórz plik ze zwdjęciem";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Equals(""))
                return null;

            Image image = Image.FromFile(openFileDialog.FileName);

            return image;
        }
    }
}
