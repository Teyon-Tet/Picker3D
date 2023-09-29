using Runtime.Commands.Player;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public byte StageValue;

        internal ForceBallsToPoolCommand ForceCommand;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerPhysicsController physicsController;

        #region Private Variables

        private PlayerData _data;

        #endregion

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPlayerData();
            SendDataToControllers();
            Init();
        }

        private PlayerData GetPlayerData()
        {
           return Resources.Load<CD_Player>("Data/PlayerData").Data;
        }

        private void SendDataToControllers()
        {
            movementController.SetData(_data);
            meshController.SetData(_data);
        }

        private void Init()
        {
            ForceCommand = new ForceBallsToPoolCommand(this, _data.ForceData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnInputTaken;
            InputSignals.Instance.onInputReleased += OnInputReleased;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            UISignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnFinishAreaEntered()
        {
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            //mini game yazilmali,
        }

        private void OnStageAreaSuccessful(byte value)
        {
            StageValue = (byte)++value;
        }

        private void OnStageAreaEntered()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputParams(inputParams);
        }

        private void OnInputReleased()
        {
            movementController.IsReadyToMove(false);
        }

        private void OnInputTaken()
        {
            movementController.IsReadyToMove(true);
        }

        private void OnReset()
        {
            StageValue = 0;
            movementController.OnReset();
            physicsController.OnReset();
            meshController.OnReset();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnInputTaken;
            InputSignals.Instance.onInputReleased -= OnInputReleased;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
    }
}
