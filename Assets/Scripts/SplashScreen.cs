using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] Text loadingText;
    [SerializeField] float loadingTextChangeDelay = 1f;

    void Start()
    {
        StartCoroutine(LoadingText());
    }

    private IEnumerator LoadingText()
    {
        while (true)
        {
            loadingText.text = "Loading";
            yield return new WaitForSeconds(loadingTextChangeDelay);
            loadingText.text = "Loading.";
            yield return new WaitForSeconds(loadingTextChangeDelay);
            loadingText.text = "Loading..";
            yield return new WaitForSeconds(loadingTextChangeDelay);
            loadingText.text = "Loading...";
            yield return new WaitForSeconds(loadingTextChangeDelay);
        }
    }
}
