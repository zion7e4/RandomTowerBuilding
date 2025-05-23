using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Block/Create Block Data")]
public class BlockData : ScriptableObject
{
    [Header("고유 식별자")]
    public string id; // (대륙_명소이름)
    [Header("표시 이름")]
    public string blockName;

    [Header("높이")]
    public float height; // 단위: unit
    [Header("너비")]
    public float width;  // 단위: unit

    [Header("질량 보정 계수")]
    public float density;
    [Header("중력 영향도")]
    public float gravityPower = 1.0f;

    public GameObject prefab; // 이 블럭에 연결될 프리팹
}