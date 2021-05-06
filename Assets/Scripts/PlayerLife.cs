using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int life = 10;
    Text lifeText;

    private void Start()
    {
        lifeText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        lifeText.text = life.ToString();
        HaveEnoughLife();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Attacker>())
        {
            TakeDamage();
        }
        Destroy(collision.gameObject);
    }

    private void TakeDamage() 
    {
        if(life > 0)
        {
            life -= 1;
        }
        UpdateDisplay();
    }

    private void HaveEnoughLife()
    {
        if (life <= 0)
        {
            GameOver();
        }
        else
        {
            return;
        }
    }

    private void GameOver()
    {
        return;
    }

    public int GetLife()
    {
        return life;
    }
}
