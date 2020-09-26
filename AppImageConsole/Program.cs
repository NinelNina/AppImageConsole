using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AppImageConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string path, fName, fExt;
            
            if (args.Length == 0)
            {
               WriteLine("Пожалуйста, введите полный путь к изображению.");
                
                path = ReadLine();
            }
            else
            {
                path = Convert.ToString(args[0]);
            }
            try
            {
                var imageInput0 = new Bitmap(Image.FromFile(path));
            }
            catch
            {
                WriteLine("Ошибка! Файл неподходящего типа.");
                ReadKey();
                
                return;
            }
            var imageInput = new Bitmap(Image.FromFile(path));
            var imageOutput = new Bitmap(imageInput.Width, imageInput.Height);

            if (!(path.EndsWith(".jpg") | path.EndsWith(".png")  | path.EndsWith(".bmp")))
            {
                WriteLine("Неверный формат файла.");

                ReadKey();
                return;
            }

            for (int j = 0; j < imageInput.Height; j++)
            {
                for (int i = 0; i < imageInput.Width; i++)
                {
                    UInt32 pixel = (UInt32)(imageInput.GetPixel(i, j).ToArgb());

                    float R = (float)((pixel & 0x00FF0000) >> 16);
                    float G = (float)((pixel & 0x0000FF00) >> 8);
                    float B = (float)(pixel & 0x000000FF);

                    R = G = B = (R + G + B) / 3.0f;

                    UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);

                    imageOutput.SetPixel(i, j, Color.FromArgb((int)newPixel));
                }
            }

            fName = Path.GetFileName(path);
            fExt = fName.Split('.')[1];
            fName = fName.Split('.')[0];
            fName = fName + "-result." + fExt;

            string tmp = Path.GetFileName(path);
            fExt = path.Substring(0, path.Length - tmp.Length) + fName;

            if (path.EndsWith(".jpg") == true)
            {
                imageOutput.Save(fExt, ImageFormat.Jpeg);
            }
            else if (path.EndsWith(".png") == true)
            {
                imageOutput.Save(fExt, ImageFormat.Png);
            }
            else if (path.EndsWith(".bmp") == true)
            {
                imageOutput.Save(fExt, ImageFormat.Bmp);
            }

            WriteLine("Файл преобразован " + fExt);

            ReadKey();
        }
    }
}
