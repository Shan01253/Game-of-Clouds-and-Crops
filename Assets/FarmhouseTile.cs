using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmhouseTile : Tile
{
    // How much water the farmhouse tile currently has
    private float currentWater = 0f;
    // How much water the farmhouse tile needs to be fully watered
    private static readonly float waterThreshold = 30f;

    // Farmhouses can be owned by players, so take in a player ID on construction
    public FarmhouseTile(int playerID) : base(playerID) { }

    // Farmhouses can be watered to give players a way back into the game if they lose all crop tiles
    public override bool Water(float waterAmount)
    {
        currentWater += waterAmount;
        bool fullyWatered = currentWater >= waterThreshold;

        if (fullyWatered)
        {
            currentWater = 0f;
        }

        return fullyWatered;
    }

    // Farmhouses cannot be set on fire
    public override bool SetOnFire()
    {
        return false;
    }

    // Farmhouses can never be converted to crops, regardless of who is converting
    public override bool CanConvertToCrop(int playerID)
    {
        return false;
    }
}
