using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followGaemObject : MonoBehaviour {

	public GameObject TrackObject;
	public Vector3 Offset;
	public string BossTage;

	// Use this for initialization
	void Start () {
		//Offset = new Vector3 (-188f, 400f, 0f);
		TrackObject = GameObject.FindGameObjectWithTag (BossTage);
	}
	
	// Update is called once per frame
	void Update () {
		TrackObject = GameObject.FindGameObjectWithTag (BossTage);
		gameObject.transform.position = Camera.main.WorldToScreenPoint (TrackObject.transform.position) + Offset;
	}
}
