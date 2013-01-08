///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.Utilities.TributePortalSecurity.Security.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used to implement encryption and decryption of data to provide security
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Security;
using System.Security.Cryptography;

namespace TributePortalSecurity
{
    public partial class Security
    {
        private const string hashProvider  = "PasswordHasher";
        private const string symmProvider = "SymmetricProvider";
          

        private const string passPhrase = "Pas5pr@se";        // can be any string
        private const string saltValue = "s@1tValue";        // can be any string
        private const string hashAlgorithm = "SHA1";             // can be "MD5"
        private const int passwordIterations = 2;                  // can be any number
        private const string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        private const int keySize = 256;                // can be 192 or 128

        public static string CreateHash(string plainText) 
        {
            string hash  = null;

         //   hash = Cryptographer.CreateHash(hashProvider, plainText);

            return hash;

        }

        public static bool CompareHash(string plainText , string hashedText ) 
        {
            bool compare  = false;

          //  compare = Cryptographer.CompareHash(hashProvider, plainText, hashedText);
            
            return compare;

        }

        public static string EncryptSymmetric(string text)
        {
            string encrypted = RijndaelSimple.Encrypt(text,passPhrase,saltValue,hashAlgorithm,
                passwordIterations,initVector,keySize);
            return encrypted;
        }

        public static string DecryptSymmetric(string text)
        {
            string decrypted = RijndaelSimple.Decrypt(text, passPhrase, saltValue, hashAlgorithm,
                passwordIterations, initVector, keySize); ;
            return decrypted;
        }
    }
}
    