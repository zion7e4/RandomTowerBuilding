using UnityEngine;

// 블럭 하나의 동작을 담당하는 스크립트
// 조작, 낙하, 정지 판정, 자동 낙하 포함
public class Block : MonoBehaviour
{
    private Rigidbody2D rb; // 블럭의 물리 본체

    // 회전 처리
    private float targetAngle = 0f; // 목표 회전각
    private float rotateSpeed = 300f; // 회전 속도 (도/초)

    // 정지 판정
    private bool isStopped = false; // 정지 상태 여부
    private float stopTimer = 0f;   // 정지 유지 시간

    // 자동 낙하 처리
    private float dropTimer = 0f;   // 생성 후 경과 시간
    private float dropDelay = 10f;  // 10초 지나면 자동 낙하
    private bool hasDropped = false; // 낙하 처리 여부

    public System.Action<Block> OnStopped; // 정지 시 GameManager에게 알림

    // 외부에서 블럭 정보를 초기화할 때 호출
    public void Init(BlockData data)
    {
        // Rigidbody2D 가져오기
        rb = GetComponent<Rigidbody2D>();

        // 스프라이트 크기 적용
        transform.localScale = new Vector3(data.width, data.height, 1f);

        // 기존 콜라이더 제거 후 새로 생성
        PolygonCollider2D oldCol = GetComponent<PolygonCollider2D>();
        if (oldCol != null)
        {
            Destroy(oldCol); // 기존 콜라이더 제거
        }

        // 새 콜라이더 생성 (현재 스프라이트 + 스케일 기준으로 다시 만들어짐)
        PolygonCollider2D newCol = gameObject.AddComponent<PolygonCollider2D>();

        // 크기 적용 (BlockData 기준)
        transform.localScale = new Vector3(data.width, data.height, 1f);

        // 기존 콜라이더 제거 후 재생성
        if (GetComponent<PolygonCollider2D>() != null)
        {
            Destroy(GetComponent<PolygonCollider2D>());
        }

        // 마찰력 적용
        PhysicsMaterial2D mat = Resources.Load<PhysicsMaterial2D>("BlockFriction");
        if (mat != null)
        {
            newCol.sharedMaterial = mat;
        }

        // 질량과 중력 설정
        rb.mass = data.height * data.width * data.density;
        rb.gravityScale = data.gravityPower;
        rb.angularDamping = 2f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // 회전 및 상태 초기화
        targetAngle = transform.eulerAngles.z;
        isStopped = false;
        hasDropped = false;
        stopTimer = 0f;
        dropTimer = 0f;

        Debug.Log($"[Block Init] {data.blockName} - 크기({data.width}x{data.height}) / 밀도: {data.density} / 질량: {rb.mass}");
    }

    // 낙하 명령 → 물리 적용
    public void ActivatePhysics()
    {
        // 이미 낙하한 블럭이면 무시
        if (hasDropped) return;

        rb.bodyType = RigidbodyType2D.Dynamic;
        hasDropped = true;
    }

    // 좌우 이동 처리
    public void Move(Vector2 direction)
    {
        if (!hasDropped)
            transform.position += (Vector3)direction;
    }

    // 회전 명령 처리 (목표 각도만 바꿈)
    public void Rotate(float deltaAngle)
    {
        if (!hasDropped)
            targetAngle += deltaAngle;
    }

    // 매 프레임마다 실행
    void Update()
    {
        // 부드러운 회전 처리
        if (!hasDropped)
        {
            float current = rb.rotation;
            float newAngle = Mathf.MoveTowardsAngle(current, targetAngle, rotateSpeed * Time.deltaTime);
            rb.MoveRotation(newAngle);
        }

        // 자동 낙하 타이머: 10초가 지나면 낙하 강제 실행
        if (!hasDropped)
        {
            dropTimer += Time.deltaTime;
            if (dropTimer >= dropDelay)
            {
                Debug.Log("자동 낙하 발동");
                ActivatePhysics();
            }
        }

        // 낙하 후 정지 상태 판단
        if (rb.bodyType != RigidbodyType2D.Dynamic) return;

        bool velocityLow = rb.linearVelocity.magnitude <= 0.1f;
        bool angularLow = Mathf.Abs(rb.angularVelocity) <= 1f;

        // 일정 시간 동안 거의 움직이지 않으면 정지로 간주
        if (velocityLow && angularLow)
        {
            stopTimer += Time.deltaTime;

            if (!isStopped && stopTimer >= 0.5f)
            {
                isStopped = true;
                OnStopped?.Invoke(this); // GameManager에 알림
            }
        }
        else
        {
            stopTimer = 0f; // 다시 흔들리면 초기화
        }
    }
}