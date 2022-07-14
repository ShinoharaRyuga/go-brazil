using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>シーンを遷移する </summary>
public class SceneChange : MonoBehaviour
{
    /// <summary>シーンを遷移させる </summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
