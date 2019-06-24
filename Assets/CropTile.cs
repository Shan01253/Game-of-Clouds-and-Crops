using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : Tile
{
    // How much water the crop tile currently has
    private float currentWater = 0f;
    // How much water the crop tile needs to be fully watered
    private static readonly float waterThreshold = 30f;

    // Crop tiles can be owned by players, so take in a player ID on construction
    public CropTile(int playerID) : base(playerID) { }

    // Crop tiles can be watered (obviously)
    // If a crop tile reaches its water threshold, reset the current water and 
    // return true so the TileContainer can convert adjacent tiles
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

    // Crop tiles can be set on fire
    public override bool SetOnFire()
    {
        throw new System.NotImplementedException();
    }

    // Crop tiles can be converted to crop tiles who are owned by a different player
    // A crop tile will not be converted if the player who currently owns the tile is trying to convert it
    public override bool CanConvertToCrop(int playerID)
    {
        return playerID != PlayerID;
    }
}
