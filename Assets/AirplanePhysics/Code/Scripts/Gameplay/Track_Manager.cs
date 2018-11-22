using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;


namespace WeLoveAero
{
    public class Track_Manager : MonoBehaviour
    {
        #region Variables
        [Header("Manager Properties")]
        public List<Track> tracks = new List<Track>();
        public Airplane_Controller airplaneController;

        [Header("Manager UI")]
        public Text gateText;
        public Text timeText;
        public Text scoreText;

        [Header("Manager Events")]
        public UnityEvent OnCompletedRace = new UnityEvent();

        private Track currentTrack;
        #endregion



        #region Builtin Methods
        // Use this for initialization
        private void Start()
        {
            FindTracks();
            InitializeTracks();

            StartTrack(0);
        }

        private void Update()
        {
            if(currentTrack)
            {
                UpdateUI();
            }
        }
        #endregion



        #region Custom Methods
        public void StartTrack(int trackID)
        {
            if (trackID >= 0 && trackID < tracks.Count)
            {
                for(int i = 0; i < tracks.Count; i++)
                {
                    if(i != trackID)
                    {
                        tracks[i].gameObject.SetActive(false);
                    }

                    tracks[trackID].gameObject.SetActive(true);
                    tracks[trackID].StartTrack();
                    currentTrack = tracks[trackID];
                }
            }
        }

        void FindTracks()
        {
            tracks.Clear();
            tracks = GetComponentsInChildren<Track>(true).ToList<Track>();
        }

        void InitializeTracks()
        {
            if(tracks.Count > 0)
            {
                foreach(Track track in tracks)
                {
                    track.OnCompletedTrack.AddListener(CompletedTrack);
                }
            }
        }

        void CompletedTrack()
        {
            Debug.Log("Completed Track!");

            if(airplaneController)
            {
                StartCoroutine("WaitForLanding");
            }
        }

        IEnumerator WaitForLanding()
        {
            while(airplaneController.State != AirplaneState.LANDED)
            {
                yield return null;
            }

            Debug.Log("Completed Race!");
            if (OnCompletedRace != null)
            {
                OnCompletedRace.Invoke();
            }

            if(currentTrack)
            {
                currentTrack.IsComplete = true;
                currentTrack.SaveTrackData();
            }
        }

        void UpdateUI()
        {
            if(gateText)
            {
                gateText.text = "Gates: " + currentTrack.CurrentGateID.ToString() + "/" + currentTrack.TotalGates.ToString();
            }

            if(timeText)
            {
                string minutes = currentTrack.CurrentMinutes.ToString("00");
                string seconds = currentTrack.CurrentSecond.ToString("00");
                timeText.text = minutes + ":" + seconds;
            }

            if(scoreText)
            {
                scoreText.text = "Score: " + currentTrack.CurrentScore.ToString("0000");
            }
        }
        #endregion
    }
}
