using UnityEngine;
using UnityEngine.EventSystems;

public class Building_Movement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
    private Vector3 moveDirection;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");  
        MoveTo(x);
        
    }

    private void MoveTo(float x)
    {
        moveDirection = new Vector3(x, 0, 0);

        transform.position += moveDirection * Speed * Time.deltaTime;
    }
}

