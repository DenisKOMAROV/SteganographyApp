using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SteganographyApp
{
    public partial class SteganographyApp : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();

        const string Filter = "Image Files (*.png) | *.png";
        const string FilterForText = "Text Files (*.txt) | *.txt";

        string textToHide = "";

        public SteganographyApp()
        {
            InitializeComponent();
        }

        //Открытие исходного изображения
        private void btnOpenBaseImage_Click(object sender, EventArgs e)
        {
            ofd.Filter = Filter;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbBaseImage.Image = new Bitmap(ofd.FileName);
            }
        }

        //Открытие текстового файла
        private void btnOpenTextFile_Click(object sender, EventArgs e)
        {
            ofd.Filter = FilterForText;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName.Text = ofd.SafeFileName; //показывает только имя файла

                textToHide = ByteArrayToBinary(File.ReadAllBytes(ofd.FileName));
            }
        }

        //Сокрытие текста в изображении
        private void btnHideText_Click(object sender, EventArgs e)
        {
            if (pbBaseImage == null)
            {
                return;
            }

            if (textToHide == String.Empty)
            {
                return;
            }

            int dataToWrite = 0;
            char[] data = textToHide.ToCharArray();

            // Длина сообщения
            int messageLength = 0;

            // Двоичная длина сообщения
            char[] messageLengthBinary = Convert.ToString(data.Length, 2).PadLeft(24, '0').ToCharArray();

            // Записываем длину данных в последние 6 пискселей изображения. Макс длина 16777215 диапазон без знака
            int MaxDataLenght = 16777215;
            if (data.Length > MaxDataLenght)
            {
                MessageBox.Show("Размер текстового файла слишком большой");
                return;
            }

            // Изоражение в которое записывается сообщение
            var stegoImage = (Bitmap)pbBaseImage.Image;

            //  Если сообщение слиишком большое, чтобы его спрятать в изображение
            // -6 тк последние 6 пикселей зарезервированы под длину сообщения
            if ((data.Length / 4) > (stegoImage.Width * stegoImage.Height) - 6)
                return;

            // Используется при замене пикселей в изображении
            var newPixel = new Pixel();

            for (int x = 0; x < stegoImage.Width; x++)
            {
                for (int y = 0; y < stegoImage.Height; y++)
                {
                    var currPixel = stegoImage.GetPixel(x, y);
                    newPixel.A = currPixel.A;
                    newPixel.R = currPixel.R;
                    newPixel.G = currPixel.G;
                    newPixel.B = currPixel.B;

                    if (ProcessingImageLastSixPixels(stegoImage, x, y))
                    {
                        newPixel.A = SetPixelChannel(currPixel.A, messageLengthBinary, ref messageLength);
                        newPixel.R = SetPixelChannel(currPixel.R, messageLengthBinary, ref messageLength);
                        newPixel.G = SetPixelChannel(currPixel.G, messageLengthBinary, ref messageLength);
                        newPixel.B = SetPixelChannel(currPixel.B, messageLengthBinary, ref messageLength);
                    }
                    else if (dataToWrite < data.Length)
                    {
                        newPixel.A = SetPixelChannel(currPixel.A, data, ref dataToWrite);
                        newPixel.R = SetPixelChannel(currPixel.R, data, ref dataToWrite);
                        newPixel.G = SetPixelChannel(currPixel.G, data, ref dataToWrite);
                        newPixel.B = SetPixelChannel(currPixel.B, data, ref dataToWrite);
                    }

                    stegoImage.SetPixel(x, y, Color.FromArgb(newPixel.A, newPixel.R, newPixel.G, newPixel.B));
                }
            }

            // Сохранение файла
            sfd.Filter = Filter;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                stegoImage.Save(sfd.FileName);
            }
        }

        // Возвращает пиксели с длиной сообщения
        private static bool ProcessingImageLastSixPixels(Bitmap img, int x, int y)
        {
            return x > (img.Width - 7) && y > (img.Height - 2);
        }

        // Хранит по одному биту в канале пикселя (4 канала: Alpha, Red, Green, Blue)
        private static int SetPixelChannel(byte currPixelChannel, char[] data, ref int messageWrite)
        {
            int newPixelChannel;

            // Если текущий канал пикселя нечетный
            if (currPixelChannel % 2 == 1)
            {
                // Если нужно записать 1
                if (data[messageWrite++] == '1')
                {
                    // сохраняем Alpha
                    newPixelChannel = currPixelChannel;
                }
                else // Если нужно записать 0
                {
                    // изменяем Alpha на 1 и сохраняем
                    newPixelChannel = currPixelChannel - 1;
                }
            }
            else // Если текущий канал пикселя четный
            {
                // Если нужно записать 1
                if (data[messageWrite++] == '1')
                {
                    // изменяем Alpha на 1 и сохраняем
                    newPixelChannel = currPixelChannel + 1;
                }
                else // Если нужно записать 0
                {
                    // изменяем Alpha на 1 и сохраняем
                    newPixelChannel = currPixelChannel;
                }
            }

            return newPixelChannel;
        }

        //Восстановление текста из изображения
        private void btnRecoverText_Click(object sender, EventArgs e)
        {
            try
            {
                if (pbBaseImage.Image == null)
                {
                    MessageBox.Show("Загрузите изображение");
                    return;
                }


                var image = (Bitmap)pbBaseImage.Image;
                var bitStream = "";

                // Получаем длину сообщения
                for (int x = image.Width - 6; x < image.Width; x++)
                {
                    int y = image.Height - 1;
                    var currPixel = image.GetPixel(x, y);

                    bitStream = GetNybbleFromPixelChannels(currPixel, bitStream);
                }

                int dataLen = Convert.ToInt32(bitStream, 2);
                int dataCtr = 0;
                bitStream = "";

                // Получаем сообщение из изображения
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var currPixel = image.GetPixel(x, y);

                        bitStream = GetNybbleFromPixelChannels(currPixel, bitStream);

                        dataCtr++;

                        if (dataCtr > ((dataLen / 4) - 1))
                        {
                            x = image.Width;
                            y = image.Height;
                        }
                    }
                }

                var hiddenImage = BinaryToByteArray(bitStream);

                sfd.Filter = FilterForText;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, hiddenImage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при декодировании. Проверьте правильность выбранного файла.");
                return;
            }
        }
    
        private static string GetNybbleFromPixelChannels(Color currPixel, string bitStream)
        {
            bitStream += currPixel.A % 2;
            bitStream += currPixel.R % 2;
            bitStream += currPixel.G % 2;
            bitStream += currPixel.B % 2;
            return bitStream;
        }
        
        // Преобразование в массив байтов
        public static byte[] BinaryToByteArray(string data)
        {
            var bytes = new byte[data.Length / 8];
            int index = 0;

            for (int i = 0; i < data.Length; i += 8)
            {
                bytes[index++] = Convert.ToByte(data.Substring(i, 8), 2);
            }

            return bytes;
        }
        
        // Преобразование байтового массива в бинарный
        public static string ByteArrayToBinary(byte[] data)
        {
            var buf = new StringBuilder();

            foreach (var b in data)
            {
                var binaryStr = Convert.ToString(b, 2);
                var padStr = binaryStr.PadLeft(8, '0');
                buf.Append(padStr);
            }

            return buf.ToString();
        }
        
    }
}
