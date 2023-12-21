using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ZAxisSorter : MonoBehaviour
{
    [SerializeField] private Transform _mainTransform;        // R�f�rence au Transform du GameObject parent principal

    private void Update()
    {
        // On r�cup�re la position du transform
        Vector3 pos = transform.position;
        // On d�finit son Z comme �tant �gal au Y du mainTransform
        pos.z = _mainTransform.position.y;
        // On red�finit la position avec le nouveau Z
        transform.position = pos;
    }
}