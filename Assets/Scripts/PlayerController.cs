using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

    void Start()
    {
        
    }

    // GameObject (Player)
        // Transform
        // PlayerController (*)

    void Update()
    {
        // Local -> World
        // TransformDirection

        // World -> Local
        // InverseTransformDirection
        
        if (Input.GetKey(KeyCode.W))    
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
    }
}
