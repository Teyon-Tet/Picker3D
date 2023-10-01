using DG.Tweening;
using Runtime.Data.ValueObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Renderer renderer;
        [SerializeField] private Text scaleText;
        [SerializeField] private ParticleSystem confetti;

        #endregion

        #region Private Variables

        private PlayerMeshData _data;

        #endregion

        #endregion

        internal void SetData(PlayerMeshData data)
        {
            _data = data;
        }

        internal void ScaleUpPlayer()
        {
            renderer.gameObject.transform.DOScaleX(_data.ScaleCounter, 1).SetEase(Ease.Flash);
        }

        internal void ShowUpText()
        {
            scaleText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
            {
                scaleText.DOFade(0, 30f).SetDelay(.35f);
                scaleText.rectTransform.DOAnchorPosY(1f, .65f).SetEase(Ease.Linear);
            });
        }

        internal void Confetti()
        {
            confetti.Play();
        }

        internal void OnReset()
        { 
            renderer.gameObject.transform.DOScaleX(1,1).SetEase(Ease.Linear);
        }

    }
}
