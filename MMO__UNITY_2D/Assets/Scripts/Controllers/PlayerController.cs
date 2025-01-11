
using System;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    public Grid grid;
    public float moveSpeed = 5f;
    public Vector3Int cellPos = Vector3Int.zero;
    
    private MoveDir _moveDir = MoveDir.None;
    private bool _isMoving = false;
    
    void Start()
    {
        Vector3 pos = grid.CellToWorld(cellPos) + new Vector3(-0.5f, -0.5f);
        transform.position = pos;
    }

    void Update()
    {
        UpdateDirection();
        UpdatePosition();
        UpdateIsMoving();
    }

    void UpdatePosition()
    {
        if (!_isMoving) return;
        
        var destination = grid.CellToWorld(cellPos) + new Vector3(-0.5f, -0.5f);
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
        if (_isMoving != false) return;
     
        switch (_moveDir)
        {
            case MoveDir.Up:
                cellPos += Vector3Int.up;
                _isMoving = true;
                break;
            case MoveDir.Down:
                cellPos += Vector3Int.down;
                _isMoving = true;
                break;
            case MoveDir.Left:
                cellPos += Vector3Int.left;
                _isMoving = true;
                break;
            case MoveDir.Right:
                cellPos += Vector3Int.right;
                _isMoving = true;
                break;
            case MoveDir.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void UpdateDirection()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _moveDir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _moveDir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _moveDir = MoveDir.Left; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _moveDir = MoveDir.Right;
        }
        else
        {
            _moveDir = MoveDir.None;
        }
    }
}
