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
    
    [SerializeField] PlayerState _state = PlayerState.Idle;
    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case PlayerState.Die:
                    break;
                case PlayerState.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    //anim.Play("WAIT");
                    break;
                case PlayerState.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    // anim.Play("RUN");
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0); // 루프
                    // anim.Play("ATTACK");
                    break;
            }
        }
    }
    
    private PlayerStat _stat;
    Vector3 _destPos;
    private int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
    private GameObject _lockTarget;

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        
        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    private void Update()
    {
        switch (State)
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
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
    }
    
    private void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);

        }
    }

    void OnHitEvent()
    {
        // 상대방에게 데미지를 넣어주는 게 더 좋다.
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            PlayerStat myStat = gameObject.GetComponent<PlayerStat>();
            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            Debug.Log(damage);
            targetStat.Hp -= damage;
        }
        
        if (_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }
        
        
    }
    
    private void UpdateIdle()
    {
       
    }

    private void UpdateMoving()
    {
        // 몬스터가 내 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = PlayerState.Skill;
                return;
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    State = PlayerState.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }

    private void UpdateDie()
    {
        // 아무것도 못함
    }

    private bool _stopSkill = false;
    private void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                    
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)    
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100f, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = PlayerState.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }
}