using System.Globalization;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpdateEnergy : MonoBehaviour
    {
        #region private var

        public TextMeshProUGUI textMeshPro;
        public Image fillImage;

        #endregion

        private void Awake()
        {
            // Events
            PlayerController.UpdateEnergyEvent += UpdateEnergyEvent;
        }


        private void OnDestroy()
        {
            PlayerController.UpdateEnergyEvent -= UpdateEnergyEvent;
        }

        private void UpdateEnergyEvent(float currentEnergy)
        {
            textMeshPro.text = currentEnergy.ToString(CultureInfo.InvariantCulture);
            fillImage.fillAmount = currentEnergy / 100;
        }
    }
}