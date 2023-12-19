using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEnemies : MonoBehaviour
{
    [SerializeField] bool inRange;
    [SerializeField] float range;
    public GameObject _player;
    [SerializeField] float distance;
    [SerializeField] float speed;
    Vector3 _destination;
    public Vector3 target;
    // Update is called once per frame
    void Update()
    {
        checkDistance();
        transform.position = Vector3.MoveTowards(transform.position, getDestination(), speed * Time.deltaTime);
    }

     void checkDistance()
    {
        
        distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance < range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
    public void setPlayer(GameObject player) { _player = player; }
    public Vector3 getTarget() { return target;}
    public void setDestination(Vector3 destination) { _destination = destination; }
    public Vector3 getDestination() { return _destination; }
    public bool getInRange() { return inRange; }
    public float getDistance() { return distance; }
    public float getSpeed() { return speed; }
    public Vector3 getPlayerPosition() { return _player.transform.position;}
    public Vector3 getCurrentPosition() { return this.transform.position; }
}
