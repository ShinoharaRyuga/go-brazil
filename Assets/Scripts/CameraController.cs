using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void ChangeCameraRotate(float rotateValue)
    {
        var changeValue = Quaternion.Euler(0, 0, rotateValue);
        transform.rotation = changeValue;
    }
}
