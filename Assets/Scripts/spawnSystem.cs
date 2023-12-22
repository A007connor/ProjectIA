using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class spawnSystem : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject player;
    [SerializeField] List<Transform> enemySpawners;
    [SerializeField] Boss_Minotaur minotaur;
    DataEnemies dataEnemies;

    TestSpawer testSpawner;

    // Start is called before the first frame update
    void Start()
    {
        minotaur =  gameObject.GetComponent<Boss_Minotaur>();
        dataEnemies = gameObject.GetComponent<DataEnemies>();
        Spawnennemies();
        if (testSpawner.killedBoss[1] == false)
        {
            SpawnBoss();
        }

    }
    void Spawnennemies()
    {
        
            foreach (Transform spawner in enemySpawners)
            {
                if(dataEnemies != null)
                {
                    Instantiate(gameObject, spawner.position, spawner.rotation);
                    dataEnemies.setPlayer(player);
                    dataEnemies.setSpawn(spawner);
                }
                
                
            }
    }   
    void SpawnBoss()
    {  
        foreach (Transform spawner in enemySpawners)
        {
            if (minotaur != null)
            {
                Instantiate(gameObject, spawner.position, spawner.rotation);
                minotaur.setPlayerTransform(player.transform);
            }
        }
    }
}
