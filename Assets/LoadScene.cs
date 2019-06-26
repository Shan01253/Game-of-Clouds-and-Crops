using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int SceneIndexToLoad = 1;
    public GameObject[] DoNotDestroyList = new GameObject[0];
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            foreach (GameObject g in DoNotDestroyList)
            {
                DontDestroyOnLoad(g);
            }
            SceneManager.LoadScene(SceneIndexToLoad);
        }

    }

    public void WithIndex(int i)
    {
        SceneManager.LoadScene(i);
    }
}
