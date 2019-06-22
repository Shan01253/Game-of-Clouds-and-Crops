using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariedDropSpawner : MonoBehaviour
{

    public Vector2 center;
    public Vector2 size;

    public float time; // time to spawn new drops
    public float timer; //counts up in seconds
    public float timeUntilDropsDestroyed;

    public int numOfDropsToSpawn;

    public float SideLengthOfField = 12;
    public float maxSpeed = 0f;


    // Start is called before the first frame update
    void Start()
    {
        numOfDropsToSpawn = 20;
        time = 0f;
        timer = 0f;
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
            SpawnDrops();
        }
    }
    //Spawn drops at random location.
    public void SpawnDrops()
    {     
        for (int i = 0; i <= numOfDropsToSpawn; i++)
        {
            Vector2 withStartingPoint = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Quaternion withNoRotation = Quaternion.identity;
            GameObject obj = ObjectPooler.SpawnFromPool("raindrop", withStartingPoint, withNoRotation);
        }
    }

    void Timer() 
    {
        if (time > 10f)
        {
            SpawnDrops();
            time = 0f;
        }
    }

    //Draw Box on Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, .25f);
        Gizmos.DrawCube(center, size);
    }
}
