using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    GameObject currentTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Attacker>())
        {
            currentTarget = collision.gameObject;
            GetComponent<Defender>().Attack(currentTarget);
        }
    }
}
