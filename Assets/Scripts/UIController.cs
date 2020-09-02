using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public Text foodEatenTxt;
    public Text timeTxt;
    public Text enemyCountTxt;
    public Text scoreTxt;
    public Text endMessageTxt;
    public Text highestScoreTxt;
    public Text timeMenuTxt;
    public Text foodEatenMenuTxt;

    public Button sfxOn;
    public Button sfxOff;
    public Button musicOn;
    public Button musicOff;

    public RectTransform gameOverMenu;
    public RectTransform pausedMenu;
    public RectTransform settings;

    public Slider sfxSlider;
    public Slider musicSlider;

    private bool isSettingsOpened;

    private GameManager gameManager;
    private StorageController storage;

    void Start()
    {
        isSettingsOpened = false;
        gameManager = GameManager.Instance;
        storage = StorageController.Instance;

        SetSettingsMenu();

        if (SceneController.Instance.GetActiveScene() == Constants.MAIN_MENU)
        {
            int score = storage.GetInt(Constants.HIGHEST_SCORE);
            highestScoreTxt.text = score.ToString();
            float time = storage.GetFloat(Constants.TIME);
            timeMenuTxt.text = time.ToString("F2");
            int foodEaten = storage.GetInt(Constants.EATEN_FOOD);
            foodEatenMenuTxt.text = foodEaten.ToString();
        }
    }

    void Update()
    {
        if (SceneController.Instance.GetActiveScene() == Constants.GAME)
        {
            SetHUDTexts();
            SetGameOverTexts();
            ManageGameOverMenu();
            ManagePauseMenu();
        }
    }

    private void SetSettingsMenu()
    {
        sfxSlider.value = storage.GetFloat(Constants.SFX_VOLUME);
        musicSlider.value = storage.GetFloat(Constants.MUSIC_VOLUME);

        SFXOn(storage.GetInt(Constants.SFX_ON_OFF) == 1);
        MusicOn(storage.GetInt(Constants.MUSIC_ON_OFF) == 1);
    }

    private void SetGameOverTexts()
    {
        scoreTxt.text = gameManager.GetScore().ToString();
        if (gameManager.IsGameCompleted())
        {
            endMessageTxt.text = Constants.GAME_COMPLETED;
        }
        else
        {
            endMessageTxt.text = Constants.GAME_OVER;
        }
    }

    private void SetHUDTexts()
    {
        if (gameManager.IsGameRunning())
        {
            enemyCountTxt.text = gameManager.GetEnemyCount().ToString();
            foodEatenTxt.text = gameManager.GetEatenFoodCount().ToString();
            timeTxt.text = gameManager.GetGameTime().ToString("F0");

        }
    }

    public void ManageGameOverMenu()
    {
        if (gameManager.IsGameOver())
        {
            gameOverMenu.DOAnchorPos(Vector2.zero, 0.5f);
        }
        else
        {
            gameOverMenu.DOAnchorPos(new Vector2(0, 1000), 0.5f);
        }
    }

    public void ManagePauseMenu()
    {
        if (gameManager.IsGamePaused() && !isSettingsOpened)
        {
            pausedMenu.DOAnchorPos(Vector2.zero, 0.5f);
        }
        else
        {
            pausedMenu.DOAnchorPos(new Vector2(1500, 0), 0.5f);
        }
    }

    public void ManageSettingsMenu(bool show)
    {
        isSettingsOpened = show;
        if (show)
        {
            settings.DOAnchorPos(Vector2.zero, 0.5f);
            pausedMenu.DOAnchorPos(new Vector2(1500, 0), 0.5f);
        }
        else
        {
            settings.DOAnchorPos(new Vector2(-1500, 0), 0.5f);
            pausedMenu.DOAnchorPos(Vector2.zero, 0.5f);
        }
    }

    public void SFXOn(bool isOn)
    {
        sfxOn.interactable = !isOn;
        sfxOff.interactable = isOn;
    }

    public void MusicOn(bool isOn)
    {
        musicOn.interactable = !isOn;
        musicOff.interactable = isOn;
    }

    public void SFXVolume()
    {
        AudioController.Instance.SetSFXVolume(sfxSlider.value);
    }

    public void MusicVolume()
    {
        AudioController.Instance.SetMusicVolume(musicSlider.value);
    }
}
