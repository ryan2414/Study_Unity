using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Define.CameraMode _mode = Define.CameraMode.QuaterView;
    [SerializeField] Vector3 _delta = new Vector3(0f, 6f, -5f);
    [SerializeField] GameObject _player = null;

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuaterView)
        {
            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform);
        }
        
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
