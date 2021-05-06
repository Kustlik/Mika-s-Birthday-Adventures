using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Level 1");
    }
}
