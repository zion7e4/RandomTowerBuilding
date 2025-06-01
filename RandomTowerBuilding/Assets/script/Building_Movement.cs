using UnityEngine;
using UnityEngine.EventSystems;

public class Building_Movement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float rotateSpeed = 30f;
    private float stabilityTime = 0.5f;
    private float stableTimer;

    private Rigidbody2D rigid2D;

    private Vector3 moveDirection;
    private Vector3 rotationDirection;

    [SerializeField]
    private bool isControllable = true;
    [SerializeField]
    private bool isStable = false;
    [SerializeField]
    private bool isCounted = false;
    [SerializeField]
    public bool isGrounded = false;
    [SerializeField]
    private bool Spawnnextblock = false;
    [SerializeField]
    private bool hasStabilized = false;
    [SerializeField]
    private bool isFirstBlock = true;

    public Building_Change buildingchange;
    public BlockData data;

    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); //A, D / <-, -> 키 
        float z = Input.GetAxisRaw("Vertical");
        if(isControllable)
        {
            MoveTo(x);
            Rotate(z);
            DropBlock();
        }

        if (isGrounded)
        {
            CheckStability();
        }

        if (isStable && !isCounted)
        {
            buildingchange.IncrementBlockCount();
            isCounted = true;
        }
    }

    private void MoveTo(float x) //좌우 이동
    {
        moveDirection = new Vector3(x, 0, 0);

        transform.position += moveDirection * Speed * Time.deltaTime;
    }

    private void Rotate(float z)
    {
        rotationDirection = new Vector3(0, 0, z);
        transform.Rotate(rotationDirection * rotateSpeed * Time.deltaTime);

    }

    private void DropBlock()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isControllable = false;
            rigid2D.gravityScale = data.gravityPower;
        }
    }

    private void CheckStability()
    {
        if(rigid2D.linearVelocity.magnitude < 0.1 && Mathf.Abs(rigid2D.angularVelocity) < 1)
        {
            stableTimer += Time.deltaTime; // 안정화 시간 증가
            if (stableTimer >= stabilityTime)
            {
                StabilizeBlock(); // 블록 안정화
            }
        }
        else
        {
            stableTimer = 0f; // 안정화 시간 초기화
        }

    }

    private void StabilizeBlock()
    {
        if (hasStabilized) return;
        hasStabilized = true;

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.RegisterBlock(this.transform);

        if (!isCounted)
        {
            buildingchange.IncrementBlockCount();
            isCounted = true;
        }

        SpawnNextBlock();
    }

    private void SpawnNextBlock()
    {
        Spawnnextblock = true;
        buildingchange.SpawnNewBlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Block"))
        {
            isGrounded = true;
            if (isFirstBlock)
            {
                StabilizeBlock();
                isFirstBlock = false;
            }
        }
    }
}

