using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>�V�[����J�ڂ��� </summary>
public class SceneChange : MonoBehaviour
{
    /// <summary>�V�[����J�ڂ����� </summary>
    /// <param name="sceneName">�J�ڐ�̃V�[����</param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
