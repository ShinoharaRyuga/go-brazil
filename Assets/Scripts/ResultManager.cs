using UnityEngine;

/// <summary>�Q�[�����ʂ�ۑ����� </summary>
public class ResultManager
{
    static private ResultManager _instance = new ResultManager();
    static public ResultManager Instance => _instance;

    Countries _startCountry = Countries.None;
    Countries _goalCountry = Countries.None;

    public Countries StartCountry { get => _startCountry; set => _startCountry = value; }
    public Countries GoalCountry { get => _goalCountry; set => _goalCountry = value; }
}
