using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEnemies : MonoBehaviour
{
    [SerializeField] bool inRange;
    [SerializeField] float range;
    public GameObject _player;
    public Transform _spawn;
    [SerializeField] float distance;
    [SerializeField] float speed;

    [SerializeField] float speedChase;

    Vector3 _destination;
    public Vector3 target;

    private void Awake()
    {
        checkDistance();
    }
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

            transform.position = Vector3.MoveTowards(transform.position, getDestination(), speedChase * Time.deltaTime);


            inRange = true;
        }
        else
        {

            transform.position = Vector3.MoveTowards(transform.position, getDestination(), speed * Time.deltaTime);


            inRange = false;
        }
    }
    public void setSpawn(Transform spawn) { _spawn = spawn; }
    public void setPlayer(GameObject player) { _player = player; }
    public Vector3 getTarget() { return target;}
    public void setDestination(Vector3 destination) { _destination = destination; }
    public Vector3 getDestination() { return _destination; }
    public bool getInRange() { return inRange; }
    public float getDistance() { return distance; }
    public void SetSpeedChase(float SpeedChase) { speedChase = SpeedChase; }
    public float getSpeedChase() { return speedChase; }
    public float getSpeed() { return speed; }
    public Vector3 getPlayerPosition() { return _player.transform.position;}
    public Vector3 getCurrentPosition() { return this.transform.position; }
}
