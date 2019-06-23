using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariedDropSpawner : MonoBehaviour
{

    public Vector2 center1;
    public Vector2 center2;
    public Vector2 center3;
    public Vector2 center4;
    public Vector2 size1;
    public Vector2 size2;
    public Vector2 size3;
    public Vector2 size4;

    public float time; // time to spawn new drops
    public float timer; //counts up in seconds
    public float timeUntilDropsDestroyed;

    public int numOfDropsToSpawn;

    public float SideLengthOfField = 12;
    public float maxSpeed = 0f;

    public float delay;

    // Start is called before the first frame update
    void Start()
    {   
        time = 0f;
        timer = 0f;
        delay = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timer += Time.deltaTime;
        Timer();

        //When time is up, Destroy all drops and spawn new ones.
        if (timer > timeUntilDropsDestroyed)
        {
            timer = 0f;
            for(int i = 0; i < numOfDropsToSpawn; i++)
            {
                StartCoroutine(SpawnDrops());
            }
        }
    }
    //Spawn drops at random location.
    IEnumerator SpawnDrops()
    {
        Vector2 topLeft = center1 + new Vector2(Random.Range(-size1.x / 2, size1.x / 2), Random.Range(-size1.y / 2, size1.y / 2));
        Vector2 topRight = center2 + new Vector2(Random.Range(-size2.x / 2, size2.x / 2), Random.Range(-size2.y / 2, size2.y / 2));
        Vector2 bottomLeft = center3 + new Vector2(Random.Range(-size3.x / 2, size3.x / 2), Random.Range(-size3.y / 2, size3.y / 2));
        Vector2 bottomRight = center4 + new Vector2(Random.Range(-size4.x / 2, size4.x / 2), Random.Range(-size4.y / 2, size4.y / 2));
        Quaternion withNoRotation = Quaternion.identity;
        GameObject obj = ObjectPooler.SpawnFromPool("raindrop", topLeft, withNoRotation);
        yield return new WaitForSeconds(delay);
        GameObject obj2 = ObjectPooler.SpawnFromPool("raindrop", topRight, withNoRotation);
        yield return new WaitForSeconds(delay);
        GameObject obj3 = ObjectPooler.SpawnFromPool("raindrop", bottomLeft, withNoRotation);
        yield return new WaitForSeconds(delay);
        GameObject obj4 = ObjectPooler.SpawnFromPool("raindrop", bottomRight, withNoRotation);
        yield return new WaitForSeconds(delay);

    }

    void Timer() 
    {
        if (time > 3f)
        {
            StartCoroutine(SpawnDrops());
            time = 0f;
        }
    }

    //Draw Box on Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, .25f);
        Gizmos.DrawCube(center1, size1);
        Gizmos.DrawCube(center2, size2);
        Gizmos.DrawCube(center3, size3);
        Gizmos.DrawCube(center4, size4);

    }
}
