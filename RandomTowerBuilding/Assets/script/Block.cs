using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    private Rigidbody2D rb;

    // 목표 회전 각도
    private float targetAngle = 0f;

    // 회전 속도
    private float rotateSpeed = 300f;

    // 블록이 멈췄는지 여부
    private bool isStopped = false;

    // 정지 감지용 타이머
    private float stopTimer = 0f;

    // 자동 낙하 대기 타이머
    private float dropTimer = 0f;

    // 자동 낙하까지의 대기 시간
    private float dropDelay = 10f;

    // 블록이 한 번이라도 떨어졌는지 여부
    private bool hasDropped = false;

    // 블록이 완전히 멈췄을 때 호출되는 이벤트
    public Action OnStopped;

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
        rb.gravityScale = data.gravityPower;
        rb.angularDamping = 2f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // 초기 상태값 설정
        targetAngle = transform.eulerAngles.z;
        isStopped = false;
        hasDropped = false;
        stopTimer = 0f;
        dropTimer = 0f;

        Debug.Log($"[Block Init] {data.blockName} - 크기({data.width}x{data.height}) / 밀도: {data.density} / 질량: {rb.mass}");
    }

    // 블록 낙하 시작
    public void ActivatePhysics()
    {
        if (hasDropped) return;
        rb.bodyType = RigidbodyType2D.Dynamic;
        hasDropped = true;
    }

    // 좌우 이동 (물리 적용 전만 가능)
    public void Move(Vector2 direction)
    {
        if (!hasDropped)
            transform.position += (Vector3)direction;
    }

    // 회전 조작 (물리 적용 전만 가능)
    public void Rotate(float deltaAngle)
    {
        if (!hasDropped)
            targetAngle += deltaAngle;
    }

    void Update()
    {
        // 물리 적용 전에는 회전 처리 및 자동 낙하 체크
        if (!hasDropped)
        {
            float current = rb.rotation;
            float newAngle = Mathf.MoveTowardsAngle(current, targetAngle, rotateSpeed * Time.deltaTime);
            rb.MoveRotation(newAngle);

            dropTimer += Time.deltaTime;
            if (dropTimer >= dropDelay)
            {
                Debug.Log("자동 낙하 발동");
                ActivatePhysics();
            }
        }

        // 물리 적용된 후 정지 감지
        if (rb.bodyType != RigidbodyType2D.Dynamic) return;

        // 선속도, 각속도가 일정 이하이면 정지로 판단
        bool velocityLow = rb.linearVelocity.magnitude <= 0.1f;
        bool angularLow = Mathf.Abs(rb.angularVelocity) <= 1f;

        if (velocityLow && angularLow)
        {
            stopTimer += Time.deltaTime;

            if (!isStopped && stopTimer >= 0.5f)
            {
                isStopped = true;
                OnStopped?.Invoke(); // 정지 이벤트 호출
            }
        }
        else
        {
            stopTimer = 0f; // 속도가 감지되면 타이머 초기화
        }
    }
}