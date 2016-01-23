using UnityEngine;
using System.Collections;

public class OpenPosition {
    public Vector2 position;
    public Side openSide;
    public Block adjacentBlock;

    public OpenPosition(Vector2 position, Side openSide, Block adjacentBlock)
    {
        this.position = position;
        this.openSide = openSide;
        this.adjacentBlock = adjacentBlock;
    }
}
