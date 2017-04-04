using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleAnimation : MonoBehaviour {


	public GameObject[] character;
	public Text textRectrkctionData;
	bool playanimation;
	// Use this for initialization
	void Start () {
		playanimation = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (textRectrkctionData.text == "Temperance" && playanimation == false) {
			character[0].GetComponent<Animator> ().SetTrigger ("CharacterActive");
			playanimation = true;
		}
		else if (textRectrkctionData.text == "Charity" && playanimation == false) {
			character[1].GetComponent<Animator> ().SetTrigger ("CharacterActive");
			playanimation = true;
		}
		else if (textRectrkctionData.text == "Chastity" && playanimation == false) {
			character[2].GetComponent<Animator> ().SetTrigger ("CharacterActive");
			playanimation = true;
		}	else if (textRectrkctionData.text == "Diligence" && playanimation == false) {
			character[3].GetComponent<Animator> ().SetTrigger ("CharacterActive");
			playanimation = true;
		}
		else if (textRectrkctionData.text == "Humility" && playanimation == false) {
			character[4].GetComponent<Animator> ().SetTrigger ("CharacterActive");
			playanimation = true;
		}
		else if (textRectrkctionData.text == "Kindness" && playanimation == false) {
			character[5].GetComponent<Animator> ().SetTrigger ("CharacterActive");
			playanimation = true;
		}
		else if (textRectrkctionData.text == "Patience" && playanimation == false) {
			character[6].GetComponent<Animator> ().SetTrigger ("CharacterActive");
			playanimation = true;
		}






		if (textRectrkctionData.text == "Null") {
			playanimation = false;
		}
	}
}
