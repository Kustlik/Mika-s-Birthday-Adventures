using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kustlik : MonoBehaviour
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
}


