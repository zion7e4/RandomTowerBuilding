using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private IEnumerator PlaySoundAndLoadScene(string sceneName)
    {
        if (buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);

        yield return new WaitForSeconds(buttonClickSound.length);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator PlaySoundAndQuit()
    {
        if (buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);

        yield return new WaitForSeconds(buttonClickSound.length);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ���� ��� �� �� ��ȯ �Լ���
    public void LoadNextStage()
    {
        if (GameManager.LastPlayedStage == "Stage1")
        {
            StartCoroutine(PlaySoundAndLoadScene("Stage2"));
        }
        else if (GameManager.LastPlayedStage == "Stage2")
        {
            StartCoroutine(PlaySoundAndLoadScene("Stage3"));
        }
    }

    public void OnReplayButtonClicked()
    {
        if (!string.IsNullOrEmpty(GameManager.LastPlayedStage))
        {
            StartCoroutine(PlaySoundAndLoadScene(GameManager.LastPlayedStage));
        }
    }

    public void SceneLoad(string sceneName)
    {
        StartCoroutine(PlaySoundAndLoadScene(sceneName));
    }

    public void QuitGame()
    {
        StartCoroutine(PlaySoundAndQuit());
    }

    // **Ŭ�� �� ���常 �ٷ� ����ϴ� �Լ�**
    public void PlayClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
