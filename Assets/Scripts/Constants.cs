
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

}
