using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] int damage = 100;

    Animator animator;

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * projectileSpeed);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileImpact(collision);
    }

    private void ProjectileImpact(Collider2D damagedObject)
    {
        if (damagedObject.gameObject.GetComponent<Defender>())
        {
            damagedObject.gameObject.GetComponent<Health>().DealDamage(GetDamage());
            Destroy(gameObject, 4);
            animator.SetTrigger("explosion");
            projectileSpeed = 0;
        }
    }

    private int GetDamage()
    {
        return damage;
    }
}
