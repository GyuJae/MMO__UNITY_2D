
using System;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Vector3Int cellPos = Vector3Int.zero;
    
    public MoveDir Dir
    {
        get {return _moveDir;}
        set
        {
            if (value == _moveDir) return;
            switch (value)
            {
                case MoveDir.Up:
                    _animator.Play("WALK_BACK");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Down:
                    _animator.Play("WALK_FRONT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Left:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Right:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.None:
                    switch (_moveDir)
                    {
                        case MoveDir.Up:
                            _animator.Play("IDLE_BACK");
                            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                            break;
                        case MoveDir.Down:
                            _animator.Play("IDLE_FRONT");
                            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                            break;
                        case MoveDir.Left:
                            _animator.Play("IDLE_RIGHT");
                            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                            break;
                        case MoveDir.Right:
                        case MoveDir.None:
                            _animator.Play("IDLE_RIGHT");
                            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
            _moveDir = value;
        }
    }
    
    private MoveDir _moveDir = MoveDir.Down;
    private bool _isMoving = false;
    private Animator _animator;
    
    void Start()
    {
        Vector3 pos = Managers.Map.CurrentGrid.CellToWorld(cellPos) + new Vector3(-0.5f, -0.5f);
        transform.position = pos;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateDirection();
        UpdatePosition();
        UpdateIsMoving();
    }

    void LateUpdate()
    {
        if (Camera.main == null) return;
        var z = Camera.main.transform.position.z;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    void UpdatePosition()
    {
        if (!_isMoving) return;
        
        var destination = Managers.Map.CurrentGrid.CellToWorld(cellPos) + new Vector3(-0.5f, -0.5f);
        var moveDir = destination - transform.position;
        
        var distance = moveDir.magnitude;
        if (distance < moveSpeed * Time.deltaTime)
        {
            transform.position = destination;
            _isMoving = false;    
        }
        else
        {
            transform.position += moveDir.normalized * (moveSpeed * Time.deltaTime);
            _isMoving = true;
        }
    }

    void UpdateIsMoving()
    {
        if (_isMoving != false || _moveDir == MoveDir.None) return;
        var destPos = cellPos;
        
        switch (_moveDir)
        {
            case MoveDir.Up:
                destPos += Vector3Int.up;
                break;
            case MoveDir.Down:
                destPos += Vector3Int.down;
                break;
            case MoveDir.Left:
                destPos += Vector3Int.left;
                break;
            case MoveDir.Right:
                destPos += Vector3Int.right;
                break;
            case MoveDir.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (!Managers.Map.CanGo(destPos)) return;
        cellPos = destPos;
        _isMoving = true;
    }

    void UpdateDirection()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDir.Left; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDir.Right;
        }
        else
        {
            Dir = MoveDir.None;
        }
    }
}
