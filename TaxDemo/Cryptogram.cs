using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace TaxDemo
{
    /// <summary>
    /// 工具类，提供密码加密、连接字符串加密功能
    /// </summary>
    public class Cryptogram
    {
        private static readonly byte[] pKEY = { 218, 239, 227, 22, 31, 53, 120, 224, 223, 223, 171, 210, 140, 158, 47, 86, 122, 39, 238, 95, 47, 138, 44, 155 };
        private static readonly byte[] pIV = { 8, 7, 6, 5, 4, 3, 2, 1 };

        private static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        private static string strTmp = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        static Cryptogram()
        {
            //				des.Mode= System.Security.Cryptography.CipherMode.ECB ;
            //				des.Padding = System.Security.Cryptography.PaddingMode.PKCS7 ;
            des.Mode = System.Security.Cryptography.CipherMode.CBC;
            des.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
        }

        //加密
        /// <summary>
        /// 将字符串转化为UTF-8的ByteArray后，使用TripleDES算法对字符串进行加密，加密后的字符串再转化为Base64字符串。
        /// </summary>
        /// <param name="toEncryptStr">待加密的字符串</param>
        /// <returns>加密并转化为Base64后的字符串</returns>
        public static string CommonEncrypt(string toEncryptStr)
        {
            int ErrorNum = -1;
            string ErrorDesc = "";

            if (string.IsNullOrEmpty(toEncryptStr)) return string.Empty;
            try
            {
                byte[] Encrypted;

                if (Encrypt(pKEY, pIV, ConvertStringToByteArray(toEncryptStr), out Encrypted))
                {
                    return ToBase64String(Encrypted);
                }
                else
                    return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //解密
        /// <summary>
        /// 待解密的字符串。该字符串应该是先转化为UTF-8的ByteArray,然后用TripleDES加密，最后再用Base64加密的字符串。解密时按相反顺序进行解密。
        /// </summary>
        /// <param name="toDecryptStr">Base64（后） + TripleDES（先）加密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string CommonDecrypt(string toDecryptStr)
        {
            int ErrorNum = -1;
            string ErrorDesc = "";

            if (string.IsNullOrEmpty(toDecryptStr)) return string.Empty;
            try
            {
                byte[] Decrypted;

                if (Decrypt(pKEY, pIV, FromBase64String(toDecryptStr), out Decrypted))
                {
                    return ConvertByteArrayToString(Decrypted);
                }
                else
                    return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 生成一个用于TripleDES算法的密钥
        /// </summary>
        /// <returns>用于TripleDES算法的密钥</returns>
        public static byte[] GenerateKey()
        {
            int ErrorNum = -1;
            string ErrorDesc = "";

            byte[] buf = null;
            try
            {
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.GenerateKey();
                buf = tdes.Key;
            }
            catch (Exception e)
            {
            }
            return buf;
        }

        /// <summary>
        /// 生成一个用于TripleDES算法的向量
        /// </summary>
        /// <returns>用于TripleDES算法的密钥</returns>
        public static byte[] GenerateIV()
        {
            int ErrorNum = -1;
            string ErrorDesc = "";
            byte[] buf = null;
            try
            {
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.GenerateIV();
                buf = tdes.IV;
            }
            catch (Exception e)
            {
            }
            return buf;
        }

        /// <summary>
        /// 使用MD5算法对输入的字符串进行单向哈希
        /// </summary>
        /// <param name="KEY">准备进行哈希的字符串</param>
        /// <returns>使用MD5算法单向哈希转化后的KEY</returns>
        public static byte[] GenerateKey(string KEY)
        {
            int ErrorNum = -1;
            string ErrorDesc = "";

            byte[] buf = null;
            try
            {
                buf = new MD5CryptoServiceProvider().ComputeHash((new UTF8Encoding()).GetBytes(KEY));
            }
            catch (Exception e)
            {
            }
            return buf;
        }

        /// <summary>
        /// 使用TripleDES算法对明码Password进行加密。采用CommonEncrypt()方法进行加密。
        /// </summary>
        /// <param name="Password">未加密的Password</param>
        /// <returns>加密后的Password</returns>
        public static string EncryptPassword(string Password)
        {
            return CommonEncrypt(Password);
        }

        /// <summary>
        /// 使用TripleDES算法对加密的Password进行解密。采用CommonDecrypt()方法进行解密。
        /// </summary>
        /// <param name="Password">加密的字符串</param>
        /// <returns>解密后得到的字符串</returns>
        public static string DecryptPassword(string Password)
        {
            if (strTmp == string.Empty)
            {
                strTmp = CommonDecrypt(Password);
            }

            return strTmp;
        }

        /// <summary>
        /// 对输入的字符串进行MD5单向哈希后，再转化为Base64字符串表示。
        /// </summary>
        /// <param name="Password">准备进行转化的字符串</param>
        /// <returns>转化后得到的字符串</returns>
        public static string HashPassword(string Password)
        {
            if (Password == null || Password == "")
                return "";
            return Cryptogram.ToBase64String(GenerateKey(Password));
        }

        /// <summary>
        /// 使用指定的密钥和向量对一个byte数组进行TripleDES加密
        /// </summary>
        /// <param name="KEY">TripleDES密钥</param>
        /// <param name="IV">TripleDES向量</param>
        /// <param name="TobeEncrypted">准备进行TripleDES加密的byte数组</param>
        /// <param name="Encrypted">加密后得到的byte数组</param>
        /// <returns>如果加密成功就返回真，否则返回假。</returns>
        public static bool Encrypt(byte[] KEY, byte[] IV, byte[] TobeEncrypted, out  byte[] Encrypted)
        {
            int ErrorNum = -1;
            string ErrorDesc = "";
            if (KEY == null || IV == null)
                throw new Exception("ERROR");
            Encrypted = null;
            try
            {
                byte[] tmpiv = { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int ii = 0; ii < 8; ii++)
                {
                    tmpiv[ii] = IV[ii];
                }
                byte[] tmpkey = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int ii = 0; ii < 24; ii++)
                {
                    tmpkey[ii] = KEY[ii];
                }
                //tridesencrypt.Dispose();
                ICryptoTransform tridesencrypt = des.CreateEncryptor(tmpkey, tmpiv);
                //tridesencrypt = des.CreateEncryptor(KEY,tmpiv);
                Encrypted = tridesencrypt.TransformFinalBlock(TobeEncrypted, 0, TobeEncrypted.Length);
                //tridesencrypt.Dispose();
                des.Clear();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 使用指定的密钥和向量对一个byte数组进行TripleDES解密
        /// </summary>
        /// <param name="KEY">TripleDES密钥</param>
        /// <param name="IV">TripleDES向量</param>
        /// <param name="TobeEncrypted">准备进行TripleDES解密的byte数组</param>
        /// <param name="Encrypted">解密后得到的byte数组</param>
        /// <returns>如果解密成功就返回真，否则返回假。</returns>
        public static bool Decrypt(byte[] KEY, byte[] IV, byte[] TobeDecrypted, out  byte[] Decrypted)
        {
            int ErrorNum = -1;
            string ErrorDesc = "";

            Decrypted = null;
            try
            {
                byte[] tmpiv = { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int ii = 0; ii < 8; ii++)
                {
                    tmpiv[ii] = IV[ii];
                }
                byte[] tmpkey = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int ii = 0; ii < 24; ii++)
                {
                    tmpkey[ii] = KEY[ii];
                }
                ICryptoTransform tridesdecrypt = des.CreateDecryptor(tmpkey, tmpiv);
                Decrypted = tridesdecrypt.TransformFinalBlock(TobeDecrypted, 0, TobeDecrypted.Length);
                des.Clear();
            }
            catch (Exception e)
            {
            }
            return true;
        }

        /// <summary>
        /// 将字符串用UTF-8编码规则转化为byte数组后进行SHA-1单向哈希，然后再进行Base64加密。
        /// </summary>
        /// <param name="s">待转化的字符串</param>
        /// <returns>转化后得到的字符串</returns>
        public static string ComputeHashString(string s)
        {
            return ToBase64String(ComputeHash(ConvertStringToByteArray(s)));
        }

        /// <summary>
        /// 使用SHA1哈希算法对byte数组进行单向哈希。
        /// </summary>
        /// <param name="buf">准备进行哈希的byte数组</param>
        /// <returns>使用SHA1算法进行单向哈希后得到的byte数组</returns>
        public static byte[] ComputeHash(byte[] buf)
        {
            return ((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(buf);
        }

        /// <summary>
        /// 将byte数组转化为用Base64表示的字符串
        /// </summary>
        /// <param name="buf">准备进行转化的byte数组</param>
        /// <returns>用Base64进行转化后得到的字符串</returns>
        public static string ToBase64String(byte[] buf)
        {
            return System.Convert.ToBase64String(buf);
        }

        /// <summary>
        /// 将Base64表示的字符串解密为byte数组
        /// </summary>
        /// <param name="s">使用Base64加密的字符串</param>
        /// <returns>进行Base64解密后得到的byte数组</returns>
        public static byte[] FromBase64String(string s)
        {
            return System.Convert.FromBase64String(s);
        }

        /// <summary>
        /// 使用UTF-8编码规则将字符串转化为byte数组
        /// </summary>
        /// <param name="s">待转化的字符串</param>
        /// <returns>转化后得到的byte数组</returns>
        public static byte[] ConvertStringToByteArray(String s)
        {
            return System.Text.Encoding.GetEncoding("utf-8").GetBytes(s);//gb2312
        }

        /// <summary>
        /// 使用UTF-8编码规则将byte数组转化为字符串
        /// </summary>
        /// <param name="buf">待转化的byte数组</param>
        /// <returns>转化后得到的字符串</returns>
        public static string ConvertByteArrayToString(byte[] buf)
        {
            return System.Text.Encoding.GetEncoding("utf-8").GetString(buf);
        }

        /// <summary>
        /// 将byte数组转化为用16进制字符串的表示形式。如byte[]{255, 13, 16}转化后即为FF0D10
        /// </summary>
        /// <param name="buf">待转化的byte数组</param>
        /// <returns>转化后得到的字符串</returns>
        public static string ByteArrayToHexString(byte[] buf)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buf.Length; i++)
            {
                sb.Append(buf[i].ToString("X").Length == 2 ? buf[i].ToString("X") : "0" + buf[i].ToString("X"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将byte数组的16进制字符串表示形式转化为实际的byte数组。如"FF0D10"转化后即为byte[]{255, 13, 16}
        /// </summary>
        /// <param name="s">byte数组的16进制字符串表示形式</param>
        /// <returns>实际的byte数组</returns>
        public static byte[] HexStringToByteArray(string s)
        {
            Byte[] buf = new byte[s.Length / 2];
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = (byte)(chr2hex(s.Substring(i * 2, 1)) * 0x10 + chr2hex(s.Substring(i * 2 + 1, 1)));
            }
            return buf;
        }

        private static byte chr2hex(string chr)
        {
            switch (chr)
            {
                case "0":
                    return 0x00;
                case "1":
                    return 0x01;
                case "2":
                    return 0x02;
                case "3":
                    return 0x03;
                case "4":
                    return 0x04;
                case "5":
                    return 0x05;
                case "6":
                    return 0x06;
                case "7":
                    return 0x07;
                case "8":
                    return 0x08;
                case "9":
                    return 0x09;
                case "A":
                    return 0x0a;
                case "B":
                    return 0x0b;
                case "C":
                    return 0x0c;
                case "D":
                    return 0x0d;
                case "E":
                    return 0x0e;
                case "F":
                    return 0x0f;
            }
            return 0x00;
        }
    }

    public class MD5Util
    {
        public static string GetHash(string password)
        {
            var hmacMD5 = new HMACMD5(System.Text.Encoding.UTF8.GetBytes("my salt"));
            var saltedHash = Convert.ToBase64String(hmacMD5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            return saltedHash;

        }

        
    }
}
