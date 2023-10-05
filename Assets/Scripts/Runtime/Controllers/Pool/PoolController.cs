using DG.Tweening;
using Runtime.Data.UnityObjects;
using Runtime.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        //[SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>;
        [SerializeField] private Text poolText;
        [SerializeField] private int poolValue;
        [SerializeField] private byte stageID;
        [SerializeField] private new Renderer renderer;

        #endregion

        #region Private Variables

        private PoolData _data;
        private byte _collectedCount;

        private readonly string _collectable = "Collectable";

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
        }

        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;
        }

        private void OnChangePoolColor(byte stageValue)
        {
            if(stageValue != stageID) return;
            
            renderer.material.DOColor(new Color(0.160f, 0.60f, 017662118f), 1).SetEase(Ease.Linear);
            
        }

        private void OnActivateTweens(byte stageValue)
        {
            if (stageValue != stageID) return;
            {
                /*
                foreach (var tween in tweens)
                {
                    tween.DOPlay();
                }
                */
            }
        }

        private void Start()
        {
            SetRequiredAmountText();
        }

        private void SetRequiredAmountText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}";
        }

        public bool TakeResults(byte managerStageValue)
        {
            if(stageID == managerStageValue)
            {
                return _collectedCount >= _data.RequiredObjectCount;
            }
            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_collectable)) return;
            IncreaseCollectedAmount();
            SetCollectedAmountToPool();
        }

        private void SetCollectedAmountToPool()
        {
            poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";
        }

        private void IncreaseCollectedAmount()
        {
            _collectedCount++;
        }

        private void DecreaseCollectedAmount()
        {
            _collectedCount--;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_collectable)) return;
            DecreaseCollectedAmount();
            SetCollectedAmountToPool();
        }
    }

}
