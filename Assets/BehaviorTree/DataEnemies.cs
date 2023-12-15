using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEnemies : MonoBehaviour
{
    [SerializeField] bool inRange;
    [SerializeField] float range;
    [SerializeField] Transform playerTransfom;
    [SerializeField] float distance;
    [SerializeField] float speed;
    // Update is called once per frame
    void Update()
    {
        checkDistance();
    }

    void checkDistance()
    {
        distance = Vector2.Distance(transform.position, playerTransfom.position);
        if (distance < range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

    public bool getInRange() { return inRange; }
    public float getDistance() { return distance; }
    public float getSpeed() { return speed; }
}
