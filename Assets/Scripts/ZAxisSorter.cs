using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ZAxisSorter : MonoBehaviour
{
    [SerializeField] private Transform _mainTransform;        // Référence au Transform du GameObject parent principal

    private void Update()
    {
        // On récupère la position du transform
        Vector3 pos = transform.position;
        // On définit son Z comme étant égal au Y du mainTransform
        pos.z = _mainTransform.position.y;
        // On redéfinit la position avec le nouveau Z
        transform.position = pos;
    }
}