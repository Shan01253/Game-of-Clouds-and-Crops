using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrajectory
{
    // Given a point and length of field square centered at zero, chooses 
    //random point on field square and returns normalized the direction to that point
    public static Vector3 PointToSquare(Vector3  startPoint, float length)
    {
        // if square is centered at zero and has this length, then a random 
        //point will have each coord component have a magnitude less than length/2
        Vector3 endPoint = new Vector3(Random.Range(-length / 2, length / 2), Random.Range(-length / 2, length / 2));

        return (endPoint - startPoint).normalized;
    }
}
