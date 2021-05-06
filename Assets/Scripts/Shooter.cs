using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform gunPosition;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] float projectileRotationSpeed = 1f;
    [SerializeField] float distance = 2;

    AttackerSpawner myLaneSpawner;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsDefenderInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

    }

    private bool IsDefenderInLane()
    {
        Defender[] defenders = FindObjectsOfType<Defender>();
        var validDefenders = new List<Defender>();
        bool shootValid = false;

        if(defenders.Length > 0)
        {
            ValidateTargets(defenders, validDefenders);
            shootValid = ValidateDistance(defenders, validDefenders, shootValid);
        }

        validDefenders.Clear();

        if (shootValid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ValidateTargets(Defender[] defenders, List<Defender> validDefenders)
    {
        if (defenders.Length > 0)
        {
            for (int index = 0; index < defenders.Length; index++)
            {
                if ((defenders[index].transform.position.y == transform.position.y) && (!defenders[index].GetComponent<Animator>().GetBool("isDying")))
                {
                    validDefenders.Add(defenders[index]);
                }
            }
        }
    }

    private bool ValidateDistance(Defender[] defenders, List<Defender> validDefenders, bool shootValid)
    {
        shootValid = false;

        if(validDefenders.Count > 0)
        {
            for (int index = 0; index < validDefenders.Count; index++)
            {
                if ((validDefenders[index].transform.position.x <= (transform.position.x)) && (validDefenders[index].transform.position.x >= (transform.position.x - distance)))
                {
                    shootValid = true;
                    break;
                }
            }
        }

        return shootValid;
    }

    public void Fire(float damage)
    {
        Instantiate(projectile, gunPosition.transform.position, Quaternion.identity);
    }
}
