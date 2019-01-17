using UnityEngine;
using UnityEngine.EventSystems;

public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down
}

[ExecuteInEditMode]
public class PlayerBlockController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float BackToIdleSpeed;
    public MoveDirection MoveDir;
    public float MoveStepSize;
    public float DragToForceFactor;
    public Transform ArrowObject;
    [HideInInspector] public float CurrentForce;

    private Transform _transform;
    private Vector3 _initialPosition;
    private bool _isDragging;
    private float _maxDisplacement;

    private void Awake()
    {
        _transform = transform;
        _initialPosition = _transform.position;
        SetupArrowAndMaxDisplacement();
    }

    private void SetupArrowAndMaxDisplacement()
    {
        _maxDisplacement = 1f;
        var arrowAngle = Vector3.zero;
        
        switch (MoveDir)
        {
            case MoveDirection.Up:
                arrowAngle = new Vector3(0f, -90f, 0f);
                break;            
            case MoveDirection.Down:
                arrowAngle = new Vector3(0f, 90f, 0f);
                break;
            case MoveDirection.Left:
                arrowAngle = new Vector3(0f, 180f, 0f);
                break;
            case MoveDirection.Right:
                break;
        }
        ArrowObject.rotation = Quaternion.Euler(arrowAngle);
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
        var newAxisPosition = 0f;
        var maxAxisPosition = 0f;

        switch (MoveDir)
        {
            case MoveDirection.Left:
                if (delta.x < 0)
                {
                    newAxisPosition = _transform.position.x - delta.x * MoveStepSize;
                    maxAxisPosition = _initialPosition.x - _maxDisplacement;
                    if (newAxisPosition < maxAxisPosition)
                        newAxisPosition = maxAxisPosition;

                    _transform.position = new Vector3(newAxisPosition, _transform.position.y, _transform.position.z);
                    CurrentForce = -delta.x * DragToForceFactor;
                }
                break;
            case MoveDirection.Right:
                if (delta.x > 0)
                {
                    newAxisPosition = _transform.position.x + delta.x * MoveStepSize;
                    maxAxisPosition = _initialPosition.x + _maxDisplacement;
                    if (newAxisPosition > maxAxisPosition)
                        newAxisPosition = maxAxisPosition;

                    _transform.position = new Vector3(newAxisPosition, _transform.position.y, _transform.position.z);
                    CurrentForce = delta.x * DragToForceFactor;
                }
                break;
            case MoveDirection.Up:
                if (delta.y > 0)
                {
                    newAxisPosition = _transform.position.z + delta.y * MoveStepSize;
                    maxAxisPosition = _initialPosition.z + _maxDisplacement;
                    if (newAxisPosition > maxAxisPosition)
                        newAxisPosition = maxAxisPosition;

                    _transform.position = new Vector3(_transform.position.x, _transform.position.y, newAxisPosition);
                    CurrentForce = delta.y * DragToForceFactor;
                }
                break;
            case MoveDirection.Down:
                if (delta.y < 0)
                {
                    newAxisPosition = _transform.position.z + delta.y * MoveStepSize;
                    maxAxisPosition = _initialPosition.z - _maxDisplacement;
                    if (newAxisPosition < maxAxisPosition)
                        newAxisPosition = maxAxisPosition;

                    _transform.position = new Vector3(_transform.position.x, _transform.position.y, newAxisPosition);
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
