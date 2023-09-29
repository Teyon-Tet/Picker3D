using Runtime.Enums;
using Runtime.Managers;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIEventSubscriptionTypes type;
        [SerializeField] private Button button;

        #endregion

        #region Private Variables

        [SerializeField] private UIManager _manager;

        [SerializeField] private UnityAction TextActionAction = delegate { };
        [SerializeField] private UnityEvent TestActionEvent = new UnityEvent();


        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch(type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.AddListener(_manager.Play);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.AddListener(_manager.NextLevel); 
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.AddListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnsubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.RemoveListener(_manager.Play);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.RemoveListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

    }
}
