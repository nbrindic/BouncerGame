using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public event Action OnHitDeadWall;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBlock"))
        {
            Debug.Log("Died!");
            OnHitDeadWall();
        }
    }

    public void ApplyForce(float force, MoveDirection direction)
    {
        Debug.Log("Should receive force: " + force + " in direction: " + direction);
    }
}
