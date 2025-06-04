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
            // 타이틀, 로비 외 씬이면 BGM 멈춤
            if (audioSource.isPlaying)
            {
                audioSource.Pause(); // Stop() 대신 Pause()로 이어서 재생 가능
            }
        }
    }

    private void PlayOrResumeBGM(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource.clip == clip)
        {
            // 같은 BGM이면 재생만 재개
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            // 다른 BGM이면 교체 후 재생
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
