using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    private Rigidbody2D rb;

    // 초기화: 데이터 기반 블록 설정
    public void Init(BlockData data)
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(data.width, data.height, 1f);

        // 기존 콜라이더 제거 후 새로 생성
        PolygonCollider2D oldCol = GetComponent<PolygonCollider2D>();
        if (oldCol != null) Destroy(oldCol);
        PolygonCollider2D newCol = gameObject.AddComponent<PolygonCollider2D>();

        // 마찰 적용
        PhysicsMaterial2D mat = Resources.Load<PhysicsMaterial2D>("BlockFriction");
        if (mat != null) newCol.sharedMaterial = mat;

        // 질량 계산 및 물리 설정
        rb.mass = data.height * data.width * data.density;
        rb.angularDamping = 2f;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}