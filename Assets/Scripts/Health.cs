using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] float health = 100f;

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        if (GetComponent<Health>().GetHealth() <= 0)
        {
            GetComponent<Animator>().SetBool("isDying", true);
        }
        Destroy(gameObject, 5f);
        StartCoroutine(FadeSpriteAfterDeath());
    }

    private IEnumerator FadeSpriteAfterDeath()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(StartCounter());
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(int healthValue)
    {
        health = healthValue;
    }

    private IEnumerator StartCounter()
    {
        float countDown = 3f;
        for (int i = 0; i < 10000; i++)
        {
            while (countDown >= 0)
            {
                if (GetComponentInChildren<SpriteRenderer>())
                {
                    GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, (countDown / 3f));
                }
                else
                {
                    transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (countDown / 3f));
                }
                countDown -= Time.smoothDeltaTime;
                yield return null;
            }
        }
    }
}