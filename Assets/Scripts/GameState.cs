using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToHide;
    bool isGameStarted = false;

    private void Start()
    {
        PauseGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void OnMouseDown()
    {
        if (!isGameStarted)
        {
            StartState();

            isGameStarted = true;
        }
    }

    void StartState()
    {
        var colliderComponent = GetComponent<BoxCollider2D>();

        Time.timeScale = 1;

        Destroy(transform.GetChild(0).gameObject);
        colliderComponent.enabled = !colliderComponent.enabled;
    }

    void BossStateStart()
    {
        HideUI();
    }

    void HideUI()
    {
        foreach(GameObject element in objectsToHide)
        {
            element.SetActive(false);
        }
    }
}
