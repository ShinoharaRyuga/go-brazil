using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// リザルトシーンにあるテキストやイメージにゲームの結果を反映させる 
/// </summary>
public class SetupResultScene : MonoBehaviour
{
    [SerializeField] TMP_Text _startCountry = default;
    [SerializeField] TMP_Text _goalCountry = default;
    [SerializeField] TMP_Text _commentText = default;
    [SerializeField, Tooltip("スタート国の国旗画像を貼り付ける")] Image _startCountryFlag = default;
    [SerializeField, Tooltip("ゴール国の国旗画像を貼り付ける")] Image _goalCountryFlag = default;

    [SerializeField, Tooltip("各国の国旗画像")] List<Sprite> _flagSprites = new List<Sprite>();
    private void Start()
    {
        var startCountry = ResultManager.Instance.StartCountry.ToString();
        var goalCountry = ResultManager.Instance.GoalCountry.ToString();
        _startCountry.text = startCountry;
        _goalCountry.text = goalCountry;
        SetFlag(startCountry, goalCountry);

        if (startCountry == goalCountry)
        {
            _commentText.text = "??????";
        }
    }

    /// <summary>ゲーム結果に合わせて国旗画像を貼り付ける </summary>
    void SetFlag(string startCountryName, string goalCountryName)
    {
        foreach (var country in _flagSprites)
        {
            if (country.name == startCountryName)
            {
                _startCountryFlag.sprite = country;
            }

            if (country.name == goalCountryName)
            {
                _goalCountryFlag.sprite = country;
            }
        }
    }
}
