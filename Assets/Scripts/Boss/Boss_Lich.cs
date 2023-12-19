using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Lich : MonoBehaviour
{
    [SerializeField] StateManager currentState;
    [SerializeField] IdleState idleState;
    [SerializeField] AttackState attackState;
    [SerializeField] float maxCouldown;
    [SerializeField] float currentCouldown;
    [SerializeField] LayerMask invocationLayer;
    [SerializeField] float maxHp;
    [SerializeField] float currentHp;
    [SerializeField] List<Transform> enemySpawners;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject player;
    [SerializeField] DataEnemies enemies;
    void Start()
    {
        currentCouldown = maxCouldown;
        currentHp = maxHp;
        
    }

    private void Update()
    {
        ChangeState();
        if (currentState.GetState() is AttackState)
        {
            SpawnEnemies();
        }
        if(currentHp <= 0) Death();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & invocationLayer) != 0)
        {
            currentHp -= 1;
            
        }
    }

    void ChangeState()
    {
        if (currentCouldown != maxCouldown)
        {
            currentCouldown += 1 * Time.deltaTime;
            currentState.SwitchNextState(idleState);
        }
        if (currentCouldown >= maxCouldown)
        {
            currentState.SwitchNextState(attackState);
            currentCouldown = 0;
        }
        
    }

    void Death()
    {
        
        Destroy(gameObject);
    }
    void SpawnEnemies()
    {
        foreach(Transform spawner in enemySpawners)
        {
            Instantiate(enemyPrefab, spawner.position, spawner.rotation);
            enemies.setPlayer(player);
            enemies.setSpawn( spawner);
        }
    }
}
