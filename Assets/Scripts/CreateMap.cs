using UnityEngine;

/// <summary>マップを生成する　棒倒し法を使って生成している </summary>
public class CreateMap : MonoBehaviour
{
    [SerializeField, Min(7), Tooltip("縦の長さ")] int _rows = 7;
    [SerializeField, Min(7), Tooltip("横の長さ")] int _columns = 7;
    [SerializeField, Tooltip("マップを形成するオブジェクト")] MapTip _mapTip = default;
    [SerializeField, Tooltip("生成されたマップの親オブジェクト")] Transform _mapParent = default;
    [SerializeField, Tooltip("マップ回転の中心")]Transform _mapRotationCenter = default;
    /// <summary>生成されたマップのデータ </summary>
    MapTip[,] _mapData = default;

    void Start()
    {
        _mapData = new MapTip[_rows, _columns];
        _mapRotationCenter.position = new Vector2(_rows / 2, _columns / 2);
        _mapRotationCenter.gameObject.name = "MapRotationCenter";
        MapCreator();
    }

    /// <summary>マップを生成する </summary>
    public void MapCreator()
    {
        for (var r = 0; r < _rows; r++)     //マップの外周を壁で固める
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
}

/// <summary>壁にする方向 </summary>
public enum Direction
{
    Down = 0,
    Left = 1,
    Right = 2,
    Up = 3,
}

