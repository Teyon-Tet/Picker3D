using Cinemachine;
using UnityEngine;

namespace Runtime.Extentions
{
    public enum CinemachineLockAxis
    {
        x,
        y,
        z
    }

    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]

    public class LockCinemachineAxis : CinemachineExtension
    {

        [SerializeField]private CinemachineLockAxis lockAxis;

        [Tooltip("Lock the Cinemachine Virtual camera's X Axis position with this specific value")]
        public float XClampValue = 0;
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            switch (lockAxis)
            {
                case CinemachineLockAxis.x:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.x = XClampValue;
                        state.RawPosition = pos;
                    }
                    break;
                case CinemachineLockAxis.y:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.y = XClampValue;
                        state.RawPosition = pos;
                    }
                    break;
                case CinemachineLockAxis.z:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.z = XClampValue;
                        state.RawPosition = pos;
                    }
                    break;
                default:
                    Debug.Log("burasi");
                    break;
            }
        }
    }
}
