using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public event Action OnHitDeadWall;
    public float _currentForceMultiplier = 1f;
    public ParticleSystem ExplosionVFX;

    private Rigidbody _rigidBody;
    private float _currentForce;
    private MoveDirection _currentDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBlock"))
        {
            var explosion = Instantiate(ExplosionVFX, this.transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
            OnHitDeadWall();
        }
    }

    public void ApplyForce(float force, MoveDirection direction)
    {
        if (force > 0)
        {
            Debug.Log("Should receive force: " + force + " in direction: " + direction);
            _currentForce = force / 2.5f;
            _currentDirection = direction;

            // Reset values
            _rigidBody.velocity = Vector3.zero;
            this.transform.rotation = Quaternion.identity;

            // Move in desired direction
            switch (_currentDirection)
            {
                case MoveDirection.Down:
                    _rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    _rigidBody.AddForce(-this.transform.forward * _currentForce * _currentForceMultiplier, ForceMode.Impulse);
                    break;
                case MoveDirection.Up:
                    _rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    _rigidBody.AddForce(this.transform.forward * _currentForce * _currentForceMultiplier, ForceMode.Impulse);
                    break;
                case MoveDirection.Left:
                    _rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
                    _rigidBody.AddForce(-this.transform.right * _currentForce * _currentForceMultiplier, ForceMode.Impulse);
                    break;
                case MoveDirection.Right:
                    _rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
                    _rigidBody.AddForce(this.transform.right * _currentForce * _currentForceMultiplier, ForceMode.Impulse);
                    break;
            }
        }
    }
}
