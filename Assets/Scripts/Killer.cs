using UnityEngine;

public class Killer : MonoBehaviour
{
    // Kill game object collide with it.
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.Kill(other.gameObject);

    }
}
