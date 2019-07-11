using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileContainer : MonoBehaviour
{
    private Tile tile;
    private SpriteRenderer spriteRenderer;

    private IList<TileContainer> neighbors = new List<TileContainer>(4);

    // Adds the given tile to this tile's list of neighbors and vice versa, ignoring duplicates
    public void AddNeighbor(TileContainer tile)
    {
        if (neighbors.Contains(tile))
            return;
        neighbors.Add(tile);
        tile.AddNeighbor(this);
    }

    void Awake()
    {
        tile = new PlainTile();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Used for grid initialization and for spreading crops during the game
    public void ConvertToCrop(int playerID)
    {
        if (tile.CanConvertToCrop(playerID))
        {
            spriteRenderer.color = PlayerColorManager.GetRGBAColor(playerID);
            spriteRenderer.sprite = Resources.Load<Sprite>("crudecrop");
            tile = new CropTile(playerID);
        }
    }

    // Used only for grid initialization
    public void ConvertToFarmhouse(int playerID)
    {
        PlayerColor color = PlayerColorManager.GetPlayerColor(playerID);
        switch (color)
        {
            case PlayerColor.PURPLE:
                spriteRenderer.sprite = Resources.Load<Sprite>("purplecrudefarmhouse");
                break;
            case PlayerColor.RED:
                spriteRenderer.sprite = Resources.Load<Sprite>("redcrudefarmhouse");
                break;
            case PlayerColor.BLUE:
                spriteRenderer.sprite = Resources.Load<Sprite>("bluecrudefarmhouse");
                break;
            case PlayerColor.YELLOW:
                spriteRenderer.sprite = Resources.Load<Sprite>("yellowcrudefarmhouse");
                break;
            default:
                return;
        }
        tile = new FarmhouseTile(playerID);
    }

    public void Water(float waterAmount)
    {
        if (tile.Water(waterAmount))
        {
            int playerID = tile.PlayerID;
            foreach (TileContainer t in neighbors)
            {
                t.ConvertToCrop(playerID);
            }
        }
    }

    // Returns the player ID of the player that owns this tile
    // If the tile is not owned by a player, returns -1
    public int GetOwnerID()
    {
        return tile.PlayerID;
    }
}
