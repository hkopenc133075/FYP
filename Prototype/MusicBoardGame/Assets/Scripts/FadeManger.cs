using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManger : MonoBehaviour {

	public static FadeManger Instance{ set; get;}

	public Image thisImage;
	public bool isInTransition;
	public float transition;
	public bool isShowing;
	public float duration;


	public void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}

	public void Test ()
	{
	}

	public void Fade(bool showing , float duration)
	{
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		transition = (isShowing) ? 0 : 1;
	}

	// Update is called once per frame
	void Update () {

		if (!isInTransition)
			return;

		transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
		thisImage.color = Color.Lerp (new Color (1, 1, 1, 0), Color.black, transition);

		if (transition > 1 || transition < 0)
			isInTransition = false;

	}
}
