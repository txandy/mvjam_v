using System;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Score : MonoBehaviour
    {
        public static Score Instance;

        public TextMeshProUGUI textMeshProUgui;

        private int _score = 0;

        public int ScorePoints => _score;

        private void Awake()
        {
            Instance = this;

            PlayerController.PlayerStartBlockEvent += PlayerStartBlockEvent;
        }

        private void PlayerStartBlockEvent()
        {
            _score++;

            textMeshProUgui.text = _score.ToString();
        }

        // Start is called before the first frame update
        void Start()
        {
            _score = 0;
        }
    }
}