using UnityEngine;
using TMPro;

/// 생성된 블록의 개수와 최고 높이를 관리하고 UI에 표시함
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI blockCountText;
    public TextMeshProUGUI heightText;
    Building_Change bc;

    [Header("기준 지면 (floor)")]
    public Transform floor;

    [Header("목표값 (클리어 조건)")]
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

    /// 블록이 생성될 때 호출됨
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