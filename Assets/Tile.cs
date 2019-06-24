using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile
{
    // Readonly member used to guarantee that tile's player ID cannot be modified
    protected readonly int _playerID;
    // Public member used to access a tile's player ID; player ID will be -1 for tiles without ownership
    public int PlayerID { get { return _playerID; } }

    public Tile(int playerID)
    {
        _playerID = playerID;
    }

    // Adds the given amount of water to the tile
    // Returns true if this tile has been fully watered, and surrounding tiles should be converted
    public abstract bool Water(float waterAmount);

    // Returns true if this tile can be set on fire
    public abstract bool SetOnFire();

    // Returns true if this tile can be converted into a crop tile owned by the given player
    public abstract bool CanConvertToCrop(int playerID);
}
