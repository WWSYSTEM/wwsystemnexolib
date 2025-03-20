using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WWsystemLib
{
    public class CryptoUtils
    {
        public static string Encrypt(string data, string key, string iv)
        {
            byte[] encryptedBytes;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = GetSHA256Hash(key);
                aesAlg.IV = GetSHA256Hash(iv).Take(16).ToArray();

                // Create an encryptor to perform the stream transform
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Write all data to the stream
                            swEncrypt.Write(data);
                        }
                    }

                    encryptedBytes = msEncrypt.ToArray();
                }
            }

            // Convert the encrypted bytes to a string and return
            return Convert.ToBase64String(encryptedBytes);
        }

        private static byte[] GetSHA256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }
        
        public static string Decrypt(string encryptedData, string key, string iv)
        {
            
            byte[] cipherText = Convert.FromBase64String(encryptedData);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetSHA256Hash(key);
                aesAlg.IV = GetSHA256Hash(iv).Take(16).ToArray();

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}