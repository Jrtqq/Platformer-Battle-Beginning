using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public bool CheckGround()
    {
        Vector2 pointA = new(_collider.bounds.max.x, _collider.bounds.max.y);
        Vector2 pointB = new(_collider.bounds.min.x, _collider.bounds.min.y);

        Collider2D ground = Physics2D.OverlapArea(pointA, pointB, _layerMask);

        if (ground != null)
        {
            return true;
        }

        return false;
    }
}
