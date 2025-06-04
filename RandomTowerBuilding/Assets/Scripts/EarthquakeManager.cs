using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EarthquakeManager : MonoBehaviour
{
    Building_Change building_Change;

    public float earthquakeDelayMin = 1f;
    public float earthquakeDelayMax = 3f;

    public GameObject warningUI;         // "지진 경고!" 텍스트 + 아이콘 UI
    /*public AudioSource sirenAudio;       // 경고 사이렌
    public AudioSource rumbleAudio;      // 지진 발생 시 진동음*/
    public Camera mainCamera;
    public float shakeDuration = 4f;
    public float shakeMagnitude = 0.07f;

    private bool earthquakeTriggered = false;


    void Start()
    {
        building_Change = FindObjectOfType<Building_Change>();
        if (warningUI != null)
        {
            warningUI.SetActive(false); // 시작 시 경고 UI 숨김
        }
    }

    void Update()
    {
        if (!earthquakeTriggered && (building_Change.blockcount >= 10 || building_Change.currentHeight >= 10f))
        {
            earthquakeTriggered = true;
            StartCoroutine(TriggerEarthquake());
        }
    }

    IEnumerator TriggerEarthquake()
    {
        // 1~3초 랜덤 딜레이 후 경고 표시
        float delay = Random.Range(earthquakeDelayMin, earthquakeDelayMax);
        yield return new WaitForSeconds(delay);

        // 경고 UI 및 사운드
        warningUI.SetActive(true);
        //sirenAudio.Play();

        yield return new WaitForSeconds(2f);

        warningUI.SetActive(false);

        // 지진 발생
        StartCoroutine(ShakeCamera());
        //rumbleAudio.Play();

        // 블록 흔들리거나 밀리는 로직은 별도로 호출
        //SendMessage("OnEarthquake");  // 예: 각 블록에 흔들림 적용

        yield return new WaitForSeconds(shakeDuration);

        //rumbleAudio.Stop();

        // 반복 가능하게 하려면 아래 라인 주석 처리
        // earthquakeTriggered = false;
    }

    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = mainCamera.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            mainCamera.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.localPosition = originalPos;
    }

}