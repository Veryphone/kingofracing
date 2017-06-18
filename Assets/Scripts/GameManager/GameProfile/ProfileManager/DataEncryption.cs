using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;

public class DataEncryption
{
	static bool isNeedEncryption = true;
	//
	static string PasswordHash = "Password";
	static string SaltKey = "Saltkey1";
	static string IVKey = "SplayStudio12345";
	//
	static byte[] keyBytes = new Rfc2898DeriveBytes (PasswordHash, Encoding.ASCII.GetBytes (SaltKey)).GetBytes (256 / 8);
	static byte[] ivKeyBytes = Encoding.ASCII.GetBytes (IVKey);
	//
	static RijndaelManaged symmetricKeyEncrypt = new RijndaelManaged () { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
	static RijndaelManaged symmetricKeyDecrypt = new RijndaelManaged () { Mode = CipherMode.CBC, Padding = PaddingMode.None };	
	//
	
	public static string Encrypt (string plainText)
	{
		if (isNeedEncryption == true) {
			byte[] plainTextBytes = Encoding.ASCII.GetBytes (plainText);

			ICryptoTransform encryptor = symmetricKeyEncrypt.CreateEncryptor (keyBytes, ivKeyBytes);

			byte[] cipherTextBytes;
		
			using (MemoryStream memoryStream = new MemoryStream()) {
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
					cryptoStream.Write (plainTextBytes, 0, plainTextBytes.Length);
					cryptoStream.FlushFinalBlock ();
					cipherTextBytes = memoryStream.ToArray ();
					cryptoStream.Close ();
				}
				memoryStream.Close ();
			}
			return Convert.ToBase64String (cipherTextBytes);
		} else {
			return plainText;
		}
	}
	
	public static string Decrypt (string encryptedText)
	{
		if (isNeedEncryption == true) {
			byte[] cipherTextBytes = Convert.FromBase64String (encryptedText);

			ICryptoTransform decryptor = symmetricKeyDecrypt.CreateDecryptor (keyBytes, ivKeyBytes);

			MemoryStream memoryStream = new MemoryStream (cipherTextBytes);
			CryptoStream cryptoStream = new CryptoStream (memoryStream, decryptor, CryptoStreamMode.Read);

			byte[] plainTextBytes = new byte[cipherTextBytes.Length];
		
			int decryptedByteCount = cryptoStream.Read (plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close ();
			cryptoStream.Close ();
			return Encoding.ASCII.GetString (plainTextBytes, 0, decryptedByteCount).TrimEnd ("\0".ToCharArray ());
		} else {
			return encryptedText;
		}
	}
}
