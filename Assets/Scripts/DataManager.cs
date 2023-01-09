using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private CacheData _cache;
    public CacheData cache
    {
        set { _cache = value; }
        get
        {
            if (_cache != null) return _cache;
            else return _cache = LoadCash();
        }

    }

    private void Start()
    {

    }
    public void SaveCash()
    {
        
        string json = JsonUtility.ToJson(cache);
        Debug.Log($"save \n{json}");
        File.WriteAllText(Application.persistentDataPath + "/lastPlayer.json", json);
    }

    public void InputName(string newPlayerName)
    {
        if (newPlayerName != "")
        {
            cache.playerName = newPlayerName;
        }
    }



    public CacheData LoadCash()
    {
        string path = Application.persistentDataPath + "/lastPlayer.json";
        if (File.Exists(path))
        {
            
            string json = File.ReadAllText(path);
            Debug.Log($"Load\n{json}");
            cache = JsonUtility.FromJson<CacheData>(json);
            return cache;
        }

        return cache = new CacheData();
    }

    [Serializable] public class CacheData
    {
        public string playerName = "Nobody...";
        public float musicVolume = 0.5f;
        public float effectVolume = 0.5f;
    }

    
}
