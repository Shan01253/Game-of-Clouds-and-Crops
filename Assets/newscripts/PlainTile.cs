using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainTile : Tile
{
    // Plain tiles are not owned by players, and as such always have their player ID be -1
    public PlainTile() : base(-1) { }

    // Plain tiles are not affected by being watered
    public override bool Water(float waterAmount)
    {
        return false;
    }

    // Plain tiles can be set on fire
    public override bool SetOnFire()
    {
        throw new System.NotImplementedException();
    }

    // Plain tiles can always be converted to crops, regardless of who is converting
    public override bool CanConvertToCrop(int playerID)
    {
        return true;
    }
}
