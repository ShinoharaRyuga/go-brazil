using UnityEngine;

/// <summary>ƒQ[ƒ€Œ‹‰Ê‚ğ•Û‘¶‚·‚é </summary>
public class ResultManager
{
    static private ResultManager _instance = new ResultManager();
    static public ResultManager Instance => _instance;

    Countries _startCountry = Countries.None;
    Countries _goalCountry = Countries.None;

    public Countries StartCountry { get => _startCountry; set => _startCountry = value; }
    public Countries GoalCountry { get => _goalCountry; set => _goalCountry = value; }
}
