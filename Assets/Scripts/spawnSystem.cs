using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class spawnSystem : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject player;
    [SerializeField] List<Transform> enemySpawners;
    DataEnemies dataEnemies;
    // Start is called before the first frame update
    void Start()
    {
        dataEnemies = gameObject.GetComponent<DataEnemies>();
        Spawn();
    }
    void Spawn()
    {
        
            foreach (Transform spawner in enemySpawners)
            {
                Instantiate(gameObject, spawner.position, spawner.rotation);
                dataEnemies.setPlayer(player);
                dataEnemies.setSpawn(spawner);
            }
    }
}
