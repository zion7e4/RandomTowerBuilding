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
        // �����Ϳ��� �÷��� ��� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���忡�� ����
        Application.Quit();
#endif
    }
}