using FINCORE.WEB.Helpers.Extensions;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace FINCORE.WEB.Helpers
{
    public class QRCodeGenerator
    {
        /// <summary>
        /// Will returning Base64String of Image PNG QRCode
        /// you can consume that String into Image element such as <img></img>
        /// </summary>
        /// <param name="identityText">your uniq key for QRCode identity</param>
        /// <returns>String Base64</returns>
        public static string CreateBase64QRCode(string identityText)
        {
            QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(identityText, QRCoder.QRCodeGenerator.ECCLevel.Q);
            QRCode qRCoded = new QRCode(qRCodeData);
            Bitmap qrBitmap = qRCoded.GetGraphic(60);
            byte[] BitmapArray = qrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}"
                , Convert.ToBase64String(BitmapArray));

            return QrUri;
        }
    }
}
