using System.Collections;
using UnityEngine;

public class EarthquakeManager : MonoBehaviour
{
    Building_Change building_Change;
    MainCameraController mainCameraController;

    public float earthquakeDelayMin = 1f;
    public float earthquakeDelayMax = 3f;

    public GameObject warningUI;
    public Camera mainCamera;
    public float shakeDuration = 4f;
    public float shakeMagnitude = 0.07f;

    private bool earthquakeTriggered = false;
    private bool isShaking = false;

    Transform[] blocks;
    Vector3[] originPosition;

    void Start()
    {
        building_Change = FindObjectOfType<Building_Change>();
        mainCameraController = FindObjectOfType<MainCameraController>();

        if (warningUI != null)
        {
            warningUI.SetActive(false);
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
        float delay = Random.Range(earthquakeDelayMin, earthquakeDelayMax);
        yield return new WaitForSeconds(delay);

        warningUI.SetActive(true);
        AudioManager.Instance.PlaySirenLoop();

        yield return new WaitForSeconds(2f);

        warningUI.SetActive(false);
        AudioManager.Instance.StopLoop(); // 사이렌 정지

        yield return new WaitForSeconds(0.5f); // 살짝 텀 주고

        AudioManager.Instance.PlayRumbleLoop(); // 지진 소리 시작
        StartCoroutine(ShakeCamera());
        StartCoroutine(ShakeBlock());

        yield return new WaitForSeconds(shakeDuration);

        AudioManager.Instance.StopLoop(); // 지진 소리 종료
    }

    IEnumerator ShakeBlock()
    {
        isShaking = true;
        float elapsed = 0f;

        blocks = mainCameraController.allBlocks.ToArray();
        originPosition = new Vector3[blocks.Length];
        for (int i = 0; i < blocks.Length; i++)
        {
            originPosition[i] = blocks[i].position;
        }

        while (elapsed < shakeDuration)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
                Vector3 newPos = originPosition[i] + new Vector3(offsetX, 0, 0);
                blocks[i].transform.position = newPos;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
    }

    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = mainCamera.transform.localPosition;

        float elapsed = 0f;

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
