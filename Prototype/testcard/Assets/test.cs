using UnityEngine;
using System.Collections;
using Vuforia;
namespace Vuforia
{
public class test : MonoBehaviour {

		void Start () {
			bool focusModeSet = CameraDevice.Instance.SetFocusMode( 
				CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

			if (!focusModeSet) {
				Debug.Log("Failed to set focus mode (unsupported mode).");
			}
			Debug.Log("hello");
		}
	
}
}