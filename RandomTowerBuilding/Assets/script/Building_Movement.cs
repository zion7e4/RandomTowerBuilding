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
    public bool isGrounded = false;
    [SerializeField]
    private bool Spawnnextblock = false;

    public Building_Change buildingchange;

    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        buildingchange = FindObjectOfType<Building_Change>();
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
            rigid2D.gravityScale = 1.0f;
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
        isStable = true;
        rigid2D.linearVelocity = Vector2.zero;
        rigid2D.angularVelocity = 0f;

        if (!Spawnnextblock)
        {
            SpawnNextBlock(); // 새로운 블록 생성
        }
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
            CheckStability();
        }
    }
}

