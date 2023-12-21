using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private LayerMask _attackBoxLayer;
    private BoxCollider2D _collider;
    private Transform _baseTransform;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _baseTransform = transform.parent.parent.parent;
    }
    public bool IsHit()
    {
        Collider2D attackBox = Physics2D.OverlapArea(transform.position - _collider.bounds.extents, transform.position + _collider.bounds.extents, _attackBoxLayer);
        return attackBox != null;
    }
    public Vector2 GetHit()
    {
        Collider2D attackBox = Physics2D.OverlapArea(transform.position - _collider.bounds.extents, transform.position + _collider.bounds.extents, _attackBoxLayer);
        if (attackBox)
        {
            return _baseTransform.position.x < attackBox.transform.position.x ? Vector2.left : Vector2.right;
        }
        return Vector2.zero;
    }
}