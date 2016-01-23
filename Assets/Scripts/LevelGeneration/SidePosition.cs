using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SidePosition {
    [SerializeField]
    private Side side;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector2 arrOffset;

    public Side Side
    {
        get { return side; }
    }

    public Vector3 Offset
    {
        get { return offset; }
    }

    public Vector2 ArrOffset
    {
        get { return arrOffset; }
    }
}
