using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    PlayerState _state = PlayerState.Idle;
    private CursorType _cursorType = CursorType.None;

    private PlayerStat _stat;
    Vector3 _destPos;

    private Texture2D _attackIcon;
    private Texture2D _handIcon;

    void Start()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }


    private void Update()
    {
        UpdateMouseCursor();

        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    private void UpdateMouseCursor()
    {
        if (Input.GetMouseButton(0)) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }

    private void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

    private void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            // TODO
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    _state = PlayerState.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }

        // 애니메이션
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태에 대한 정보를 넘겨준다.
        anim.SetFloat("speed", _stat.MoveSpeed);
    }

    private void UpdateDie()
    {
        // 아무것도 못함
    }

    private int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    private GameObject _lockTarget;
    
    private void OnMouseEvent(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die) return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        _state = PlayerState.Moving;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.PointerUp:
                _lockTarget = null;
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget != null)
                        _destPos = _lockTarget.transform.position;
                    else
                    if (raycastHit)
                        _destPos = hit.point;
                }
                break;
        }
     
    }
}