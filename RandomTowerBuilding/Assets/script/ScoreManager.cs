using UnityEngine;
using TMPro;

/// ������ ����� ������ �ְ� ���̸� �����ϰ� UI�� ǥ����
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI blockCountText;
    public TextMeshProUGUI heightText;
    Building_Change bc;

    [Header("���� ���� (floor)")]
    public Transform floor;

    [Header("��ǥ�� (Ŭ���� ����)")]
    public int targetBlockCount = 15;
    public float targetHeight = 15f;
    public float maxHeight = 0f;

    void Update()
    {
        UpdateUI();
    }

    void Start()
    {
        bc = FindObjectOfType<Building_Change>();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// ����� ������ �� ȣ���
    public void RegisterBlock(Transform block)
    {

        float blockTop = block.position.y + (block.GetComponent<Renderer>().bounds.size.y / 2f);
        float baseY = floor.position.y;
        float height = (blockTop - baseY) * 100f;

        if (height > maxHeight)
            maxHeight = height;
    }

    void UpdateUI()
    {
        if (blockCountText != null)
            blockCountText.text = $"blockCount: {bc.blockcount}";

        if (heightText != null)
            heightText.text = $"height: {maxHeight:F2}m";
    }

    public void ResetScore()
    {
        bc.blockcount = 0;
        maxHeight = 0f;
        UpdateUI();
    }
}