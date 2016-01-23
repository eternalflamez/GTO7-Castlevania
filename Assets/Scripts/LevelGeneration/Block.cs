using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    [SerializeField]
    private float weightAddition;
    [SerializeField]
    private float weight;
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private SidePosition[] sidePositions;
    
    private Side lastRequestedSide;

    public Vector2 Size
    {
        get { return size; }
    }

    public Side LastRequestedSide
    {
        get { return lastRequestedSide; }
    }

    public float Weight
    {
        get { return weight; }
    }

    public float WeightAddition
    {
        get { return weightAddition; }
    }

    public bool AllowSide(Side side)
    {
        for (int i = 0; i < sidePositions.Length; i++)
        {
            if (SideMath.GetCompatibleSides(side).Contains(sidePositions[i].Side))
            {
                lastRequestedSide = sidePositions[i].Side;
                return true;
            }
        }

        return false;
    }

    public List<Side> GetSides()
    {
        List<Side> sides = new List<Side>();

        foreach (SidePosition pos in sidePositions)
        {
            sides.Add(pos.Side);
        }

        return sides;
    }

    public Vector3 GetOffset(Side side)
    {
        foreach (SidePosition pos in sidePositions)
        {
            if (pos.Side == side)
            {
                return pos.Offset;
            }
        }

        // Should never happen.
        Debug.LogWarning("Couldn't find an offset!?");
        return new Vector3();
    }

    public Vector2 GetArrOffset(Side side)
    {
        foreach (SidePosition pos in sidePositions)
        {
            if(pos.Side == side)
            {
                return pos.ArrOffset;
            }
        }

        // Should never happen.
        Debug.LogWarning("Couldn't find an arrOffset!?");
        return new Vector2();
    }
}
