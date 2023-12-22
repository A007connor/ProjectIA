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

    public TestSpawer testSpawner;

    private void OnEnable()
    {
        Boss_Minotaur.onBossDeath += HandleBossDeath;
    }
    private void OnDisable()
    {
        Boss_Minotaur.onBossDeath -= HandleBossDeath;
    }

    private void HandleBossDeath()
    {
        if(testSpawner != null)
        {
            testSpawner.killedBoss[1] = true; // Or use the appropriate boss index
            testSpawner.SaveGame();
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        minotaur =  gameObject.GetComponent<Boss_Minotaur>();
        dataEnemies = gameObject.GetComponent<DataEnemies>();
        Spawnennemies();

        if (testSpawner != null && testSpawner.killedBoss.Count > 1 && testSpawner.killedBoss[1] == false)
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

                dataEnemies.setPlayer(player);
                dataEnemies.setSpawn(spawner);
                Instantiate(gameObject, spawner.position, spawner.rotation);
                
            }               
                
        }
     }   
    void SpawnBoss()
    {  
        foreach (Transform spawner in enemySpawners)
        {
            if (minotaur != null)
            {
                minotaur.setPlayerTransform(player.transform);
            }
            Instantiate(gameObject, spawner.position, spawner.rotation);
        }
    }

}
