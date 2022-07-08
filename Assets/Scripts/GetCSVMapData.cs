using System;
using System.IO;
using UnityEngine;

public class GetCSVMapData : MonoBehaviour
{
    [SerializeField] TextAsset _csvMapData = default;
    int _rows = 0;
    int _columns = 0;

    public int Rows { get => _rows; }
    public int Columns { get => _columns; }

    public int[,] GetData()
    {
        var sr = new StringReader(_csvMapData.text);
        sr.ReadLine();
        var length = sr.ReadLine().Split(',');
        _rows = int.Parse(length[0]);
        _columns = int.Parse(length[1]);
        var data = new int[_rows, _columns];
        var currentRow = 0;

        while (true)
        {
            var line = sr.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            var numbers = Array.ConvertAll(line.Split(','), s => int.Parse(s));

            for (var i = 0; i < _columns; i++)
            {
                data[currentRow, i] = numbers[i];
            }

            currentRow++;
        }

        Debug.Log("end");
        return data;
    }
}
