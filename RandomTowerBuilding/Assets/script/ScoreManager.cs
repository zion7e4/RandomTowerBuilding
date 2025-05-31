using UnityEngine;
using TMPro;

/// ������ ����� ������ �ְ� ���̸� �����ϰ� UI�� ǥ����
public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI blockCountText;
    public TextMeshProUGUI heightText;

    [Header("���� ���� (floor)")]
    public Transform floor;

    [Header("��ǥ�� (Ŭ���� ����)")]
    public int targetBlockCount = 15;
    public float targetHeight = 15f;

    private int blockCount = 0;
    private float maxHeight = 0f;

    /// ����� ������ �� ȣ���
    public void RegisterBlock(Transform block)
    {
        blockCount++;

        float blockTop = block.position.y + (block.GetComponent<Renderer>().bounds.size.y / 2f);
        float baseY = floor.position.y;
        float height = (blockTop - baseY) * 100f;

        if (height > maxHeight)
            maxHeight = height;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (blockCountText != null)
            blockCountText.text = $"blockCount: {blockCount}";

        if (heightText != null)
            heightText.text = $"height: {maxHeight:F2}m";
    }

    public bool CheckClearCondition()
    {
        return blockCount >= targetBlockCount || maxHeight >= targetHeight;
    }

    public void ResetScore()
    {
        blockCount = 0;
        maxHeight = 0f;
        UpdateUI();
    }
}