using UnityEngine;

public class RainSystem : MonoBehaviour
{
    public float newFriction = 0.5f; // ������ ���� ���

    void Start()
    {
        // Collider2D ������Ʈ�� �����ɴϴ�.
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            // Collider2D�� ����� Physics Material 2D�� �����ɴϴ�.
            PhysicsMaterial2D material = collider.sharedMaterial;

            if (material != null)
            {
                // ���� ����� �����մϴ�.
                material.friction = newFriction;
            }
        }
    }
}
