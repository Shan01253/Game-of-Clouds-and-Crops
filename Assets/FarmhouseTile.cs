using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmhouseTile : Tile
{
    private int playerID;

    public override bool Water(float waterAmount)
    {
        throw new System.NotImplementedException();
    }

    public override bool SetOnFire()
    {
        return false;
    }
}
