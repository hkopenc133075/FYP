using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

	public Material[] material;
	Renderer rend;
	public Slider playerHP;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = material [3];
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void showHP(int i)
	{
		rend.sharedMaterial = material [i];
	}
}
