using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// ���U���g�V�[���ɂ���e�L�X�g��C���[�W�ɃQ�[���̌��ʂ𔽉f������ 
/// </summary>
public class SetupResultScene : MonoBehaviour
{
    [SerializeField] TMP_Text _startCountry = default;
    [SerializeField] TMP_Text _goalCountry = default;
    [SerializeField] TMP_Text _commentText = default;
    [SerializeField, Tooltip("�X�^�[�g���̍����摜��\��t����")] Image _startCountryFlag = default;
    [SerializeField, Tooltip("�S�[�����̍����摜��\��t����")] Image _goalCountryFlag = default;

    [SerializeField, Tooltip("�e���̍����摜")] List<Sprite> _flagSprites = new List<Sprite>();
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

    /// <summary>�Q�[�����ʂɍ��킹�č����摜��\��t���� </summary>
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
