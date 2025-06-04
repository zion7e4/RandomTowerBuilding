using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Clips")]
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioClip sirenSound;
    [SerializeField] private AudioClip rumbleSound;
    [SerializeField] private AudioClip thunderSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDropSound() => PlayOneShot(dropSound);
    public void PlayLandSound() => PlayOneShot(landSound);
    public void PlayThunder() => PlayOneShot(thunderSound);
    public void PlaySirenLoop() => PlayLoop(sirenSound);
    public void StopLoop() => audioSource.Stop();
    public void PlayRumbleLoop() => PlayLoop(rumbleSound);

    private void PlayOneShot(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    private void PlayLoop(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StartThunderLoop(float minDelay = 10f, float maxDelay = 30f)
    {
        StartCoroutine(ThunderLoop(minDelay, maxDelay));
    }

    private IEnumerator ThunderLoop(float min, float max)
    {
        while (true)
        {
            float waitTime = Random.Range(min, max);
            yield return new WaitForSeconds(waitTime);
            PlayThunder();
        }
    }
}
