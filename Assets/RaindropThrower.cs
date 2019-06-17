using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaindropThrower : MonoBehaviour
{
    public float delay = 1;
    public float referenceCircleRadius = 6 * Mathf.Sqrt(2);
    public float SideLengthOfField = 12;
    public float maxSpeed = 10;
    float radius;
    float π = Mathf.PI;

    public GameObject ignoreCollisionsWith;
    // Start is called before the first frame update
    void Start()
    {
        radius = referenceCircleRadius;
        StartCoroutine(RandomThrow_WithDelay());
    }

    IEnumerator RandomThrow_WithDelay()
    {
        yield return new WaitForSeconds(delay);

        float θ = Random.value * (2 * π);
        Vector2 withStartingPoint = radius * new Vector2(Mathf.Cos(θ), Mathf.Sin(θ));
        Quaternion withNoRotation = Quaternion.identity;
        Vector2 trajectory = RandomTrajectory.PointToSquare(withStartingPoint, SideLengthOfField);

        GameObject obj = ObjectPooler.SpawnFromPool("raindrop", withStartingPoint, withNoRotation);
        obj.GetComponent<Rigidbody2D>().velocity = trajectory * maxSpeed * Random.Range(0.3f, 1);
        Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), ignoreCollisionsWith.GetComponent<EdgeCollider2D>());

        StartCoroutine(RandomThrow_WithDelay());
    }
}
