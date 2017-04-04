using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tuition : MonoBehaviour {

	public GameObject Boss;
	public GameObject AttactEffect;
	public AudioSource MusicNote , BgMusic;
	public AudioClip C, E , G;
	public Slider ShowHealth1;
	public int iBossHealth;
	public int BossTurn;
	string noteaAnswer , bgMusicAnswer;
	bool bDamaged , bmultiHit , bStart;
	public string [] muiscNote = new string[]{ "C,E,G", "C,G,E" };

	// Use this for initialization
	void Start () {
		bDamaged = false;
		bmultiHit = false;
		bStart = false;
		StartCoroutine ("delete");
		StartCoroutine (mainStep ());
	}

	IEnumerator mainStep(){

		yield return  new WaitUntil (() => bStart == true);
		while(bStart)
		{
			
		yield return new WaitForSeconds(1);
	
		MyNotifications.CallNotification("Listen Background Music", 5);
		yield return new WaitForSeconds(1);
		BgMusic.Play ();
		yield return new WaitForSeconds(5);


		MyNotifications.CallNotification("Listen Music Note", 20);
		string[] requirtNote = { "", "", "" };
		requirtNote = muiscNote[0].Split (',');
		PlayRequire (requirtNote);
		yield return new WaitForSeconds (20);

		MyNotifications.CallNotification("Goodbye World!", 3);
		}
	}



	// Update is called once per frame
	void Update () {
		if (!bStart) {
			Debug.Log ("update here");
			StartCoroutine(select("CheckClient",0,null,null,0));
		}
	}

	IEnumerator insert(string stAction , string stContent)
	{


		WWW itemsData = new WWW ("http://199.175.49.17/FYP/dataType.php?Action=insert&GameAction=" + stAction + "&Content=" + stContent);
		//bInsterResult = true;
		yield return itemsData;
		string stItemsData = itemsData.text;

		if (stItemsData == "Successful") 
		{
			
		}
		else if(stItemsData == "Fail")
		{

		}
		yield return null;
	}

	IEnumerator delete()
	{

		string[] arrLongItem;
		string[] arrItem;
		string[] arrActionID;
		string[] arrGameAction;
		string[] arrContent;

		WWW itemsData = new WWW ("http://199.175.49.17/FYP/dataType.php?Action=select");
		yield return itemsData;
		string stItemsData = itemsData.text;
		arrLongItem = stItemsData.Split (';');

		if (arrLongItem [0] == "Fail")
		{
			arrActionID = new string[1]{null};
			arrGameAction = new string[1]{null};
			arrContent = new string[1]{null};

		} 
		else
		{
			arrActionID = new string[arrLongItem.Length];
			arrGameAction = new string[arrLongItem.Length];
			arrContent = new string[arrLongItem.Length ];

			for (int x = 0; x < arrLongItem.Length; x++) 
			{
				arrItem = arrLongItem[x].Split (',');
				arrActionID [x] = arrItem [0];
				arrGameAction [x] = arrItem [1];
				arrContent [x] = arrItem [2];
			}
		}

		for (int y = 0; y < arrActionID.Length; y++)
		{
			itemsData = new WWW ("http://199.175.49.17/FYP/dataType.php?Action=deletById&ActionID=" + arrActionID[y]);
			yield return itemsData;
		}

	}

	IEnumerator select(string strAction ,int inumBoss, string strRectriction , string srtNote , int iBgmNum)
	{

		string[] arrLongItem;
		string[] arrItem;
		string[] arrContentItem = new string[2];
		string[] arrActionID;
		string[] arrGameAction;
		string[] arrContent;

		WWW itemsData = new WWW ("http://199.175.49.17/FYP/dataType.php?Action=select");
		yield return itemsData;
		string stItemsData = itemsData.text;
		arrLongItem = stItemsData.Split (';');

		if (arrLongItem [0] == "Fail")
		{
			arrActionID = new string[1]{null};
			arrGameAction = new string[1]{null};
			arrContent = new string[1]{null};

		} 
		else
		{
			arrActionID = new string[arrLongItem.Length];
			arrGameAction = new string[arrLongItem.Length];
			arrContent = new string[arrLongItem.Length ];

			for (int x = 0; x < arrLongItem.Length; x++) 
			{
				arrItem = arrLongItem[x].Split (',');
				arrActionID [x] = arrItem [0];
				arrGameAction [x] = arrItem [1];
				arrContent [x] = arrItem [2];
			}
		}


		if (strAction == "CheckQuit")  { 
			if (arrGameAction [arrContent.Length - 1] == "QuitGame") {
				Debug.Log ("quit");
				Application.LoadLevel ("Fyp_Interface");
			}
		}else if (strAction == "HitBoss") {

			if (arrGameAction [arrContent.Length - 1] == "InputNoteByClient") {

				StartCoroutine (delete ()); //as the db is show all data, we need to clean the data to ready to next turn data comeing.
				Debug.Log ("already detel ");
				Debug.Log ("deteled");

				arrContentItem = arrContent [arrContent.Length - 1].Split ('_');

				if (srtNote == arrContentItem [1] && bgMusicAnswer == arrContentItem [2]) {  
					bDamaged = true;

					if (strRectriction == arrContentItem [0]) {
						bmultiHit = true;
					} else {
						bmultiHit = false;
					}

					Debug.Log ("Attact scuccess");

				} else {
					Debug.Log ("Attact fail");
				
				}
			}

		} else if (strAction == "CheckClient") {
			if (arrGameAction [arrGameAction.Length - 1] == "EntherServer") {
				bStart = true;
				StartCoroutine (delete ());
			}
		} 
		yield return null;
	}
		
	IEnumerator PlayerHP(string Action , int HP)
	{
		if (Action == "delete") {
			WWW itemsData = new WWW ("http://199.175.49.17/FYP/playerHP.php?Action=delete");
			yield return itemsData;
		}
		else if (Action == "insert") {
			WWW itemsData = new WWW ("http://199.175.49.17/FYP/playerHP.php?Action=insert&HP=" + HP);

			yield return itemsData;
			string stItemsData = itemsData.text;

			if (stItemsData == "Successful") 
			{

			}
			else if(stItemsData == "Fail")
			{

			}
			yield return null;
		}
		yield return null;
	}


	IEnumerator PlayRequire(string [] arrRequire)
	{
		Debug.Log("The Note is " + arrRequire[0] + "," + arrRequire[1] + "," + arrRequire[2]);
		for (int y = 0; y < 3; y++) {

			StartCoroutine(select("CheckQuit",0,null,null,0));

			for (int x = 0; x < arrRequire.Length; x++) {
				if (arrRequire [x] == "C") {
					MusicNote.clip = C;
					MusicNote.Play ();
				} else if (arrRequire [x] == "E") {
					MusicNote.clip = E;
					MusicNote.Play ();
				} else if (arrRequire [x] == "G") {
					MusicNote.clip = G;
					MusicNote.Play ();
				} 

				yield return new WaitForSeconds (1);
			}
			yield return new WaitForSeconds (3);
		}
	}

}
