using System;
using UnityEngine;

namespace Tutorial
{
    public class Tutorial : MonoBehaviour
    {
        public static Action StopTutorialEvent = delegate { };

        public GameObject TutorialBox;
        public GameObject TutorialBox1;
        public GameObject TutorialBox2;

        private void Awake()
        {
            TutorialBox.SetActive(false);
            GameManager.StartTutorialEvent += StartTutorialEvent;
            GameManager.SecondTutorialEvent += SecondTutorialEvent;
        }

        private void OnDestroy()
        {
            GameManager.StartTutorialEvent -= StartTutorialEvent;
            GameManager.SecondTutorialEvent -= SecondTutorialEvent;
        }

        private void StartTutorialEvent()
        {
            TutorialBox.SetActive(true);
            TutorialBox1.SetActive(true);
            TutorialBox2.SetActive(false);
        }
        
        private void SecondTutorialEvent()
        {
            TutorialBox.SetActive(true);
            TutorialBox1.SetActive(false);
            TutorialBox2.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) && TutorialBox.activeSelf)
            {
                Stop();
            }
        }

        private void Stop()
        {
            TutorialBox.SetActive(false);
            TutorialBox1.SetActive(false);
            TutorialBox2.SetActive(false);
            StopTutorialEvent();
        }
    }
}