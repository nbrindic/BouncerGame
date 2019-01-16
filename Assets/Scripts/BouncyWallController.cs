using UnityEngine;

public class BouncyWallController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bouncy wall hit!");
    }
}
