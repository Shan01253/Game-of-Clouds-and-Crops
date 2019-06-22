using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    TileContainer()
    {
        tile = new PlainTile();
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Used for grid initialization and for spreading crops during the game
    public void ConvertToCrop(int playerID)
    {
        spriteRenderer.color = PlayerColorManager.GetRGBAColor(playerID);
        spriteRenderer.sprite = Resources.Load<Sprite>("crudecrop");
        tile = new CropTile();
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
        tile = new FarmhouseTile();
    }
}
