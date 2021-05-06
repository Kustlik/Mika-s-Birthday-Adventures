using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : MonoBehaviour
{
    [SerializeField] List<GameObject> attackers;
    float damage = 100f;
    bool attackAnim = false;

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

    public void DealCleaveDamage()
    {
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

    private void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        if (attackers.Count != 0 && !attackAnim)
        {
            transform.parent.gameObject.GetComponent<Mika>().SetAttackAnimation(true);
            attackAnim = true;
        }
        else if (attackers.Count == 0 && attackAnim)
        {
            transform.parent.gameObject.GetComponent<Mika>().SetAttackAnimation(false);
            attackAnim = false;
        }
    }
}
