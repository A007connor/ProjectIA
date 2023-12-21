using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class spawnSystem : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject player;
    DataEnemies dataEnemies;
    // Start is called before the first frame update
    private void Awake()
    {
        dataEnemies = gameObject.GetComponent<DataEnemies>();
        dataEnemies.setSpawn(this.transform);
        dataEnemies.setPlayer(player);
    }
    void Start()
    {

        Spawn();
    }

    void Spawn()
    {
        Instantiate(gameObject, this.transform.position, this.transform.rotation);
    }
}
