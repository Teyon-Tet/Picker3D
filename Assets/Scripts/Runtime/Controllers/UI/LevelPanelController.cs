using Runtime.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Image> stageImages = new List<Image>();
        [SerializeField] private List<Text> levelText = new List<Text>();

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            UISignals.Instance.onSetLevelValue += OnSetLevelValue;
            UISignals.Instance.onSetStageColor += OnSetStageColor;
        }

        private void OnSetStageColor(byte stageValue)
        {
            stageImages[stageValue].color = Color.red;
        }

        private void OnSetLevelValue(byte levelValue)
        {
            var additionalValue = ++levelValue;
            levelText[0].text = additionalValue.ToString();
            additionalValue++;
            levelText[1].text = additionalValue.ToString();
        }

        private void UnSubscribeEvent()
        {
            UISignals.Instance.onSetLevelValue -= OnSetLevelValue;
            UISignals.Instance.onSetStageColor -= OnSetStageColor;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }
    }
}
