using UnityEngine;

/// <summary>
/// �}�b�v����Ɉړ�������
/// ��Ɉړ������邱�Ƃŗ�������\������B
/// </summary>
public class MapMove : MonoBehaviour
{
    [SerializeField, Min(0.01f),Tooltip("�ړ����x")] float _moveValue = 0.1f;
    [SerializeField] GameManager _gameManager;

    void Update()
    {
        if (_gameManager.IsGameing)
        {
            transform.Translate(0, _moveValue, 0, Space.World);
        }
    }
}
