using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Xml;

namespace Encrypter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Encrypt crypt = new Encrypt();
        string keypath = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FindPublicKeybtn(object sender, RoutedEventArgs e)//Finds the key gets the path and shows the information
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
            }
        }

        private void Encrypt(object sender, RoutedEventArgs e)//Calling the encryption methods and returns the chipertext
        {
             Chipertext.Text = Convert.ToBase64String(crypt.StartEncrypt(keypath,Encoding.UTF8.GetBytes(PlainText.Text)));
        }
    }
}
