using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    [SerializeField] GameObject projectiles;
    [SerializeField] DataEnemies enemies;
    [SerializeField] float attackRange;
    [SerializeField] float maxCouldown;
    [SerializeField] float currentCouldown;
    // Start is called before the first frame update
    void Start()
    {
       currentCouldown = maxCouldown;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentCouldown < maxCouldown)
        {
            enemies.setDestination(this.transform.position);
            currentCouldown+= 1 *Time.deltaTime;
        }
        else
        {
            attack();
        }
        

    }

    void attack()
    {
        if(enemies.getDistance() <= attackRange)
        {
            
            GameObject newProjectiles = Instantiate(projectiles, this.transform.position, this.transform.rotation);
            currentCouldown = 0;
        }
    }
}
