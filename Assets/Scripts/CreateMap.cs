using UnityEngine;

/// <summary>�}�b�v�𐶐�����@�_�|���@���g���Đ������Ă��� </summary>
public class CreateMap : MonoBehaviour
{
    [SerializeField, Tooltip("�}�b�v�̐������@")] CreateMode _createMode = CreateMode.Ramdom;
    [SerializeField, Min(7), Tooltip("�c�̒���")] int _rows = 7;
    [SerializeField, Min(7), Tooltip("���̒���")] int _columns = 7;
    [SerializeField, Range(3, 10), Tooltip("�ʘH�̕�")] int _roadWidth = 1;
    [SerializeField, Tooltip("�}�b�v���`������I�u�W�F�N�g")] MapTip _mapTip = default;
    [SerializeField, Tooltip("�������ꂽ�}�b�v�̐e�I�u�W�F�N�g")] Transform _mapParent = default;
    [SerializeField, Tooltip("�}�b�v��]�̒��S")] Transform _mapRotationCenter = default;
    [SerializeField, Tooltip("�ǂݍ��񂾃}�b�v�f�[�^���擾����")] GetCSVMapData _getCSVMapData = default;
    [SerializeField] GameManager _gameManager = default;
    /// <summary>���߂ă}�b�v�𐶐��������ǂ��� </summary>
    bool _isFirstCreate = true;
    /// <summary>�������ꂽ�}�b�v�̃f�[�^ </summary>
    MapTip[,] _mapData = default;

    void Start()
    {
        _mapData = new MapTip[_rows, _columns];
        MapCreate();
        _gameManager.CreateMap += MapCreate;
    }

