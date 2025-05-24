using UnityEngine;
using UnityEngine.EventSystems;

public class Building_Movement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float rotateSpeed = 30f;
    private Rigidbody2D rigid2D;
    private Vector3 moveDirection;
    private Vector3 rotationDirection;
    private bool isControllable = true;
    public Building_Change buildingchange;

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
            rigid2D.gravityScale = 1.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Block"))
        {
            isControllable = false;
            rigid2D.linearVelocity = Vector2.zero; // 충돌 시 정지
        }
    }
}

