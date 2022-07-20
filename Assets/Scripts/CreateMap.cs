using UnityEngine;

/// <summary>マップを生成する　棒倒し法を使って生成している </summary>
public class CreateMap : MonoBehaviour
{
    [SerializeField, Tooltip("マップの生成方法")] CreateMode _createMode = CreateMode.Ramdom;
    [SerializeField, Min(7), Tooltip("縦の長さ")] int _rows = 7;
    [SerializeField, Min(7), Tooltip("横の長さ")] int _columns = 7;
    [SerializeField, Range(3, 10), Tooltip("通路の幅")] int _roadWidth = 1;
    [SerializeField, Tooltip("マップを形成するオブジェクト")] MapTip _mapTip = default;
    [SerializeField, Tooltip("生成されたマップの親オブジェクト")] Transform _mapParent = default;
    [SerializeField, Tooltip("マップ回転の中心")] Transform _mapRotationCenter = default;
    [SerializeField, Tooltip("読み込んだマップデータを取得する")] GetCSVMapData _getCSVMapData = default;
    [SerializeField] GameManager _gameManager = default;
    /// <summary>初めてマップを生成したかどうか </summary>
    bool _isFirstCreate = true;
    /// <summary>生成されたマップのデータ </summary>
    MapTip[,] _mapData = default;

    void Start()
    {
        _mapData = new MapTip[_rows, _columns];
        MapCreate();
        _gameManager.CreateMap += MapCreate;
    }

    /// <summary>マップをランダム生成する </summary>
    public void RamdomMapCreator()
    {
        if (!_isFirstCreate)
        {
            ResetMapData();
        }

        for (var r = 0; r < _rows; r++)     //マップの外周を壁で固める
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

        for (var r = 2; r < _rows - 1; r += 2)      //壁にする位置を取得し壁にする
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

                //最初に取得したtipの状態が壁だったらやり直す
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

    /// <summary>CSVから取得したマップデータを使用してマップを作成する </summary>
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

    /// <summary>4方向に出入口を作成する</summary>
    void SetExit()
    {
        _mapParent.transform.SetParent(null);

        for (var i = 0; i < 4; i++)    
        {
            var direction = (Direction)i;
            var index = 0;
            var result = false;
          
            while (!result)     //出口にしたいマス目が壁で閉じられていたら別の出口にする
            {
                index = Random.Range(1, _rows - 1);
                result = CheckAroundTip(index, direction);
            }

            switch (direction)      //各壁に出口を作成する
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
                    //自機の位置を変更する
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
    /// 出口にしたいマス目が壁で閉じられているか調べる
    /// 閉じられていたらfalseを返す
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

    /// <summary>_createModeによってマップ生成方法を変更し生成する </summary>
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
    /// マップを再生成する為に_mapDataの中身を削除する 
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

    /// <summary>決められた方向のMapTipを配列から取得し値を返す </summary>
    /// <param name="direction">取得する方向</param>
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
    /// 指定した位置にMapTipを生成し配列に代入する 
    /// 生成したMapTipにステータスを設定する為にMapTipを返す
    /// </summary>
    /// <returns>生成したMapTip</returns>
    MapTip MapTipGenerator(int r, int c)
    {
        var tip = Instantiate(_mapTip, new Vector2(r * _roadWidth, c * _roadWidth), Quaternion.identity);
        tip.transform.localScale = new Vector3 (_roadWidth, _roadWidth, 0);
        tip.transform.transform.SetParent(_mapParent);
        _mapData[r, c] = tip;

        return tip;
    }
}

/// <summary>壁にする方向 </summary>
public enum Direction
{
    Down = 0,
    Left = 1,
    Right = 2,
    Up = 3,
}

/// <summary>マップの生成方法 </summary>
public enum CreateMode
{
    Ramdom = 0,
    /// <summary>CSVファイルなどからマップデータを読み込む </summary>
    MapData = 1,
}

