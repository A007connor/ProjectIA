using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeProjectiles : MonoBehaviour
{
    float durationMax = 2f;
    float currentduration;
    float speed = 5;
    Vector3 _targetPosition;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (_targetPosition - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
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
