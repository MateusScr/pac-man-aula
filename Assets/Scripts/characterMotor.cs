using System;
using UnityEngine;

public enum Direction
{
    None,
    Up,
    Right,
    Down,
    Left
}
public class characterMotor : MonoBehaviour
{
    public float velocMovi;
    private Vector2 _direcMoveAtual;
    private Vector2 _direcMoveDesej;
    private Rigidbody2D _rigidbody;

    private Vector2 _boxSize;

    public event Action<Direction> OnDirectionChanged;
    public Direction DireMoveAtual
    {
        get
        {
            //up
            if (_direcMoveAtual.y > 0)
            {
                return Direction.Up;
            }
            //left
            if (_direcMoveAtual.x < 0)
            {
                return Direction.Left;
            }
            //Down
            if (_direcMoveAtual.y < 0)
            {
                return Direction.Down;
            }
            //Right
            if (_direcMoveAtual.x > 0)
            {
                return Direction.Right;
            }

            return Direction.None;
        }
    }

    public void SetMoveDirection(Direction newMoveDirection)
    {
        switch (newMoveDirection)
        {
            default:
            case Direction.None:
                break;

            case Direction.Up:
                _direcMoveDesej = Vector2.up;
                break;

            case Direction.Left:
                _direcMoveDesej = Vector2.left;
                break;

            case Direction.Down:
                _direcMoveDesej = Vector2.down;
                break;

            case Direction.Right:
                _direcMoveDesej = Vector2.right;
                break;
        }



    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxSize = GetComponent<BoxCollider2D>().size;
    }


    private void FixedUpdate()
    {
        float DistaMov = velocMovi * Time.fixedDeltaTime;
        var nextPosMov = _rigidbody.position + _direcMoveAtual * DistaMov;
        //up
        if (_direcMoveAtual.y > 0)
        {
            var maxY = Mathf.CeilToInt(_rigidbody.position.y);
            if (nextPosMov.y >= maxY)
            {
                transform.position = new Vector2(_rigidbody.position.x, maxY);
                DistaMov = nextPosMov.y - maxY;
            }
        }
        //left
        if (_direcMoveAtual.x < 0)
        {
            var minX = Mathf.FloorToInt(_rigidbody.position.x);
            if (nextPosMov.x <= minX)
            {
                transform.position = new Vector2(minX, _rigidbody.position.y);
                DistaMov = minX - nextPosMov.x;
            }
        }
        //Down
        if (_direcMoveAtual.y < 0)
        {
            var minY = Mathf.FloorToInt(_rigidbody.position.y);
            if (nextPosMov.y <= minY)
            {
                transform.position = new Vector2(_rigidbody.position.x, minY);
                DistaMov = minY - nextPosMov.y;
            }
        }
        //Right
        if (_direcMoveAtual.x > 0)
        {
            var maxX = Mathf.CeilToInt(_rigidbody.position.x);
            if (nextPosMov.x >= maxX)
            {
                transform.position = new Vector2(maxX, _rigidbody.position.y);
                DistaMov = nextPosMov.x - maxX;
            }
        }
        Physics2D.SyncTransforms();

        if (_rigidbody.position.y == Mathf.CeilToInt(_rigidbody.position.y) && _rigidbody.position.x == Mathf.CeilToInt(_rigidbody.position.x))
        {
            if (_direcMoveAtual != _direcMoveDesej)
            {
                if (!Physics2D.BoxCast(_rigidbody.position, _boxSize, 0, _direcMoveDesej, 1f, 1 << LayerMask.NameToLayer("Level")))
                {
                    _direcMoveAtual = _direcMoveDesej;
                    OnDirectionChanged?.Invoke(DireMoveAtual);
                }
            }

            if (!Physics2D.BoxCast(_rigidbody.position, _boxSize, 0, _direcMoveAtual, 1f, 1 << LayerMask.NameToLayer("Level")))
            {
                _direcMoveAtual = Vector2.zero;
                OnDirectionChanged?.Invoke(DireMoveAtual);
            }
        }

        _rigidbody.MovePosition(_rigidbody.position + _direcMoveAtual * DistaMov);
    }
}