    /// <summary>�}�b�v�������_���������� </summary>
    public void RamdomMapCreator()
    {
        if (!_isFirstCreate)
        {
            ResetMapData();
        }

        for (var r = 0; r < _rows; r++)     //�}�b�v�̊O����ǂŌł߂�
        {
            for (var c = 0; c < _columns; c++)
            {
                var tip = MapTipGenerator(r, c);

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
                        direction = (Direction)UnityEngine.Random.Range(0, 3);
                    }
                    else
                    {
                        direction = (Direction)UnityEngine.Random.Range(0, 4);
                    }

                    getdata = GetMapTip(direction, r, c);
                }
                while (getdata.Status == Status.Wall);

                getdata.Status = Status.Wall;
            }
        }

        SetExit();

        _mapRotationCenter.position = _gameManager.Player.transform.position;
        _mapParent.transform.SetParent(_mapRotationCenter);
        _isFirstCreate = false;
    }

    /// <summary>CSV����擾�����}�b�v�f�[�^���g�p���ă}�b�v���쐬���� </summary>
    void DataCreateMap()
    {
        var getMapData = _getCSVMapData.GetData();
        var startPoint = Vector2.zero;

        for (var r = 0; r < _getCSVMapData.Rows; r++)
        {
            for (var c = 0; c < _getCSVMapData.Columns; c++)
            {
                var tip = MapTipGenerator(r, c);

                if (getMapData[r, c] == 0)
                {
                    tip.Status = Status.Road;
                }
                else if (getMapData[r, c] == 1)
                {
                    tip.Status = Status.Wall;
                }
                else if (getMapData[r, c] == 2)
                {
                    startPoint = new Vector2(tip.transform.position.x, tip.transform.position.y + 5);
                }
            }
        }

        _gameManager.Player.transform.position = startPoint;
        _mapRotationCenter.position = _gameManager.Player.transform.position;
        _mapParent.transform.SetParent(_mapRotationCenter);
    }

    /// <summary>4�����ɏo�������쐬����</summary>
    void SetExit()
    {
        _mapParent.transform.SetParent(null);

        for (var i = 0; i < 4; i++)    
        {
            var direction = (Direction)i;
            var index = 0;
            var result = false;
          
            while (!result)     //�o���ɂ������}�X�ڂ��ǂŕ����Ă�����ʂ̏o���ɂ���
            {
                index = Random.Range(1, _rows - 1);
                result = CheckAroundTip(index, direction);
            }

            switch (direction)      //�e�ǂɏo�����쐬����
            {
                case Direction.Up:
                    _mapData[0, index].Status = Status.Road;
                    _mapData[0, index - 1].Status = Status.GoalWall;
                    _mapData[0, index + 1].Status = Status.GoalWall;
                    break;
                case Direction.Down:
                    _mapData[_rows - 1, index].Status = Status.Road;
                    _mapData[_rows - 1, index - 1].Status = Status.GoalWall;
                    _mapData[_rows - 1, index + 1].Status = Status.GoalWall;
                    break;
                case Direction.Right:
                    _mapData[index, _columns - 1].Status = Status.Road;
                    _mapData[index - 1, _columns - 1].Status = Status.GoalWall;
                    _mapData[index + 1, _columns - 1].Status = Status.GoalWall;
                    //���@�̈ʒu��ύX����
                    var startPoint = new Vector2(_mapData[index, _columns - 1].gameObject.transform.position.x, _mapData[index, _columns - 1].gameObject.transform.position.y + 5);
                    _gameManager.SetPlayerStartPoition(startPoint);
                    break;
                case Direction.Left:
                    _mapData[index, 0].Status = Status.Road;
                    _mapData[index - 1, 0].Status = Status.GoalWall;
                    _mapData[index + 1, 0].Status = Status.GoalWall;

                    break;
            }
        }
    }

    /// <summary>
    /// �o���ɂ������}�X�ڂ��ǂŕ����Ă��邩���ׂ�
    /// �����Ă�����false��Ԃ�
    /// </summary>
    bool CheckAroundTip(int targetIndex, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                if (_mapData[1, targetIndex].Status == Status.Wall)
                {
                    return false;
                }
                return true;
            case Direction.Down:
                if (_mapData[_rows - 2, targetIndex].Status == Status.Wall)
                {
                    return false;
                }
                return true;
            case Direction.Right:
                if (_mapData[targetIndex, _columns - 2].Status == Status.Wall)
                {
                    return false;
                }
                return true;
            case Direction.Left:
                if (_mapData[targetIndex, 1].Status == Status.Wall)
                {
                    return false;
                }
                return true;
            default:
                return false;
        }
    }

    /// <summary>_createMode�ɂ���ă}�b�v�������@��ύX���������� </summary>
    void MapCreate()
    {
        if (_createMode == CreateMode.Ramdom)
        {
            RamdomMapCreator();
        }
        else if (_createMode == CreateMode.MapData)
        {
            DataCreateMap();
        }
    }

    /// <summary>
    /// �}�b�v���Đ�������ׂ�_mapData�̒��g���폜���� 
    /// </summary>
    void ResetMapData()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                if (_mapData[r, c].gameObject != null)
                {
                    Destroy(_mapData[r, c].gameObject);
                }
            }
        }
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

    /// <summary>
    /// �w�肵���ʒu��MapTip�𐶐����z��ɑ������ 
    /// ��������MapTip�ɃX�e�[�^�X��ݒ肷��ׂ�MapTip��Ԃ�
    /// </summary>
    /// <returns>��������MapTip</returns>
    MapTip MapTipGenerator(int r, int c)
    {
        var tip = Instantiate(_mapTip, new Vector2(r * _roadWidth, c * _roadWidth), Quaternion.identity);
        tip.transform.localScale = new Vector3 (_roadWidth, _roadWidth, 0);
        tip.transform.transform.SetParent(_mapParent);
        _mapData[r, c] = tip;

        return tip;
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

/// <summary>�}�b�v�̐������@ </summary>
public enum CreateMode
{
    Ramdom = 0,
    /// <summary>CSV�t�@�C���Ȃǂ���}�b�v�f�[�^��ǂݍ��� </summary>
    MapData = 1,
}

