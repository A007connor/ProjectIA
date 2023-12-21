using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Vector3[] positions;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[0], Time.deltaTime * speed);

        if(transform.position == positions[index])
        {
            if(index == positions.Length - 1)
            {
                index = 0;
            }
        }
        else
        {
            index++;
        }

    }
}
