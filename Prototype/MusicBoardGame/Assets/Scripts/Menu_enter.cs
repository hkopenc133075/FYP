using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_enter : MonoBehaviour {

	public InputField inputfile;
	public Button enterbtn;
	public string code;
	bool isEnter;
	// Use this for initialization
	void Start () {
		enterbtn.onClick.AddListener (EnterGame);
		isEnter = false;
		code = "null";
	}

	// Update is called once per frame
	void Update () {
		code = inputfile.text;
		if (isEnter) {
			StartCoroutine (select ());
		}
	}

	void EnterGame()
	{
		StartCoroutine (insert (code));
	}

	IEnumerator insert(string code)
	{
		if (code != "null") {
			WWW itemsData = new WWW ("http://199.175.49.17/FYP/verification.php?Action=insert&code=" + code);
			yield return itemsData;
			string stItemsData = itemsData.text;

			if (stItemsData == "Successful") {
				isEnter = true;
			} else if (stItemsData == "Fail") {
				isEnter = false;
			}
		}
		yield return null;
	}

	IEnumerator select()
	{
		WWW itemsData = new WWW ("http://199.175.49.17/FYP/notification.php?Action=select");
		yield return itemsData;
		string stItemsData = itemsData.text;
		Debug.Log (stItemsData);
		if (stItemsData == "WaitInput") {
			itemsData = new WWW ("http://199.175.49.17/FYP/notification.php?Action=delete");
			yield return itemsData;
			string strData = itemsData.text;
			if (strData == "Successful") {
				Application.LoadLevel ("FYP_InputSide_Interface");
			} else if (strData == "Fail") {

			}
		} 

		yield return null;
	}
}
