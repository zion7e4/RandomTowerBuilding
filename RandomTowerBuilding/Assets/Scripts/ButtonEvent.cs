using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    /*public void OnReplayButtonClicked()
    {
        if (!string.IsNullOrEmpty(carmovement.LastPlayedStage))
        {
            SceneManager.LoadScene(carmovement.LastPlayedStage);
        }
    }

    public void NextStage()
    {
        if (carmovement.LastPlayedStage == "Stage1")
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (carmovement.LastPlayedStage == "Stage2")
        {
            SceneManager.LoadScene("Stage3");
        }
    }*/

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