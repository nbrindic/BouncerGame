using System;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public event Action OnFinishReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            OnFinishReached();
    }
}
