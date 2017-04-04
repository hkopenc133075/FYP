using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menucontrol : MonoBehaviour {

	public Button displayBtn;
	public Button inputBtn;

	// Use this for initialization
	void Start () {

		Screen.orientation = ScreenOrientation.LandscapeLeft;
		displayBtn.onClick.AddListener (LoadDisplay);
		inputBtn.onClick.AddListener (LoadInput);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadDisplay()
	{
		Application.LoadLevel ("Display");
	}

	public void LoadInput()
	{
		Application.LoadLevel ("Input");
	}
}
