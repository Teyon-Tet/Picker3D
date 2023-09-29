using Cinemachine;
using Runtime.Signals;
using System;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera virtualCamera;


        #endregion

        #region
        private float3 _firsPosition;

        #endregion

        #endregion

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _firsPosition = transform.position;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnReset()
        {
            transform.position = _firsPosition;
        }

        private void OnSetCameraTarget()
        {
            //var player = FindObjectOfType<PlayerManager>().transform;
            //virtualCamera.Follow = player;
            //virtualCamera.LookAt = player;
        }

        private void UnSubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }


    }
}
