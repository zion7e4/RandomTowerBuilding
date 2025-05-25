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
        rb = GetComponent<Rigidbody2D>();

        // 크기 설정 (비율 기반으로 스케일 조절)
        transform.localScale = new Vector3(data.width, data.height, 1f);

        // 질량 = 가로 × 세로 × 질량 보정 계수
        rb.mass = data.height * data.width * data.density;

        // 중력 영향도
        rb.gravityScale = data.gravityPower;

        // 생성 직후에는 조작만 가능하게 설정 (중력 X)
        rb.bodyType = RigidbodyType2D.Kinematic;

        // 회전 초기 상태 저장
        targetAngle = transform.eulerAngles.z;

        // 상태 초기화
        isStopped = false;
        hasDropped = false;
        stopTimer = 0f;
        dropTimer = 0f;
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