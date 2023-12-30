using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private int _number;

    public int Number => _number;
}
