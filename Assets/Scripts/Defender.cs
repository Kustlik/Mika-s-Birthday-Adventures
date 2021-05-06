using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int starCost = 100;
    [SerializeField] List<GameObject> currentTarget;
    [Range(0f, 5f)] float currentSpeed = 0f;

    public void AddStars(int amount)
    {
        FindObjectOfType<StarDisplay>().AddStars(amount);
    }

    public int GetStarCost()
    {
        return starCost;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
        if (!gameObject.GetComponent<Mika>())
        {
            UpdateAnimationState();
        }
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public void Attack(GameObject target)
    {
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetBool("isAttacking", true);
        }
        currentTarget.Add(target);
    }

    public void StrikeCurrentTarget(float damage)
    {
        if (currentTarget.Count == 0) { return; }

/*        foreach(GameObject enemy in currentTarget)
        {
            Health health = enemy.GetComponent<Health>();
            if (health)
            {
                health.DealDamage(damage);
                if(health.GetHealth() <= 0)
                {
                    currentTarget.Remove(enemy);
                }
            }
        }  */

        for(int index = 0; index < currentTarget.Count; index++)
        {
            Health health = currentTarget[index].GetComponent<Health>();
            if (health)
            {
                health.DealDamage(damage);
                if (health.GetHealth() <= 0)
                {
                    currentTarget.Remove(currentTarget[index]);
                    if (currentTarget.Count != 0)
                    {
                        index--;
                    }
                }
            }
        }
    }

    public void UpdateAnimationState()
    {
        if (currentTarget.Count == 0)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
    }
}
