using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : Tile
{
    // How much water the crop tile currently has
    private float currentWater = 0f;
    // How much water the crop tile needs to be fully watered
    
    // this feels arbitrary so I have changed it to be percents so that in watering code I can fill be a certain percent -Yahiya
    private static readonly float waterThreshold = 1;

    // Crop tiles can be owned by players, so take in a player ID on construction
    public CropTile(int playerID) : base(playerID) { }

    // Crop tiles can be watered (obviously)
    // If a crop tile reaches its water threshold, reset the current water and 
    // return true so the TileContainer can convert adjacent tiles
    public override bool Water(float waterAmount)
    {
        // put these to not catch you by surprise -Yahiya
        if (waterAmount < 0)
        {
            throw new System.ArgumentException("Cannot water negative amount!");
        }
        if (waterAmount > 1)
        {
            Debug.Log(waterAmount);
            throw new System.ArgumentException("Cannot water more than 200 percent at a time");
        }

        currentWater += waterAmount;
        bool fullyWatered = currentWater >= waterThreshold;

        if (fullyWatered)
        {
            //Debug.Log("received full amount of water");
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
