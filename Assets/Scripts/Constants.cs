
using System.Collections;
using System.Collections.Generic;

public class Constants 
{
    // Animator Controller Boolean Triggers
    public static string IS_WALKING = "isWalking";
    public static string IS_JUMPING = "isJumping";

    // Tags
    public static string PLAYER = "Player";
    public static string KILLER = "Killer";
    public static string ENEMY = "Enemy";
    public static string HP = "Health Pickup";

    public static IList<string> KILLABLES = new List<string>() { ENEMY };

    // Game End Messages
    public static string GAME_OVER = "GAME OVER";
    public static string GAME_COMPLETED = "CONGRATULATIONS";

    // Scene Names
    public static string GAME = "Game";
    public static string MAIN_MENU = "Main Menu";

    // PlayerPref Keys
    public static string HIGHEST_SCORE = "highestScore";
    public static string TIME = "time";
    public static string EATEN_FOOD = "eatenFood";
    public static string SFX_VOLUME = "sfxVolume";
    public static string MUSIC_VOLUME = "musicVolume";
    public static string SFX_ON_OFF = "sfxOnOff";
    public static string MUSIC_ON_OFF = "musicOnOff";
}
