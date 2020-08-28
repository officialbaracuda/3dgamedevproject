using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private int level;

    private float spawnRate;

    private Spawner spawner;
    [SerializeField]
    private PlayerController player;

    #region SINGLETON
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    void Start()
    {
        ResetGame();
        spawner = Spawner.Instance;

        InvokeRepeating("SpawnZombie", 0f, spawnRate);
        InvokeRepeating("SpawnFood", 0f, spawnRate/2);
    }

    public void Kill(GameObject gameObject)
    {
        Debug.Log(gameObject.name + "is killed");
        if (gameObject.tag == Constants.PLAYER)
        {
            GameOver();
        }
    }

    public void Collect(GameObject gameObject) {
        score += 10;
        player.GetBigger();
        spawner.Remove(Spawner.poolTag.FOOD, gameObject);
    }

    void SpawnZombie()
    {
        spawner.SpawnFromQueue(Spawner.poolTag.ENEMY);
    }

    void SpawnFood()
    {
        spawner.SpawnFromQueue(Spawner.poolTag.FOOD);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }

    private void ResetGame()
    {
        score = 0;
        level = 0;
        spawnRate = 5.0f;
    }
}
