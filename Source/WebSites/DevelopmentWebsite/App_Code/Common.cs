///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.App_Code.Common.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to define common methods
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
//using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common 
{
    public Common()
    {
        
    }


    

    private const string hashProvider = "PasswordHasher";
    private const string symmProvider = "SymmetricProvider";


    private const string passPhrase = "Pas5pr@se";        // can be any string
    private const string saltValue = "s@1tValue";        // can be any string
    private const string hashAlgorithm = "SHA1";             // can be "MD5"
    private const int passwordIterations = 2;                  // can be any number
    private const string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
    private const int keySize = 256;                // can be 192 or 128

    public static string CreateHash(string plainText)
    {
        string hash = null;

        //hash = Cryptographer.CreateHash(hashProvider, plainText);

        return hash;

    }

    public static bool CompareHash(string plainText, string hashedText)
    {
        bool compare = false;

        //compare = Cryptographer.CompareHash(hashProvider, plainText, hashedText);

        return compare;

    }

    //public static string EncryptSymmetric(string text)
    //{
    //    //ICryptoTransform onj= base.CreateEncryptor();
    //    //onj.
    //  //  Rijndael obj = new Rijndael();

    //    string encrypted = RijndaelSimple.Encrypt(text, passPhrase, saltValue, hashAlgorithm,
    //        passwordIterations, initVector, keySize);
    //    return encrypted;
    //}

    //public static string DecryptSymmetric(string text)
    //{
    //    string decrypted = RijndaelSimple.Decrypt(text, passPhrase, saltValue, hashAlgorithm,
    //        passwordIterations, initVector, keySize); ;
    //    return decrypted;
    //}


    private static void AddControls(ref ArrayList al, ControlCollection cc)
    {
        foreach (Control c in cc)
        {
            al.Add(c);
            if (c.Controls.Count > 0)
            {
                AddControls(ref al, c.Controls);
            }
        }
    }

    public static ArrayList GetControls(ControlCollection cc)
    {
        ArrayList al = new ArrayList();
        AddControls(ref al, cc);
        return al;
    }

    public bool CheckDirectory(string strLogPath)
    {
        try
        {
            int nFindSlashPos = strLogPath.Trim().LastIndexOf("\\");
            string strDirectoryname = strLogPath.Trim().Substring(0, nFindSlashPos);

            if (false == Directory.Exists(strDirectoryname))
                Directory.CreateDirectory(strDirectoryname);

            return true;
        }
        catch (Exception ex)
        {
            return false;

        }
    }

    public static string EncryptString(string InputText)
    {

        // We are now going to create an instance of the
        // Rihndael class.  
        string Password = "####";
        RijndaelManaged RijndaelCipher = new RijndaelManaged();
        // First we need to turn the input strings into a byte array.
        byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
        // We are using salt to make it harder to guess our key
        // using a dictionary attack.
        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
        // The (Secret Key) will be generated from the specified 
        // password and salt.
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
        // Create a encryptor from the existing SecretKey bytes.
        // We use 32 bytes for the secret key 
        // (the default Rijndael key length is 256 bit = 32 bytes) and
        // then 16 bytes for the IV (initialization vector),
        // (the default Rijndael IV length is 128 bit = 16 bytes)

        ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
        // Create a MemoryStream that is going to hold the encrypted bytes 
        MemoryStream memoryStream = new MemoryStream();
        // Create a CryptoStream through which we are going to be processing our data. 
        // CryptoStreamMode.Write means that we are going to be writing data 
        // to the stream and the output will be written in the MemoryStream
        // we have provided. (always use write mode for encryption)
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
        // Start the encryption process.
        cryptoStream.Write(PlainText, 0, PlainText.Length);
        // Finish encrypting.
        cryptoStream.FlushFinalBlock();
        // Convert our encrypted data from a memoryStream into a byte array.
        byte[] CipherBytes = memoryStream.ToArray();

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        // Convert encrypted data into a base64-encoded string.
        // A common mistake would be to use an Encoding class for that. 
        // It does not work, because not all byte values can be
        // represented by characters. We are going to be using Base64 encoding
        // That is designed exactly for what we are trying to do. 

        string EncryptedData = Convert.ToBase64String(CipherBytes);
        // Return encrypted string.
        return EncryptedData;

    }

    public static string DecryptString(string InputText)
    {
        string Password = "####";
        RijndaelManaged RijndaelCipher = new RijndaelManaged();
        byte[] EncryptedData = Convert.FromBase64String(InputText);
        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

        // Create a decryptor from the existing SecretKey bytes.
        ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
        MemoryStream memoryStream = new MemoryStream(EncryptedData);

        // Create a CryptoStream. (always use Read mode for decryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

        // Since at this point we don't know what the size of decrypted data
        // will be, allocate the buffer long enough to hold EncryptedData;
        // DecryptedData is never longer than EncryptedData.
        byte[] PlainText = new byte[EncryptedData.Length];

        // Start decrypting.
        int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
        memoryStream.Close();
        cryptoStream.Close();
        // Convert decrypted data into a string. 
        string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
        // Return decrypted string.   
        return DecryptedData;

    }




}
