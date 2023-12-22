using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed;

    private BoxCollider boxCollider;

    float durationMax = 2f;
    float currentduration;
    Vector3 _targetPosition;

    private bool hit;

    public int damage = 20;

    void Update()
    {
        transform.Translate(_targetPosition * speed * Time.deltaTime);
        currentduration += 1 * Time.deltaTime;
        if (currentduration >= durationMax)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

    }

    public void setTargetPosition(Vector3 targetPosition) { _targetPosition = targetPosition; }

}
