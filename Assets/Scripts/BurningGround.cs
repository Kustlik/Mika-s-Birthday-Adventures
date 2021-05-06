using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGround : MonoBehaviour
{
    [SerializeField] float startingBurnTime = 5f;
    [SerializeField] float burningIntensity = 504.3f;
    [SerializeField] List<GameObject> attackers;
    float damage;

    private void Start()
    {
        attackers.Clear();
    }

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    public void StartBurning()
    {
        StartCoroutine(BurnTheGround());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Attacker>())
        {
            attackers.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Attacker>())
        {
            attackers.Remove(collision.gameObject);
        }
    }

    public void ClearAttackers()
    {
        attackers.Clear();
    }

    private IEnumerator BurnTheGround()
    {
        var particles = GetComponent<ParticleSystem>().emission;

        particles.rateOverTime = burningIntensity;
        StartCoroutine(DealDamageOverTime());

        float countDown = startingBurnTime;
        for (int i = 0; i < 10000; i++)
        {
            while (countDown >= 0)
            {
                countDown -= Time.smoothDeltaTime;
                yield return null;
            }
        }

        particles.rateOverTime = 0;
        Destroy(gameObject, 3f);
    }

    private IEnumerator DealDamageOverTime()
    {
        for (int index = 0; index < startingBurnTime; index++)
        {

            float countDown = 1;
            for (int i = 0; i < 10000; i++)
            {
                while (countDown >= 0)
                {
                    countDown -= Time.smoothDeltaTime;
                    yield return null;
                }
            }

            CleanAnArray();

            for (int enemy = 0; enemy < attackers.Count; enemy++)
            {
                attackers[enemy].GetComponent<Health>().DealDamage(damage);
                if (attackers[enemy].GetComponent<Health>().GetHealth() <= 0)
                {
                    var item = attackers[enemy];
                    attackers.Remove(item);
                    if (attackers.Count != 0)
                    {
                        enemy--;
                    }
                }
            }
        }
    }

    private void CleanAnArray()
    {
        for (int index = 0; index < attackers.Count; index++)
        {
            if (attackers[index] == null)
            {
                attackers.Remove(attackers[index]);
                if (attackers.Count != 0)
                {
                    index--;
                }
            }
        }
    }
}
