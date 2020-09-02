using UnityEngine;

public class StorageController : MonoBehaviour
{
    public static StorageController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StoreString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public void StoreInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void StoreFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public int GetInt(string key)
    {
        if(PlayerPrefs.HasKey(key)){
            return PlayerPrefs.GetInt(key);
        }
        return 0;
    }

    public float GetFloat(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        return 0;
    }
}
