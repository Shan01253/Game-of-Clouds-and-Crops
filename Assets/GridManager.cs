using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int numberRows;
    public int numberCols;
    public bool showGrid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (showGrid)
        {
            for (int i = 0; i < numberRows; i++)
            {
                Gizmos.DrawLine(transform.position + new Vector3(i, 0, 0), transform.position + new Vector3(i, 13, 0));
            }
            for (int i = 0; i < numberRows; i++)
            {
                Gizmos.DrawLine(transform.position + new Vector3(0, i, 0), transform.position + new Vector3(13, i, 0));
            }
        }
    }
}
