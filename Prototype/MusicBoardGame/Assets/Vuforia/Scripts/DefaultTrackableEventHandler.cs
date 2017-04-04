/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    
	public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;

		public Text textNoteData;
		public Text textRectrkctionData;
		public Text textBgMusic;

    
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
			CheckRight (mTrackableBehaviour.TrackableName);
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

			textNoteData = GameObject.Find ("NoteData").GetComponent<Text> ();
			textRectrkctionData = GameObject.Find ("RectrkctionData").GetComponent<Text> ();
			textBgMusic = GameObject.Find ("BgMusicData").GetComponent<Text> ();

			textNoteData.text = "Null";
			textRectrkctionData.text = "Null";
			textBgMusic.text = "Null";

			//CheckError (mTrackableBehaviour.TrackableName);
        }


		void CheckRight (string strCheckItem)
		{
			textNoteData = GameObject.Find ("NoteData").GetComponent<Text> ();
			textRectrkctionData = GameObject.Find ("RectrkctionData").GetComponent<Text> ();
			textBgMusic = GameObject.Find ("BgMusicData").GetComponent<Text> ();

			if (strCheckItem == "Temperance") {
				textRectrkctionData.text = "Temperance";
			} else if (strCheckItem == "Chastity") {
				textRectrkctionData.text = "Chastity";
			}else if (strCheckItem == "Charity") {
				textRectrkctionData.text = "Charity";
			}else if (strCheckItem == "Diligence") {
				textRectrkctionData.text = "Diligence";
			}else if (strCheckItem == "Humilty") {
				textRectrkctionData.text = "Humility";
			}else if (strCheckItem == "Kindness") {
				textRectrkctionData.text = "Kindness";
			}else if (strCheckItem == "Patience") {
				textRectrkctionData.text = "Patience";
			}

			if(strCheckItem == "Charity_GEC")
			{
				textNoteData.text = "GEC";
			}
			else if(strCheckItem == "Charity_GCE")
			{
				textNoteData.text = "GCE";
			}
			else if(strCheckItem == "Charity_EGC")
			{
				textNoteData.text = "EGC";
			}
			else if(strCheckItem == "Charity_ECG")
			{
				textNoteData.text = "ECG";
			}
			else if (strCheckItem == "Charity_CGE")
			{
				textNoteData.text = "CGE";
			}
			else if (strCheckItem == "Charity_CEG")
			{
				textNoteData.text = "CEG";
			}

			if (strCheckItem == "ar_A") {
				textRectrkctionData.text = "Test";
			} 
		}



        #endregion // PRIVATE_METHODS
    }

}
