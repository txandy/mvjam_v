using System;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class DiePopup : MonoBehaviour
    {
        public TextMeshProUGUI textMeshProUgui;
        public List<string> dieTexts;
        public Button playAgainButton;
        public Transform container;
        
        private void Awake()
        {
            PlayerController.PlayerDieEvent += PlayerDieEvent;
            container.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            PlayerController.PlayerDieEvent -= PlayerDieEvent;
        }

        private void PlayerDieEvent()
        {
            Time.timeScale = 0;
            container.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            textMeshProUgui.text = dieTexts[Random.Range(0, dieTexts.Count)];
        }

        // Start is called before the first frame update
        void Start()
        {
            playAgainButton.onClick.AddListener(PlayAgainClick);
        }

        private void PlayAgainClick()
        {
            GameManager.Instance.RestartGame();
        }
    }
}