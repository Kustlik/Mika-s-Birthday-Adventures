using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarczo : MonoBehaviour
{
    [SerializeField] GameObject colliderFront;
    [SerializeField] GameObject colliderBack;
    [SerializeField] float damageAmount = 20;

    public void DealFrontDamage()
    {
        colliderFront.GetComponent<TarczoHammer>().DealDamage(damageAmount);
    }

    public void DealBackDamage()
    {
        colliderBack.GetComponent<TarczoHammer>().DealDamage(damageAmount);
    }
}
