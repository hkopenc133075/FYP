using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputController : MonoBehaviour {

	public Text textNoteData;
	public Text textRectrkctionData;
	public Text textBgMusic;
	public TypeOutScript textAlert;
	public Button butInput;
	public Button testBtn;
	public Button helpHealth;
	public Button quitBtn;
	bool bInsterResult , bButton;
	public GameObject test2;
	public Slider HP;

	static string myLog = "";
	private string output;
	private string stack;

	int iNumHealth;

	IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		//textNoteData.enabled = false;
		//textRectrkctionData.enabled = false;
		butInput.onClick.AddListener (sendToServer);
		//testBtn.onClick.AddListener (delObj);
		helpHealth.onClick.AddListener (HelpHealth);
		quitBtn.onClick.AddListener (QuitGame);
		bButton = false;
		iNumHealth = 1;
		StartCoroutine (insert (false,"EntherServer", "Null"));
	}

	void QuitGame()
	{
		StartCoroutine (insert (false,"QuitGame", "Null"));
		Application.LoadLevel ("Fyp_Interface");
	}

	void HelpHealth()
	{
		StartCoroutine (PlayerHP ("insert"));
		iNumHealth--;
	}

	void delObj ()
	{
		GameObject obj;
		GameObject matherobj;
		int x, y;
		obj = GameObject.FindGameObjectWithTag ("Temperance");
		matherobj = GameObject.Find (textRectrkctionData.text);
		x = (int)obj.transform.position.x;
		y = (int)obj.transform.position.y;
		//GameObject.Equals(test,obj);
		DestroyObject (obj);
		//coroutine = Wait (obj,matherobj,x,y);
		StartCoroutine (Wait (test2,matherobj,x,y));
		//Destroy (obj, 5);
	}
	
	// Update is called once per frame
	void Update () {

		if (!bButton) {
			StartCoroutine (select ("checkButton", "Null", "Null"));
			butInput.interactable = false; 
		} 
		else 
		{
			butInput.interactable = true; 
		}

		StartCoroutine (PlayerHP ("select"));


		Debug.Log (iNumHealth);
		if (iNumHealth > 0) {
			helpHealth.interactable = true; 
		} else {
			helpHealth.interactable = false; 
		}
	}

	IEnumerator PlayerHP(string Action)
	{	
		if (Action == "select") {
			WWW itemsData = new WWW ("http://199.175.49.17/FYP/playerHP.php?Action=select");
			yield return itemsData;
			string stItemsData = itemsData.text;

			if (stItemsData == "Fail") {
				HP.value = 0;
			} else {
				HP.value = int.Parse (stItemsData);
			}
		} else if (Action == "insert") {
			Debug.Log (Mathf.RoundToInt (HP.value));
			int totalHP =  Mathf.RoundToInt(HP.value) + 1;
			WWW itemsData = new WWW ("http://199.175.49.17/FYP/playerHP.php?Action=insert&HP="+totalHP);
			yield return itemsData;
			string stItemsData = itemsData.text;

			if (stItemsData == "Successful") {

			} else {
				
			}

		}
		yield return null;
	}

	IEnumerator insert(bool boutput , string stAction , string stContent)
	{

		WWW itemsData = new WWW ("http://199.175.49.17/FYP/dataType.php?Action=insert&GameAction=" + stAction + "&Content=" + stContent);
		yield return itemsData;
		string stItemsData = itemsData.text;


		if (stItemsData == "Successful" && boutput) 
		{
			bInsterResult = true;
			textAlert.On = true;
			textAlert.FinalText = "Input Successful";
			bButton = false;
			textAlert.reset = true;
		}
		else if(stItemsData == "Fail" && boutput)
		{
			bInsterResult = false;
			textAlert.On = true;
			textAlert.FinalText = "Input Faill";
			textAlert.reset = true;
		}

		//StartCoroutine (clearnText (3));

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

	IEnumerator select(string strAction , string strRectriction , string srtNote)
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

		if (strAction == "checkButton") {
			if (arrGameAction [arrLongItem.Length - 1] == "RequiredNote") 
			{
				bButton = true;
				StartCoroutine (delete ());
			}
		}
			
		yield return null;
	}

	void sendToServer()
	{
		if (textRectrkctionData.text != "Null" && textNoteData.text != "Null") 
		{
			StartCoroutine (insert (true , "InputNoteByClient" , textRectrkctionData.text + "_" + textNoteData.text + "_" + textBgMusic.text));
		} 
		else 
		{
			textAlert.FinalText = "";
		
			if (textRectrkctionData.text == "Null")
			{
				textAlert.On = true;
				textAlert.FinalText += "Rectrkction not yet to scran\n";
			}
			if (textNoteData.text == "Null") 
			{
				textAlert.On = true;
				textAlert.FinalText += "Note not yet to scran";
			}
			StartCoroutine (clearnText (3));
		}

	}

	IEnumerator clearnText(int second)
	{
		yield return new WaitForSeconds (second);
		textAlert.reset = true;
		yield return null;
	}

	IEnumerator Wait(GameObject obj , GameObject matherobj , int x , int y)
	{
		yield return new WaitForSeconds(5);
		Debug.Log ("waited");

		obj = Instantiate(obj, new Vector3(x, y, 0), Quaternion.identity);
		obj.tag = "Temperance";
		obj.transform.parent = matherobj.transform;
	}
		
}
