using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void LoadNextStage()
    {
        if (GameManager.LastPlayedStage == "Stage1")
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (GameManager.LastPlayedStage == "Stage2")
        {
            SceneManager.LoadScene("Stage3");
        }
    }
    public void OnReplayButtonClicked()
    {
        if (!string.IsNullOrEmpty(GameManager.LastPlayedStage))
        {
            SceneManager.LoadScene(GameManager.LastPlayedStage);
        }
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // 에디터에서 플레이 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드에서 종료
        Application.Quit();
#endif
    }
}