using System;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawer : MonoBehaviour
{
    public SaveSystem saveSystem;
    public List<bool> killedBoss = new List<bool>();

    public bool boss1Killed;
    public bool boss2Killed;
    public bool boss3Killed;


    private void Awake()
    {
        LoadGame();
    }

    public void Clear()
    {

        killedBoss.Clear();
    }

    public void Initialize()
    {
        killedBoss.Add(false);
        killedBoss.Add(false);
        killedBoss.Add(false);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.bossData = killedBoss;

/*        for(int i = 0; i < killedBoss.Count; i++)
        {
            data.Add(killedBoss[i]);
        }*/

        var dataToSave = JsonUtility.ToJson(data);
        saveSystem.SaveData(dataToSave);
    }

    public void LoadGame()
    {

        Clear();
        string dataToLoad = saveSystem.LoadData();
        //dataToLoad = saveSystem.LoadData();
        if (String.IsNullOrEmpty(dataToLoad)==false)
        {
            SaveData data = JsonUtility.FromJson<SaveData>(dataToLoad);
            killedBoss = data.bossData;
/*            for (int i = 0;i < data.bossData.Count; i++)
            {
                var bossStatue = data.bossData[i];
                killedBoss.Add(bossStatue);
            }*/
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