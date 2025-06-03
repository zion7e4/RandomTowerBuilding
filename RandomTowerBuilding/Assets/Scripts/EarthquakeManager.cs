using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EarthquakeManager : MonoBehaviour
{
    Building_Change building_Change;
    MainCameraController mainCameraController;

    public float earthquakeDelayMin = 1f;
    public float earthquakeDelayMax = 3f;

    public GameObject warningUI;         // "���� ���!" �ؽ�Ʈ + ������ UI
    /*public AudioSource sirenAudio;       // ��� ���̷�
    public AudioSource rumbleAudio;      // ���� �߻� �� ������*/
    public Camera mainCamera;
    public float shakeDuration = 4f;
    public float shakeMagnitude = 0.07f;

    private bool earthquakeTriggered = false;

    Transform[] blocks;
    Vector3[] originPosition;
   
    private bool isShaking = false;

    void Start()
    {
        building_Change = FindObjectOfType<Building_Change>();
        mainCameraController = FindObjectOfType<MainCameraController>();

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
        OnEarthquake();

        /*GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            block.SendMessage("OnEarthquake");
        }*/
        //rumbleAudio.Play();

        // ��� ��鸮�ų� �и��� ������ ������ ȣ��
        //SendMessage("OnEarthquake");  // ��: �� ��Ͽ� ��鸲 ����

        yield return new WaitForSeconds(shakeDuration);

        //rumbleAudio.Stop();

        // �ݺ� �����ϰ� �Ϸ��� �Ʒ� ���� �ּ� ó��
        // earthquakeTriggered = false;
    }
    public void OnEarthquake()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeBlock());
        }
    }
    private IEnumerator ShakeBlock()
    {
        Debug.Log("Earthquake triggered, shaking blocks...");
        isShaking = true;
        float elapsed = 0f;

        blocks = mainCameraController.allBlocks.ToArray(); // MainCameraController���� ��� ����Ʈ ��������
        originPosition = new Vector3[blocks.Length];
        for(int i = 0; i < blocks.Length; i++)
        {
            originPosition[i] = blocks[i].position; // �� ����� �ʱ� ��ġ ����
        }

        while (elapsed < shakeDuration)
        {
            for(int i = 0; i < blocks.Length; i++)
            {
                float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
                Vector3 newPos= originPosition[i] + new Vector3(offsetX, 0, 0); // �� ����� �ʱ� ��ġ ���
                blocks[i].transform.position = newPos;
                Debug.Log("Block " + i + " moved to " + newPos);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
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