using UnityEngine;
using UnityEngine.EventSystems;

public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down
}

public class PlayerBlockController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float BackToIdleSpeed;
    public MoveDirection MoveDir;
    public float MoveStepSize;
    public float DragToForceFactor;

    public float CurrentForce;

    private Transform _transform;
    private Vector3 _initialPosition;
    private bool _isDragging;

    private void Awake()
    {
        _transform = transform;
        _initialPosition = _transform.position;    
    }

    private void Update ()
    {
        if (!_isDragging)
            _transform.position = Vector3.Lerp(_transform.position, _initialPosition, BackToIdleSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            other.GetComponent<BallController>().ApplyForce(CurrentForce, MoveDir);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;

        switch (MoveDir)
        {
            case MoveDirection.Left:
                if (delta.x < 0)
                {
                    _transform.position = new Vector3(_transform.position.x - delta.x * MoveStepSize, _transform.position.y, _transform.position.z);
                    CurrentForce = -delta.x * DragToForceFactor;
                }
                break;
            case MoveDirection.Right:
                if (delta.x > 0)
                {
                    _transform.position = new Vector3(_transform.position.x + delta.x * MoveStepSize, _transform.position.y, _transform.position.z);
                    CurrentForce = delta.x * DragToForceFactor;
                }
                break;
            case MoveDirection.Up:
                if (delta.y > 0)
                {
                    _transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z + delta.y * MoveStepSize);
                    CurrentForce = delta.y * DragToForceFactor;
                }
                break;
            case MoveDirection.Down:
                if (delta.y < 0)
                {
                    _transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z + delta.y * MoveStepSize);
                    CurrentForce = -delta.y * DragToForceFactor;
                }
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CurrentForce = 0f;
        _isDragging = false;
    }
}
