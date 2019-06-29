using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;


    //COMMENTED OUT PREVIOUS CODE AND IMPLEMENTED AN EASIER METHOD WITH LOADING BAR IMPLEMENTATION
    //public int SceneIndexToLoad = 1;
    //public GameObject[] DoNotDestroyList = new GameObject[0];
    // Start is called before the first frame update
 
        /*
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
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
    #endregion */

    public void SceneLoader(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            Debug.Log(progress);
            yield return null;
        }
    }
}
