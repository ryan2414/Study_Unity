using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1) 나 혹은 상대한테 RigidBody가 있어야 한다. (IsKinematic : Off)
    // 2) 나한테 Collider가 있어야 한다. (IsTrigger : Off)
    // 3) 상대한테 Collider가 있어야 한다. (IsTrigger : Off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name}");
    }

    // 1) 둘 다 Collider가 있어야 한다.
    // 2) 둘 중 하나는 IsTrigger : On
    // 3) 둘 중 하나는 RigidBody가 있어야 한다.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger ! {other.name}");

    }

    private void Update()
    {
        // Local <-> World <-> Viewport <-> Screen (화면)

        //Debug.Log(Input.mousePosition); // Screen

        Debug.Log( Camera.main.ScreenToViewportPoint(Input.mousePosition)); // Viewport
    }

}
