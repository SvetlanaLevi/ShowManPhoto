using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace ShowManPhoto.Repositories
{
    public class Servises
    {
        /// <summary>
        /// Метод, подготавливающий поверхность для рисования GDI+
        /// </summary>
        /// <returns>Возвращает объект Graphics</returns>
        private static Graphics GdiBase( Image image, ref Bitmap bitmap, out int imageWidth, out int imageHeight)
        {
            // Получить изображение, его ширину и высоту, преобразовать в объект Bitmap
            imageWidth = image.Width;
            imageHeight = image.Height;

            bitmap = new Bitmap(imageWidth, imageHeight, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            // Базовый класс GDI+, создающий слой для рисования
            Graphics graphics = Graphics.FromImage(bitmap);

            // Рисуем картинку
            graphics.DrawImage(
                image,
                new Rectangle(0, 0, imageWidth, imageHeight),
                0,
                0,
                imageWidth,
                imageHeight,
                GraphicsUnit.Pixel);

            return graphics;
        }

        /// <summary>
        /// Метод для добавления текстовой подписи
        /// </summary>
        /// <param name="image">Файл загруженной картинки</param>
        /// <param name="text">Текст подписи</param>
        /// <param name="pathSave">Путь для сохранения картинки</param>
        /// <param name="fontName">Шрифт текста подписи (по умолчанию Arial)</param>
        public static void SetWatermark(Image image, string text, string pathSave, string fontName = "arial")
        {
            // Поолучаем объект Graphics
            Bitmap bitmap = null;
            int imageWidth = 0, imageHeight = 0;

            using (Graphics graphics = GdiBase(image, ref bitmap, out imageWidth, out imageHeight))
            {
                // Задаем качество рендеринга для картинки
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Подбираем размер шрифта, чтобы подпись полность помещалась на картинке
                int[] fontSizes = new int[] { 64, 32, 16 };
                Font font = null;
                SizeF size = new SizeF();
                for (int i = 0; i < 7; i++)
                {
                    font = new Font(fontName, fontSizes[i], FontStyle.Bold);
                    size = graphics.MeasureString(text, font);

                    if ((ushort)size.Width < (ushort)imageWidth)
                        break;
                }

                // Добавляем смещение 5% относительно низа экрана и выравниваем по центру
                int yPixelsFromBottom = (int)(imageHeight * 0.1);
                float positionY = ((imageHeight -
                            yPixelsFromBottom) - (size.Height / 2));
                float centerX = (imageWidth / 2);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;

                // Полупрозрачная кисть черного цвета для обводки текста
                SolidBrush brush2 = new SolidBrush(Color.FromArgb(152, 0, 0, 0));

                graphics.DrawString(text,
                    font,
                    brush2,
                    new PointF(centerX + 1, positionY + 1),
                    stringFormat);

                // Полупрозрачная кисть белого цвета для заливки текста
                SolidBrush brush = new SolidBrush(
                                Color.FromArgb(153, 255, 255, 255));

                graphics.DrawString(text,
                    font,
                    brush,
                    new PointF(centerX, positionY),
                    stringFormat);

                // Сохранить картинку
                bitmap.Save(pathSave, ImageFormat.Jpeg);
            }
        }
    }
}
