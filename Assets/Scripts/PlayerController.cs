using UnityEngine;

class Tank
{
    // 온갖 정보
    public float speed = 10.0f;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 10.0f;
    float _rotateSpeed = 0.2f;


    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;    
        Managers.Input.KeyAction += OnKeyboard;    
    }

    private void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), _rotateSpeed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), _rotateSpeed);
            transform.position += Vector3.back * Time.deltaTime * _speed;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), _rotateSpeed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _rotateSpeed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
