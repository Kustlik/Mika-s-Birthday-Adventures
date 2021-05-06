using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarczoHammer : MonoBehaviour
{
    GameObject currentTarget;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() && collision.GetComponent<Defender>())
        {
            currentTarget = collision.gameObject;
        }
    }

    public void DealDamage(float damageValue)
    {
        if (currentTarget == null || !currentTarget.GetComponent<Defender>()) { return; }

        if (currentTarget.GetComponent<Health>().GetHealth() > 0 && currentTarget.GetComponent<Defender>())
        {
            currentTarget.GetComponent<Health>().DealDamage(damageValue);
        }
    }
}
