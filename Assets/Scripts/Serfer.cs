using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serfer : MonoBehaviour
{
    GameObject currentTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Attacker>())
        {
            GetComponent<Animator>().SetBool("isAttacking", true);
            currentTarget = collision.gameObject;
            GetComponent<Defender>().Attack(currentTarget);


        }
    }

    public void BubbleUp()
    {
        GetComponent<Health>().SetHealth(99999);
        var BubbleParticles = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().emission;
        BubbleParticles.rateOverTime = 455.3f;
        StartCoroutine(StartCounter(BubbleParticles));
    }

    private IEnumerator StartCounter(ParticleSystem.EmissionModule particles)
    {
        float countDown = 8f;
        for (int i = 0; i < 10000; i++)
        {
            while (countDown >= 0)
            {
                countDown -= Time.smoothDeltaTime;
                yield return null;
            }
        }

        GetComponent<Health>().SetHealth(1);
        particles.rateOverTime = 0f;
    }
}
