using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI ǥ��")]
    public TextMeshProUGUI blockCountText;
    public TextMeshProUGUI heightText;

    private int blockCount = 0;
    private float maxHeight = 0f;

    public int CurrentBlockCount => blockCount;
    public float CurrentHeight => maxHeight;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �ߺ� ����
        }
        else
        {
            Instance = this;
        }
    }

    public void RegisterBlock(Transform block)
    {
        blockCount++;

        float blockTop = block.position.y;
        Renderer rend = block.GetComponent<Renderer>();
        if (rend != null)
        {
            blockTop = rend.bounds.max.y;
        }

        if (blockTop > maxHeight)
        {
            maxHeight = blockTop;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (blockCountText != null)
            blockCountText.text = $"blockCount: {blockCount}";

        if (heightText != null)
            heightText.text = $"height: {maxHeight:F2}m";
    }
}