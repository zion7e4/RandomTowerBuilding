using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Block/Create Block Data")]
public class BlockData : ScriptableObject
{
    [Header("���� �ĺ���")]
    public string id; // (���_����̸�)
    [Header("ǥ�� �̸�")]
    public string blockName;

    [Header("����")]
    public float height; // ����: unit
    [Header("�ʺ�")]
    public float width;  // ����: unit

    [Header("���� ���� ���")]
    public float density;
    [Header("�߷� ���⵵")]
    public float gravityPower = 1.0f;

    public GameObject prefab; // �� ���� ����� ������
}