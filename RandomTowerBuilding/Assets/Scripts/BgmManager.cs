using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManager : MonoBehaviour
{
    public static BgmManager Instance;

    public AudioClip titleBGM;
    public AudioClip lobbyBGM;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title")
        {
            PlayOrResumeBGM(titleBGM);
        }
        else if (scene.name == "Lobby")
        {
            PlayOrResumeBGM(lobbyBGM);
        }
        else
        {
            // Ÿ��Ʋ, �κ� �� ���̸� BGM ����
            if (audioSource.isPlaying)
            {
                audioSource.Pause(); // Stop() ��� Pause()�� �̾ ��� ����
            }
        }
    }

    private void PlayOrResumeBGM(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource.clip == clip)
        {
            // ���� BGM�̸� ����� �簳
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            // �ٸ� BGM�̸� ��ü �� ���
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
