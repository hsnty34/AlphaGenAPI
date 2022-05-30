using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlphaGenAPI.Tools
{
    public class CryptographyService
    {
        public static string Encrypt(string plainText)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                string key = "AAECAwQFBgcICQoLDA0ODw==";
                string iv = "AAECAwQFBgcICQoLDA0ODw==";
                ICryptoTransform encryptor = aes.CreateEncryptor(Convert.FromBase64String(key), Convert.FromBase64String(iv));
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        public static string Decrypt(string cipherText)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                string key = "AAECAwQFBgcICQoLDA0ODw==";
                string iv = "AAECAwQFBgcICQoLDA0ODw==";
                ICryptoTransform decryptor = aes.CreateDecryptor(Convert.FromBase64String(key), Convert.FromBase64String(iv));
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))

                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            plaintext = reader.ReadToEnd();

                        }
                    }
                }
                return plaintext;

            }
        }

    }
}

