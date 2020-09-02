using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }
    private void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartGame() {
        LoadScene(Constants.GAME);
    }

    public void MainMenu()
    {
        LoadScene(Constants.MAIN_MENU);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public string GetActiveScene() {
        return SceneManager.GetActiveScene().name;
    }
}
