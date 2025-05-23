using UnityEngine;
using UnityEngine.EventSystems;

public class Building_Movement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float rotateSpeed = 30f;
    private Vector3 moveDirection;
    private Vector3 rotationDirection;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); //A, D / <-, -> Å° 
        float z = Input.GetAxisRaw("Vertical");
        MoveTo(x);
        Rotate(z);
        
    }

    private void MoveTo(float x) //ÁÂ¿ì ÀÌµ¿
    {
        moveDirection = new Vector3(x, 0, 0);

        transform.position += moveDirection * Speed * Time.deltaTime;
    }

    private void Rotate(float z)
    {
        rotationDirection = new Vector3(0, 0, z);
        transform.Rotate(rotationDirection * rotateSpeed * Time.deltaTime); 
        
    }
}

