using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] int loadDelayInSec = 4;
    int currentScene = 0;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Splash Screen")
        {
            LoadStartMenu();
        }
    }

    public void LoadStartMenu()
    {
        StartCoroutine(LoadDelay());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    private IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(loadDelayInSec);
        SceneManager.LoadScene("Level 1");
    }
}
