using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.name == "Gravestone(Clone)")
        {
            GetComponent<Animator>().SetTrigger("jumpTrigger");
        }
        else if (otherObject.GetComponent<Defender>() && otherObject.name != "Gravestone(Clone)")
        {
            GetComponent<Attacker>().Attack(otherObject);
        }
    }
}
