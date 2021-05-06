using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mika : MonoBehaviour
{
    [SerializeField] GameObject cleaveCollider;
    [SerializeField] float damage;
    [SerializeField] GameObject[] dialog;

    Animator animator;
    Cleave cleaveDamage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cleaveDamage = transform.GetChild(1).GetComponent<Cleave>();
    }

    public void SetAttackAnimation(bool value)
    {
        animator.SetBool("isAttacking", value);
    }

    public void DealCleaveDamage()
    {
        cleaveDamage.DealCleaveDamage();
    }

    public GameObject[] GetDialog()
    {
        return dialog;
    }
}
