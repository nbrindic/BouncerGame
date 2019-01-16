using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float OrbitSpeed = 10f;
    public float PositionSwitchSpeed = 10f;
    public GameObject GamePlayPosition;

    private Vector3 _lookAtPoint;
    private bool _shouldOrbit;
    private Transform _transform;
    private Vector3 _initialPosition;
    private BallController _ballInstance;

    private void Awake()
    {
        _transform = transform;
        _initialPosition = _transform.position;
    }

    private void Start()
    {
        _lookAtPoint = Target.transform.position;
        _transform.LookAt(_lookAtPoint);
        Orbit();
    }

    private void Update()
    {
        if(_shouldOrbit)
            _transform.RotateAround(_lookAtPoint, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * OrbitSpeed);
        else
        {
            _transform.position = Vector3.Lerp(
                _transform.position, 
                new Vector3(_ballInstance.transform.position.x, GamePlayPosition.transform.position.y, GamePlayPosition.transform.position.z), 
                PositionSwitchSpeed * Time.deltaTime);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, GamePlayPosition.transform.rotation, PositionSwitchSpeed * Time.deltaTime);
        }
    }

    public void Orbit()
    {
        _shouldOrbit = true;
    }

    public void SetViewForGamePlay(BallController ballInstance)
    {
        _ballInstance = ballInstance;
        _shouldOrbit = false;
    }
}
