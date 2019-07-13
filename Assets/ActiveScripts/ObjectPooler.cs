using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // unity attributes
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    // grab objectPooler from cubeSpawner

    private static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // initialize poolDictionary
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // spawn specified tag
    public static GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if(Instance == null)
        {
            Debug.LogException(new System.Exception("There is no ObjectPooler in hierarchy!"));
        }
        if (!Instance.poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exist");
            return null;
        }

        GameObject objectToSpawn = Instance.poolDictionary[tag].Dequeue();

        //for some reason particles misbehave if they are active and art setactive redundantly
        if (objectToSpawn.activeInHierarchy) { objectToSpawn.SetActive(false); }
        objectToSpawn.SetActive(true);


        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // add tag back to queue for reuse
        Instance.poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}