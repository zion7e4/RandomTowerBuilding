using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EarthquakeManager : MonoBehaviour
{
    Building_Change building_Change;

    public float earthquakeDelayMin = 1f;
    public float earthquakeDelayMax = 3f;

    public GameObject warningUI;         // "���� ���!" �ؽ�Ʈ + ������ UI
    /*public AudioSource sirenAudio;       // ��� ���̷�
    public AudioSource rumbleAudio;      // ���� �߻� �� ������*/
    public Camera mainCamera;
    public float shakeDuration = 4f;
    public float shakeMagnitude = 0.07f;

    private bool earthquakeTriggered = false;


    void Start()
    {
        building_Change = FindObjectOfType<Building_Change>();
        if (warningUI != null)
        {
            warningUI.SetActive(false); // ���� �� ��� UI ����
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
        // 1~3�� ���� ������ �� ��� ǥ��
        float delay = Random.Range(earthquakeDelayMin, earthquakeDelayMax);
        yield return new WaitForSeconds(delay);

        // ��� UI �� ����
        warningUI.SetActive(true);
        //sirenAudio.Play();

        yield return new WaitForSeconds(2f);

        warningUI.SetActive(false);

        // ���� �߻�
        StartCoroutine(ShakeCamera());
        //rumbleAudio.Play();

        // ��� ��鸮�ų� �и��� ������ ������ ȣ��
        //SendMessage("OnEarthquake");  // ��: �� ��Ͽ� ��鸲 ����

        yield return new WaitForSeconds(shakeDuration);

        //rumbleAudio.Stop();

        // �ݺ� �����ϰ� �Ϸ��� �Ʒ� ���� �ּ� ó��
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