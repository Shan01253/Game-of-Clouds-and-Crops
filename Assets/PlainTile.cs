using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainTile : Tile
{
    public override bool Water(float waterAmount)
    {
        return false;
    }

    public override bool SetOnFire()
    {
        return false;
    }
}
