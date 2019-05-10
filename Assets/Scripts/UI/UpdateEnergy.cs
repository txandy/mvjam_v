using System.Globalization;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UpdateEnergy : MonoBehaviour
    {
        #region private var

        private TextMeshProUGUI _textMeshPro;

        #endregion

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();

            // Events
            PlayerController.UpdateEnergyEvent += UpdateEnergyEvent;
        }


        private void OnDestroy()
        {
            PlayerController.UpdateEnergyEvent -= UpdateEnergyEvent;
        }

        private void UpdateEnergyEvent(float currentEnergy)
        {
            _textMeshPro.text = currentEnergy.ToString(CultureInfo.InvariantCulture);
        }
    }
}