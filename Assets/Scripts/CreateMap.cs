using UnityEngine;

/// <summary>�}�b�v�𐶐�����@�_�|���@���g���Đ������Ă��� </summary>
public class CreateMap : MonoBehaviour
{
    [SerializeField, Min(7), Tooltip("�c�̒���")] int _rows = 7;
    [SerializeField, Min(7), Tooltip("���̒���")] int _columns = 7;
    [SerializeField, Tooltip("�}�b�v���`������I�u�W�F�N�g")] MapTip _mapTip = default;
    [SerializeField, Tooltip("�������ꂽ�}�b�v�̐e�I�u�W�F�N�g")] Transform _mapParent = default;
    [SerializeField, Tooltip("�}�b�v��]�̒��S")]Transform _mapRotationCenter = default;
    /// <summary>�������ꂽ�}�b�v�̃f�[�^ </summary>
    MapTip[,] _mapData = default;

    void Start()
    {
        _mapData = new MapTip[_rows, _columns];
        _mapRotationCenter.position = new Vector2(_rows / 2, _columns / 2);
        _mapRotationCenter.gameObject.name = "MapRotationCenter";
        MapCreator();
    }

    /// <summary>�}�b�v�𐶐����� </summary>
    public void MapCreator()
    {
        for (var r = 0; r < _rows; r++)     //�}�b�v�̊O����ǂŌł߂�
        {
            for (var c = 0; c < _columns; c++)
            {
                var tip = Instantiate(_mapTip, new Vector2(r, c), Quaternion.identity);
                tip.transform.SetParent(_mapParent);
                _mapData[r, c] = tip;

                if (r == 0 || r == _rows - 1 || c == 0 || c == _columns - 1)
                {
                    tip.Status = Status.Wall;
                }
            }
        }

        for (var r = 2; r < _rows - 1; r += 2)      //�ǂɂ���ʒu���擾���ǂɂ���
        {
            var isLast = false;
            var getdata = _mapData[0, 0];
            var direction = Direction.Up;

            if (r + 2 >= _rows - 1)
            {
                isLast = true;
            }

            for (var c = 2; c < _columns - 1; c += 2)
            { 
                _mapData[r, c].Status = Status.Wall;

                //�ŏ��Ɏ擾����tip�̏�Ԃ��ǂ��������蒼��
                do
                {
                    if (isLast)
                    {
                        direction = (Direction)Random.Range(0, 3);
                    }
                    else
                    {
                        direction = (Direction)Random.Range(0, 4);
                    }

                    getdata = GetMapTip(direction, r, c);
                }
                while (getdata.Status == Status.Wall);

                getdata.Status = Status.Wall;
            }
        }

        _mapParent.SetParent(_mapRotationCenter);
    }

    /// <summary>���߂�ꂽ������MapTip��z�񂩂�擾���l��Ԃ� </summary>
    /// <param name="direction">�擾�������</param>
    MapTip GetMapTip(Direction direction, int r, int c)
    {
        switch (direction)
        {
            case Direction.Up:
                return _mapData[r + 1, c];
            case Direction.Down:
                return _mapData[r - 1, c];
            case Direction.Left:
                return _mapData[r, c - 1];
            case Direction.Right:
                return _mapData[r, c + 1];
            default:
                return _mapData[r, c];
        }
    }
}

/// <summary>�ǂɂ������ </summary>
public enum Direction
{
    Down = 0,
    Left = 1,
    Right = 2,
    Up = 3,
}

