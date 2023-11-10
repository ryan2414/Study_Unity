using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

    void Start()
    {
        
    }

    float _yAngle = 0f;

    void Update()
    {
        _yAngle += Time.deltaTime * 100f;

        // 절대 회전값
        //transform.eulerAngles = new Vector3(0f, _yAngle, 0f);

        // +- delta
        // transform.Rotate(new Vector3(0f, Time.deltaTime * 100f, 0f)); 

        // transform.rotation = Quaternion.Euler(new Vector3(0f, _yAngle, 0f));

        float speed = 0.2f;

        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //.rotation = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), speed);
            transform.position += Vector3.back * Time.deltaTime * _speed;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), speed);
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), speed);
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
