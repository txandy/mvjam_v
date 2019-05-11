using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class MusicController : MonoBehaviour
    {
        const float MusicSourceVolume = 0.2f;
        
        public Sprite musicOn;
        public Sprite musicOff;
        public Button toggleMusic;
        public Image buttonImage;
        private int _volume;

        private void Awake()
        {
            if (PlayerPrefs.HasKey(Helper.PREFS_MUSIC))
            {
                _volume = PlayerPrefs.GetInt(Helper.PREFS_MUSIC);
            }

            _volume = 1;
        }

        // Start is called before the first frame update
        void Start()
        {
            ChangeVolume();

            toggleMusic.onClick.AddListener(OnClickButton);

            ToggleButtonSprite();
        }

        private void OnClickButton()
        {
            _volume = _volume == 0 ? 1 : 0;

            PlayerPrefs.SetInt(Helper.PREFS_MUSIC, _volume);
            PlayerPrefs.Save();
            
            ChangeVolume();

            ToggleButtonSprite();
        }

        private void ChangeVolume()
        {
            if (_volume == 0)
            {
                GameManager.Instance.musicSource.volume = 0;
            }
            else
            {
                GameManager.Instance.musicSource.volume = MusicSourceVolume;
            }
        }

        void ToggleButtonSprite()
        {
            if (_volume == 0)
            {
                buttonImage.sprite = musicOff;
            }
            else
            {
                buttonImage.sprite = musicOn;
            }
        }
    }
}