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

    [SerializeField]
    private float time; // time to spawn new drops
    [SerializeField]
    private float timer; //counts up in seconds
    public float timeUntilDropsSpawn = 2;
    public float timeUntilDropsDestroyed = 5;
    public int maxNumOfDropsToSpawn = 2;

    //public float SideLengthOfField = 12;
    //public float maxSpeed = 0f;

    private Queue<GameObject> dropsToDeactivate = new Queue<GameObject>();

    public float maxDelay = 5;
    public float minDelay = 3;
     float delay;

    // Start is called before the first frame update
    void Start()
    {   
        time = 0f;
        //timer = 0f;
        delay = Random.Range(minDelay, maxDelay);
        StartCoroutine(PeriodicallyDeactivateDrops());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //timer += Time.deltaTime;

        //regularly deactivate drops (make sure objectpooler has sufficient drops to not mess with this)
        //When time is up, Spawn drops.
        if (time > timeUntilDropsSpawn)
        {
            time = 0f;
            delay = Random.Range(minDelay, maxDelay);
            for (int i = 0; i < Random.Range(1, maxNumOfDropsToSpawn); i++)
            {
                delay = Random.Range(minDelay, maxDelay);
                StartCoroutine(SpawnDrops(delay));
            }
        }
    }

    IEnumerator PeriodicallyDeactivateDrops()
    {
        yield return new WaitForSeconds(timeUntilDropsDestroyed);
        if (dropsToDeactivate.Count > 0)
        {
            GameObject g = dropsToDeactivate.Dequeue();
            g?.SetActive(false);
        }
        StartCoroutine(PeriodicallyDeactivateDrops());
    }


    //Spawn drops at random location.
    IEnumerator SpawnDrops(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector2 topLeft = center1 + new Vector2(Random.Range(-size1.x / 2, size1.x / 2), Random.Range(-size1.y / 2, size1.y / 2));
        Vector2 topRight = center2 + new Vector2(Random.Range(-size2.x / 2, size2.x / 2), Random.Range(-size2.y / 2, size2.y / 2));
        Vector2 bottomLeft = center3 + new Vector2(Random.Range(-size3.x / 2, size3.x / 2), Random.Range(-size3.y / 2, size3.y / 2));
        Vector2 bottomRight = center4 + new Vector2(Random.Range(-size4.x / 2, size4.x / 2), Random.Range(-size4.y / 2, size4.y / 2));
        Quaternion withNoRotation = Quaternion.identity;
        GameObject obj;
        switch (Random.Range(1, 5))
        {
            case 1:
                 obj = ObjectPooler.SpawnFromPool("raindrop", topLeft, withNoRotation);
                break;
            case 2:
                 obj = ObjectPooler.SpawnFromPool("raindrop", topRight, withNoRotation);
                break;
            case 3:
                 obj = ObjectPooler.SpawnFromPool("raindrop", bottomLeft, withNoRotation);
                break;
            default:
                 obj = ObjectPooler.SpawnFromPool("raindrop", bottomRight, withNoRotation);
                break;
        }
        dropsToDeactivate.Enqueue(obj);
                

        }

    //void Timer()
    //{
    //    if (time > timeUntilDropsSpawn)
    //    {
    //        //StartCoroutine(SpawnDrops());
    //        time = 0f;
    //    }
    //}

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
