using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public string a;
	public string[] BossName = {"Lust_1" , "Gluttony" , "Greed" , "Sloth" , "Wrath" , "Enry" , "Pride"};
	public string[] BossWeak = {"Chastity" , "Temperance"};

	// Use this for initialization
	void Start () {


		//BossArray[0] = "Test";
	}
		

	public string[] getBossNote(string SbossName)
	{
		string[] BossNote = new string[]{};
		if (SbossName == BossName[0]) {
			BossNote = new string[]{ "C,E,G", "C,G,E", "E,G,C", "E,C,G", "G,C,E", "G,E,C" };
		} else if (SbossName == BossName[1]) {
			BossNote = new string[]{ "D,F,A", "D,A,F", "F,A,D", "F,D,A", "A,D,F", "A,F,D" };
		}
		return BossNote;
	}

	public Vector3 getBossPosition(string SbossName)
	{
		Vector3 vr3Position = new Vector3 (0f , 0f ,0f);
		if (SbossName == BossName[0]) {
			return vr3Position = new Vector3(0.72f , -0.45f ,9f);
			Debug.Log ("HI");
		} else if (SbossName == BossName[1]) {
			return vr3Position = new Vector3(-1f , -0.49f ,9f);
			Debug.Log ("HI");
		}
		return vr3Position;
	}

	public int getBossTurn(string SbossName)
	{
		if (SbossName == BossName[0]) {
			return 3;
		} else if (SbossName == BossName[1]) {
			return 4;
		}
		return 0;
	}

	public Vector3 getBossforward(string SbossName)
	{
		if (SbossName == BossName[0]) {
			return new Vector3 (0, 0, -1);
		} else if (SbossName == BossName[1]) {
			return new Vector3 (0, 0, -1);
		}
		return new Vector3 (0, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
