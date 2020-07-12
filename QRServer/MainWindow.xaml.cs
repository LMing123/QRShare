using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace QRServer
{
    public class QRModel
    {
       public BitmapImage Img { get; set; }
    }
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            var handle = new WindowInteropHelper(this).EnsureHandle();
            var acrylic = new AcrylicHelper(handle);
            acrylic.SetAcrylic();
            Init();
        }
        private void Init()
        {
            Task.Run( ()=> QRServerWeb.Program.CreateHostBuilder(null).Build().Run());

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(GetIPAdress(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var pic=qrCode.GetGraphic(20, System.Drawing.Color.Black, System.Drawing.Color.Azure,true);
            MemoryStream memoryStream = new MemoryStream();
            pic.Save(memoryStream, ImageFormat.Png);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            bitmap.Freeze();
            QRModel qRModel = new QRModel() { Img = bitmap };

            QR_Img.SetBinding(System.Windows.Controls.Image.SourceProperty, new Binding { Source = qRModel,Path=new PropertyPath("Img")});
          //  QR_Img.Source = bitmap;
        }
        private string GetIPAdress()
        {
            var hostName = Dns.GetHostName();
            IPHostEntry iPHostEntry = Dns.GetHostEntry(hostName);
            var ipAdress = iPHostEntry.AddressList.Where(x => !(x.IsIPv6LinkLocal || x.IsIPv6Multicast || x.IsIPv6SiteLocal || x.IsIPv6Teredo));
            return ipAdress.FirstOrDefault().ToString();
        }


    }
}
