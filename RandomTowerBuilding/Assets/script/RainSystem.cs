using UnityEngine;

public class RainSystem : MonoBehaviour
{
    public float newFriction = 0.5f; // 변경할 마찰 계수

    void Start()
    {
        // Collider2D 컴포넌트를 가져옵니다.
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            // Collider2D에 적용된 Physics Material 2D를 가져옵니다.
            PhysicsMaterial2D material = collider.sharedMaterial;

            if (material != null)
            {
                // 마찰 계수를 변경합니다.
                material.friction = newFriction;
            }
        }
    }
}
