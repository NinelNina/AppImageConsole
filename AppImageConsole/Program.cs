using System;
using static System.Console;
using ImageMagick;
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
         

            if (!(path.EndsWith(".jpg") | path.EndsWith(".png")  | path.EndsWith(".bmp")))
            {
                WriteLine("Неверный формат файла.");

                ReadKey();
                return;
            }

            var imageInput = new MagickImage(path);

            imageInput.ColorSpace = ColorSpace.Gray;
            
            fName = Path.GetFileName(path);
            fExt = fName.Split('.')[1];
            fName = fName.Split('.')[0];
            fName = fName + "-result." + fExt;

            string tmp = Path.GetFileName(path);
            fExt = path.Substring(0, path.Length - tmp.Length) + fName;

            imageInput.Write(fExt);

            WriteLine("Файл преобразован в " + fExt);

            ReadKey();
        }
    }
}
