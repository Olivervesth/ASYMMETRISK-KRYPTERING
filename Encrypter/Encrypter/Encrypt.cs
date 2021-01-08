using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encrypter
{
    class Encrypt
    {

        public byte[] StartEncrypt(string publickeypath,byte[] data)//Does the encryption
        {
            byte[] chiper;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publickeypath));

                chiper = rsa.Encrypt(data,true);
            }
            return chiper;
        }
    }
}
