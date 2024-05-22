using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// 二维码和条形码
    /// </summary>
    public class CodeHelper
    {
        /// <summary>
        /// 生成条形码返回Bitmap
        /// </summary>
        /// <param name="message">消息字符串</param>
        /// <param name="width">宽度px</param>
        /// <param name="height">长度px</param>
        /// <returns></returns>
        public static Bitmap CreateBarCodeBitmap(string message, int width, int height)
        {
            if (string.IsNullOrWhiteSpace(message))
                return null;

            if (width < 1 || height < 1)
                width = height = 40;

            BarcodeWriter writer = new BarcodeWriter()
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = width,
                    Width = height,
                    PureBarcode = true,
                    Margin = 10,
                },
            };

            return writer.Write(message);
        }

        /// <summary>
        /// 生成二维码返回Bitmap
        /// </summary>
        /// <param name="message">消息字符串</param>
        /// <param name="width">宽度px</param>
        /// <param name="height">长度px</param>
        /// <param name="isCutWhite">是否剪除白边</param>
        /// <returns></returns>
        public static Bitmap CreateQRCodeBitmap(string message, int width, int height, bool isCutWhite = false)
        {
            if (string.IsNullOrWhiteSpace(message))
                return null;

            if (width < 1 || height < 1)
                width = height = 100;

            if (width > 10000 || height > 10000)
                width = height = 1000;

            if (isCutWhite)
            {
                width += (int)(width * 0.39);
                height += (int)(height * 0.39);
            }
            var matrix = new MultiFormatWriter().encode(message, BarcodeFormat.QR_CODE, width, height);
            matrix = CutWhiteBorder(matrix);

            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,//设置内容编码
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
                Margin = 1//设置二维码的边距,单位不是固定像素
            };
            writer.Options = options;

            Bitmap bitmap;
            if (isCutWhite)
            {
                writer.Options.Width = matrix.Width;
                writer.Options.Height = matrix.Height;
                bitmap = writer.Write(matrix);
            }
            else
                bitmap = writer.Write(message);

            return bitmap;
        }

        /// <summary>
        /// 切除二维码白边
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private static BitMatrix CutWhiteBorder(BitMatrix matrix)
        {
            int[] rec = matrix.getEnclosingRectangle();
            int resWidth = rec[2] + 1;
            int resHeight = rec[3] + 1;
            BitMatrix resMatrix = new BitMatrix(resWidth + 1, resHeight + 1);
            resMatrix.clear();
            for (int i = 0; i < resWidth; i++)
            {
                for (int j = 0; j < resHeight; j++)
                {
                    if (matrix[i + rec[0], j + rec[1]])
                    {
                        resMatrix.flip(i + 1, j + 1);
                    }
                }
            }
            return resMatrix;
        }

        ///// <summary>
        ///// 生成带Logo的二维码
        ///// </summary>
        ///// <param name="text">内容</param>
        ///// <param name="width">宽度</param>
        ///// <param name="height">高度</param>
        //public static Bitmap Generate3(string text, int width, int height)
        //{
        //    //Logo 图片
        //    string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\img\logo.png";
        //    Bitmap logo = new Bitmap(logoPath);
        //    //构造二维码写码器
        //    MultiFormatWriter writer = new MultiFormatWriter();
        //    Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
        //    hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
        //    hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
        //    //hint.Add(EncodeHintType.MARGIN, 2);//旧版本不起作用，需要手动去除白边

        //    //生成二维码
        //    BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, width + 30, height + 30, hint);
        //    bm = deleteWhite(bm);
        //    BarcodeWriter barcodeWriter = new BarcodeWriter();
        //    Bitmap map = barcodeWriter.Write(bm);

        //    //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
        //    int[] rectangle = bm.getEnclosingRectangle();

        //    //计算插入图片的大小和位置
        //    int middleW = Math.Min((int)(rectangle[2] / 3), logo.Width);
        //    int middleH = Math.Min((int)(rectangle[3] / 3), logo.Height);
        //    int middleL = (map.Width - middleW) / 2;
        //    int middleT = (map.Height - middleH) / 2;

        //    Bitmap bmpimg = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
        //    using (Graphics g = Graphics.FromImage(bmpimg))
        //    {
        //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        g.DrawImage(map, 0, 0, width, height);
        //        //白底将二维码插入图片
        //        g.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
        //        g.DrawImage(logo, middleL, middleT, middleW, middleH);
        //    }
        //    return bmpimg;
        //}

    }
}
