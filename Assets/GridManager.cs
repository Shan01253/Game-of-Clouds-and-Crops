using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] CloudsToHighlightTilesUnder;
    public bool HighlightTilesUnderCloudsAndNear = false;


    // Parameters that determine number of rows/columns
    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;

    // How far to offset the farmhouses from the edge of the grid (0 to have them on the edge)
    [SerializeField]
    private int farmhouseOffsetX;
    [SerializeField]
    private int farmhouseOffsetY;

    // Option to show the grid lines in the editor
    [SerializeField]
    private bool showGrid;

    // Prefab for initializing the grid
    [SerializeField]
    private GameObject plainTile;

    private TileContainer[][] grid;

    private void Start()
    {
        Vector3 bottomLeft = GetBottomLeftOfGrid() + new Vector3(0.5f, 0.5f);

        grid = new TileContainer[rows][];

        // Fill grid with plain tiles
        for (int ii = 0; ii < rows; ii++)
        {
            grid[ii] = new TileContainer[columns];
            for (int jj = 0; jj < columns; jj++)
            {
                GameObject tileObj = Instantiate(plainTile, bottomLeft + new Vector3(jj, ii), Quaternion.identity, transform);
                TileContainer tile = tileObj.GetComponent<TileContainer>();
                grid[ii][jj] = tile;

                // Add tile above and tile to the left to this tile's neighbors, and add this tile to their neighbors
                if (ii > 0)
                    tile.AddNeighbor(grid[ii - 1][jj]);
                if (jj > 0)
                    tile.AddNeighbor(grid[ii][jj - 1]);
            }
        }

        // Place initial farmhouses and crop tiles based on serialized offset values
        grid[farmhouseOffsetY][farmhouseOffsetX].ConvertToFarmhouse(0);
        grid[farmhouseOffsetY + 1][farmhouseOffsetX].ConvertToCrop(0);
        grid[farmhouseOffsetY][farmhouseOffsetX + 1].ConvertToCrop(0);

        grid[farmhouseOffsetY][columns - farmhouseOffsetX - 1].ConvertToFarmhouse(1);
        grid[farmhouseOffsetY + 1][columns - farmhouseOffsetX - 1].ConvertToCrop(1);
        grid[farmhouseOffsetY][columns - farmhouseOffsetX - 2].ConvertToCrop(1);

        grid[rows - farmhouseOffsetY - 1][farmhouseOffsetX].ConvertToFarmhouse(2);
        grid[rows - farmhouseOffsetY - 2][farmhouseOffsetX].ConvertToCrop(2);
        grid[rows - farmhouseOffsetY - 1][farmhouseOffsetX + 1].ConvertToCrop(2);

        grid[rows - farmhouseOffsetY - 1][columns - farmhouseOffsetX - 1].ConvertToFarmhouse(3);
        grid[rows - farmhouseOffsetY - 2][columns - farmhouseOffsetX - 1].ConvertToCrop(3);
        grid[rows - farmhouseOffsetY - 1][columns - farmhouseOffsetX - 2].ConvertToCrop(3);
    }

    private void OnDrawGizmos()
    {
        // Show grid lines in editor
        if (showGrid)
        {
            Gizmos.color = Color.magenta;
            Vector3 bottomLeft = GetBottomLeftOfGrid();

            // draw rows
            for (int i = 0; i <= rows; i++)
            {
                Gizmos.DrawLine(bottomLeft + new Vector3(0, i, 0), bottomLeft + new Vector3(columns, i, 0));
            }
            // draw columns
            for (int i = 0; i <= columns; i++)
            {
                Gizmos.DrawLine(bottomLeft + new Vector3(i, 0, 0), bottomLeft + new Vector3(i, rows, 0));
            }
        }

        if (HighlightTilesUnderCloudsAndNear)
        {
            foreach(GameObject g in CloudsToHighlightTilesUnder)
            {
                Color c = (Color.white + Color.black + Color.blue)/3;
                c.a = 0.5f;
                Gizmos.color = c;
                
                Vector3Int tilePos = Vector3Int.RoundToInt(g.transform.position);
                Gizmos.DrawCube(tilePos, Vector3.one);

                
                Vector3 temp = Vector3.right;

                // draws cubes around the tilepos
                c = (Color.white + Color.blue) / 2;
                c.a = 0.5f;
                Gizmos.color = c;
                for (int i = 0; i < 8; i++)
                {

                    Gizmos.DrawCube(tilePos + temp, Vector3.one);

                    temp = new Vector3((temp.x - temp.y), (temp.x + temp.y), 0);

                    if (temp.x != 0)
                        temp.x = temp.x / Mathf.Abs(temp.x);
                    if (temp.y != 0)
                        temp.y = temp.y / Mathf.Abs(temp.y);
                }

            }
        }
    }

    private Vector3 GetBottomLeftOfGrid()
    {
        return transform.position - new Vector3((float)columns / 2, (float)rows / 2);
    }
}
