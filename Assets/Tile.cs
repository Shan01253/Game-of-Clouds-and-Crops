using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile
{
    // Returns true if this tile has been fully watered, and surrounding tiles should be converted
    public abstract bool Water(float waterAmount);

    // Returns true if this tile can be set on fire
    public abstract bool SetOnFire();
}
