using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private int eatenFoodCount;

    private float spawnRate;
    public bool isGameOver;
    public bool isGamePaused;

    private Spawner spawner;
    [SerializeField]
    private PlayerController player;
    private StorageController storage;
    private AudioController audioController;

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
        audioController = AudioController.Instance;
        storage = StorageController.Instance;
        ResetGame();
        spawner = Spawner.Instance;

        InvokeRepeating("SpawnZombie", 0f, spawnRate);
        InvokeRepeating("SpawnFood", 0f, spawnRate/2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver) {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                PauseGame();
            }
            else {
                UnPauseGame();
            }
        }
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
        eatenFoodCount++;
        player.GetBigger();
        audioController.PlaySFX(AudioController.Clip.PICKUP);
        spawner.Remove(Spawner.poolTag.FOOD, gameObject);
    }

    void SpawnZombie()
    {
        if (!isGameOver && !isGamePaused) {
            spawner.SpawnFromQueue(Spawner.poolTag.ENEMY);
        }
    }

    void SpawnFood()
    {
        if (!isGameOver && !isGamePaused)
        {
            spawner.SpawnFromQueue(Spawner.poolTag.FOOD);
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        if (storage.GetInt(Constants.HIGHEST_SCORE) < score) {
            storage.StoreInt(Constants.HIGHEST_SCORE, score);
            storage.StoreFloat(Constants.TIME, GetGameTime());
            storage.StoreInt(Constants.EATEN_FOOD, eatenFoodCount);
        }
        audioController.PlaySFX(AudioController.Clip.GAME_OVER);
        Debug.Log("Game Over");
    }

    private void ResetGame()
    {
        UnPauseGame();
        isGamePaused = false;
        isGameOver = false;
        score = 0;
        eatenFoodCount = 0;
        spawnRate = 5.0f;
    }

    private void PauseGame() {
        isGamePaused = true;
    }
    public void UnPauseGame()
    {
        isGamePaused = false;
    }

    public bool IsGameRunning() {
        return !isGamePaused && !IsGameOver();
    }

    #region UI CONTROLLER
    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public bool IsGameOver()
    {
        return isGameOver || IsGameCompleted();
    }

    public int GetEnemyCount()
    {
        return spawner.GetActiveEnemyCount();
    }

    public int GetEatenFoodCount()
    {
        return eatenFoodCount;
    }

    public float GetGameTime()
    {
        return Time.timeSinceLevelLoad;
    }

    public int GetScore()
    {
        return score;
    }

    public bool IsGameCompleted()
    {
        if (eatenFoodCount >= 30) {
            audioController.PlaySFX(AudioController.Clip.GAME_OVER);
            return true;
        }
        return false;
    }

    #endregion
}
