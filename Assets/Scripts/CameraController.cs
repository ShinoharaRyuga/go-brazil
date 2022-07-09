using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineTargetGroup _targetGroup = default;

    public void SetCameraTargets(Transform[] targets)
    {
        _targetGroup.m_Targets = null;
        _targetGroup.m_Targets = new CinemachineTargetGroup.Target[]
        {
            new CinemachineTargetGroup.Target
            {
                target = targets[0],
                weight = 4.5f,
                radius = 2,
            },
            new CinemachineTargetGroup.Target
            {
                target = targets[1],
                weight = 1.5f,
                radius = 2,
            },
            new CinemachineTargetGroup.Target
            {
                target = targets[2],
                weight = 2,
                radius = 2,
            },
            new CinemachineTargetGroup.Target
            {
                target = targets[3],
                weight = 4.5f,
                radius = 2,
            }
        };


    }

    public void ChangeCameraRotate(float rotateValue)
    {
        var changeValue = Quaternion.Euler(0, 0, rotateValue);
        transform.rotation = changeValue;
    }
}
