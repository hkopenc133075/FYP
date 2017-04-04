using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.UI;

public class Menu_display : MonoBehaviour {

	string key = "12345678901234567890126535789000";
	string Text;
	public Text code;
	// Use this for initialization
	void Start () {
		Text = System.DateTime.Now.ToString ("MMddyyyy") + System.DateTime.Now.ToString ("hhmmss");
		Text = Encrypt (Text, key);
		Text = Text.Substring (0, 5);
		code.text = Text;
	}

	public static string Encrypt(string Text, string sKey)
	{
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes(sKey);

		RijndaelManaged encryption = new RijndaelManaged();

		encryption.Key = keyArray;

		encryption.Mode = CipherMode.ECB;

		encryption.Padding = PaddingMode.PKCS7;

		ICryptoTransform cTransform = encryption.CreateEncryptor();

		byte[] _EncryptArray = UTF8Encoding.UTF8.GetBytes(Text);

		byte[] resultArray = cTransform.TransformFinalBlock(_EncryptArray, 0, _EncryptArray.Length);

		return Convert.ToBase64String(resultArray, 0, resultArray.Length);

	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (select (Text));
	}

	IEnumerator select(string code)
	{
		WWW itemsData = new WWW ("http://199.175.49.17/FYP/verification.php?Action=select");
		yield return itemsData;
		string stItemsData = itemsData.text;


		if (stItemsData == code) {
			itemsData = new WWW ("http://199.175.49.17/FYP/verification.php?Action=delete");
			yield return itemsData;

			itemsData = new WWW ("http://199.175.49.17/FYP/notification.php?Action=insert&data=WaitInput");
			yield return itemsData;

			string strData = itemsData.text;

			if (strData == "Successful") 
			{
				Application.LoadLevel ("Fyp_Scene");
			}
			else if(strData == "Fail")
			{
				
			}

		}

		yield return  null;
	}
}
