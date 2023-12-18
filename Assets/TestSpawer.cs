using System;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawer : MonoBehaviour
{
    public SaveSystem saveSystem;

    public bool boss1Killed;
    public bool boss2Killed;
    public bool boss3Killed;


    public List<bool> killedBoss = new List<bool>();

    public void Clear()
    {

        killedBoss.Clear();
    }

    public void Initialize()
    {
        killedBoss.Add(false);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        for(int i = 0; i < killedBoss.Count; i++)
        {
            data.Add(killedBoss[i]);
        }
        var dataToSave = JsonUtility.ToJson(data);
        saveSystem.SaveData(dataToSave);
    }

    public void LoadGame()
    {

        Clear();
        string dataToLoad = "";
        dataToLoad = saveSystem.LoadData();
        if (String.IsNullOrEmpty(dataToLoad)==false)
        {
            SaveData data = JsonUtility.FromJson<SaveData>(dataToLoad);
            for (int i = 0;i < data.bossData.Count; i++)
            {
                var bossStatue = data.bossData[i];
                killedBoss.Add(bossStatue);
            }
        }
    }

    [Serializable]
    public class SaveData
    {
        public List<bool> bossData;

        public SaveData()
        {
            bossData = new List<bool>();
        }

        public void Add(bool bossStatue)
        {
            bossData.Add(bossStatue);
        }
    }

    [Serializable]
    public class BossData
    {
        public float x, y, z;
        public bool bossStatue;

        public BossData (bool bossStatue)
        {
            this.bossStatue = bossStatue;
        }
  
    }
}