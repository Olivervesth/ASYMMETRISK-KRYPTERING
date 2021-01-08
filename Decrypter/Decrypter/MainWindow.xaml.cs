using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Xml;

namespace Decrypter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Decrypt crypt = new Decrypt();
        string keypath;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Filefinder(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                keypath = openFileDialog.FileName;
                Moduluspath.Text = keypath;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(keypath);
            XmlNodeList xnList = doc.SelectNodes("RSAKeyValue");
            foreach (XmlNode xn in xnList)
            {
                Exponenttext.Text = xn["Exponent"].InnerText;
                Modulustext.Text = xn["Modulus"].InnerText;
                Dtext.Text = xn["D"].InnerText;
                DPtext.Text = xn["DP"].InnerText;
                DQtext.Text = xn["DQ"].InnerText;
                inversQtext.Text = xn["InverseQ"].InnerText;
                Ptext.Text = xn["P"].InnerText;
                Qtext.Text = xn["Q"].InnerText;
            }
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
           Decryptedtext.Text = Encoding.UTF8.GetString(crypt.DecryptData(keypath, Convert.FromBase64String(chipertext.Text)));
        }

        private void Generatenewkeys(object sender, RoutedEventArgs e)
        {
            string KeyPathprivate = "";
            string KeyPathpublic = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                KeyPathprivate = dialog.FileName;
                KeyPathpublic = dialog.FileName;
            }
            KeyPathprivate += "\\Privatekey.Xml";
            KeyPathpublic += "\\Publickey.Xml";

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            rsa.PersistKeyInCsp = false;
            File.WriteAllText(KeyPathpublic, rsa.ToXmlString(false));
            File.WriteAllText(KeyPathprivate, rsa.ToXmlString(true));
        }
    }
}
