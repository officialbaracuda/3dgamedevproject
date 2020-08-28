using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health;

    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public int GetHealth()
    {
        return health;
    }

    public void GetBigger() {
        Vector3 scale = transform.localScale;
        scale *= 1.02f;
        transform.localScale = scale;
    }
}
