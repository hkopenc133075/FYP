using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour {

	public Boss BossClass;
	public GameObject Boss1, Boss2;
	public GameObject AttactEffect;
	public BackgroundMusic bgClass;
	public AudioSource MusicNote;
	public AudioSource BgMusic;
	public AudioClip C;
	public AudioClip D;
	public AudioClip E;
	public AudioClip F;
	public AudioClip G;
	public AudioClip A;
	public AudioClip B;
	public Slider ShowHealth1 , ShowHealth2;
	public Image damageImage;
	bool bStart , bInsterResult , bDeletResult , bDamaged ,bReceive , bmultiHit , bnextTurn;
	public Text textDebug;
	public TypeOutScript textNotice;
	public int iBoss1Health , iBoss2Health;
	public int iMultipleHit;
	int iLevel = 6;
	int iBoss1 , iBoss2;
	int PlayerHealth;
	Vector3 Boss1Position;
	Vector3 Boss2Position;
	public int iPlayerHP;

	string[] Boss1Note = new string[] {};
	string[] Boss2Note = new string[] {};
	//Boss1Note

	int iBoss1turn , iBoss2turn;
	Vector3 iBoss1forward , iBoss2forward;
	bool bBoss1Receive , bBoss2Receive;

	public PlayerHP playerHPclass;

	// Use this for initialization
	void Start () {
		iBoss1 = 0;
		iBoss2 = 1;
		Init (iBoss1, iBoss2);
		iPlayerHP = 4;
		StartCoroutine (PlayerHP ("delete",0));
		StartCoroutine (Game());
	}

	void Init( int iBoss1 , int iBoss2)
	{
		GameObject tempGameObject = (GameObject) Resources.Load (BossClass.BossName [iBoss1], typeof(GameObject));
		StartCoroutine (delete ());
		textNotice.On = true;
		textNotice.FinalText = "";
		bDamaged = false;
		bInsterResult = false;
		bDeletResult = false;
		bStart = false;
		iBoss1Health = 10;
		iMultipleHit = 3;
		ShowHealth1.value = iBoss1Health;
		Boss1Position = BossClass.getBossPosition (BossClass.BossName [iBoss1]);
		Boss2Position = BossClass.getBossPosition (BossClass.BossName [iBoss2]);
		Boss1Note = BossClass.getBossNote (BossClass.BossName [iBoss1]);
		Boss2Note = BossClass.getBossNote (BossClass.BossName [iBoss2]);
		Debug.Log (BossClass.BossName [iBoss2]);
		Boss1 = Instantiate(Resources.Load(BossClass.BossName [iBoss1], typeof(GameObject)), transform.position = Boss1Position,  Quaternion.Euler(0f,-180f,0f)) as GameObject;
		tempGameObject = (GameObject) Resources.Load (BossClass.BossName [iBoss2], typeof(GameObject));
		Boss2 = Instantiate(Resources.Load(BossClass.BossName [iBoss2], typeof(GameObject)), transform.position = Boss2Position, Quaternion.Euler(0f,-180f,0f)) as GameObject;
		//	Boss  = Instantiate(Resources.Load("test", typeof(GameObject)),transform.position = BossPosition, Quaternion.Euler(BossRotation)) as GameObject;
		Boss1.tag = "Boss1";
		Boss2.tag = "Boss2";
		iBoss1turn = BossClass.getBossTurn(BossClass.BossName [iBoss1]);
		iBoss2turn = BossClass.getBossTurn (BossClass.BossName [iBoss1]);
		iBoss1forward = BossClass.getBossforward(BossClass.BossName [iBoss1]);
		iBoss2forward = BossClass.getBossforward (BossClass.BossName [iBoss2]);

	}

	IEnumerator Game()
	{
		
		yield return  new WaitUntil (() => bStart == true);
		while(bStart)
		{


				int Boss1Rnote =  Random.Range(0, 6);
				int Boss2Rnote =  Random.Range(0, 6);
				//int BgMusicNum =   Random.Range(0, 3);
				int BgMusicNum =  0;
				textDebug.text = "Game Start";
				string[] strBoss1Require;
				string[] strBoss2Require;
				textNotice.On = true;
				textNotice.FinalText = "Are you ready?";
				yield return new WaitForSeconds (3);
				BgMusic.Play ();
				while (iPlayerHP > 0) 
				{

					BgMusic.clip = bgClass.backgroundMusic [BgMusicNum];
					BgMusic.Play ();
					playerHPclass.showHP (iPlayerHP - 1);
					StartCoroutine(select("CheckQuit",0,null,null,0));
					textNotice.reset = true;

					StartCoroutine (PlayerHP ("insert",iPlayerHP));

					strBoss1Require = new string[] { "", "", "" };
					strBoss2Require = new string[] { "", "", "" };

					textDebug.text = "Game Start and Round:" + iBoss1turn + " and " + iBoss2turn;

					strBoss1Require = Boss1Note [Boss1Rnote].Split (',');
					StartCoroutine(PlayRequire (strBoss1Require));

					yield return new WaitForSeconds (20);

					strBoss2Require = Boss2Note [Boss2Rnote].Split (',');
					StartCoroutine(PlayRequire (strBoss2Require));

					StartCoroutine(insert("RequiredNote","NULL"));
					bBoss1Receive = false;
					bBoss2Receive = false;
					bnextTurn = false;

					Debug.Log(strBoss1Require[0] + "," + strBoss1Require[1] + "," + strBoss1Require[2]);
					yield return new WaitForSeconds (10);

				while (!bBoss1Receive && bInsterResult && !bBoss2Receive && !bnextTurn) {
						StartCoroutine(select("CheckQuit",0,null,null,0));
						StartCoroutine (select ("HitBoss", 1 ,  BossClass.BossWeak[iBoss1], strBoss1Require[0]+strBoss1Require[1]+strBoss1Require[2],BgMusicNum));
						StartCoroutine (select ("HitBoss", 2 , BossClass.BossWeak[iBoss2], strBoss2Require[0]+strBoss2Require[1]+strBoss2Require[2],BgMusicNum));
						yield return new WaitForSeconds (1);
					}

				//check boss 1 move or attact 
				if (bBoss1Receive) {

					if (bmultiHit) {
						iBoss1Health = iBoss1Health - 1 * iMultipleHit;
						ShowHealth1.value = iBoss1Health;
					} else {
						iBoss1Health = iBoss1Health - 1;
						ShowHealth1.value = iBoss1Health;
					}

				} 
				else 
				{
					if (iBoss1turn > 0) {
						Boss1Position = Boss1Position + iBoss1forward;
						Boss1.transform.position = Boss1Position;
						iBoss1turn--;
					}
					else
					{
						iPlayerHP--;
					}
				}
				//check boss 2 move or attact 
				if (bBoss2Receive) {
					if (bmultiHit) {
						iBoss2Health = iBoss2Health - 1 * iMultipleHit;
						ShowHealth2.value = iBoss2Health;
					} else {
						iBoss2Health = iBoss2Health - 1;
						ShowHealth2.value = iBoss2Health;
					}
				}
				else
				{
					if (iBoss2turn > 0) {
						Boss2Position = Boss2Position + iBoss2forward;
						Boss2.transform.position = Boss2Position;
						iBoss2turn--;
					}
					else
					{
						iPlayerHP--;
					}
				}

				Boss1Rnote =  Random.Range(0, 6);
             	Boss2Rnote =  Random.Range(0, 6);
				BgMusicNum =   Random.Range(0, 3);
					//yield return null;
				}
			/*GameObject Effect = (GameObject)Instantiate (AttactEffect, Boss1Position, transform.rotation);
			Destroy (Effect, 1f);
			Destroy (Boss1, 1f);*/
			while (true) {
				textNotice.On = true;
				textNotice.FinalText = "End the Game!";
				yield return new WaitForSeconds (1);
			}
			yield return new WaitForSeconds (1);
		}
	}


	// Update is called once per frame
	void Update () {
		
		if (!bStart) 
		{
			textDebug.text = "Wait Client";
			StartCoroutine(select("CheckClient",0,null,null,0));
		}

		if (bDamaged) {
			damageImage.rectTransform.sizeDelta = new Vector2 (300, 300);
			damageImage.color = new Color (1f, 0f, 0f, 0.1f);
			GameObject Effect = (GameObject)Instantiate(AttactEffect, Boss1Position,transform.rotation);
			StartCoroutine (delay (1));
			Destroy (Effect, 1f);

		} else {
			//damageImage.rectTransform.sizeDelta = new Vector2 (300, 300);
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, 10f * Time.deltaTime);
		}

		bDamaged = false;
	}



	IEnumerator insert(string stAction , string stContent)
	{


		WWW itemsData = new WWW ("http://199.175.49.17/FYP/dataType.php?Action=insert&GameAction=" + stAction + "&Content=" + stContent);
		//bInsterResult = true;
		yield return itemsData;
		string stItemsData = itemsData.text;

		if (stItemsData == "Successful") 
		{
			bInsterResult = true;
		}
		else if(stItemsData == "Fail")
		{
			bInsterResult = false;
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
				//bReceive = true;

				arrContentItem = arrContent [arrContent.Length - 1].Split ('_');

				if (srtNote == arrContentItem [1] && bgClass.nameOfBg[iBgmNum] == arrContentItem [2]) {   //change here bBoss1Receive , bBoss2Receive

					bDamaged = true;

					if (strRectriction == arrContentItem [0]) {
						bmultiHit = true;
					} else {
						bmultiHit = false;
					}

					if (inumBoss == 1) {
						bBoss1Receive = true;
					} else if (inumBoss == 2) {
						bBoss2Receive = true;
					}

					Debug.Log ("Attact scuccess");
					textNotice.On = true;
					textNotice.FinalText = "Attact scuccess";
					bnextTurn = true;
				} else {
					Debug.Log ("Attact fail");
					textNotice.On = true;
					textNotice.FinalText = "Attact fail";
					bnextTurn = true;
				}
				StartCoroutine (clearnText (3));
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
				bInsterResult = true;
			}
			else if(stItemsData == "Fail")
			{
				bInsterResult = false;
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
				} else if (arrRequire [x] == "D") {
					MusicNote.clip = D;
					MusicNote.Play ();
				} else if (arrRequire [x] == "E") {
					MusicNote.clip = E;
					MusicNote.Play ();
				} else if (arrRequire [x] == "F") {
					MusicNote.clip = F;
					MusicNote.Play ();
				} else if (arrRequire [x] == "G") {
					MusicNote.clip = G;
					MusicNote.Play ();
				} else if (arrRequire [x] == "A") {
					MusicNote.clip = A;
					MusicNote.Play ();
				} else if (arrRequire [x] == "B") {
					MusicNote.clip = B;
					MusicNote.Play ();
				}

				yield return new WaitForSeconds (1);
			}
			yield return new WaitForSeconds (3);
		}
	}

	IEnumerator delay(int second)
	{
		yield return new WaitForSeconds (second);
	}
		
	IEnumerator clearnText(int second)
	{
		yield return new WaitForSeconds (second);
		textNotice.reset = true;
		yield return null;
	}

}
