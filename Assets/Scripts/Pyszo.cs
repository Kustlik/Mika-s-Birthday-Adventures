using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyszo : MonoBehaviour
{
    [SerializeField] GameObject flamestrikeTarget;
    [SerializeField] float startingCastTime = 10f;
    [SerializeField] float damage = 100;
    Animator animator;
    AttackerSpawner myLaneSpawner;
    IEnumerator castingCoroutine;
    bool targetLock;
    bool isDead;

    private void Start()
    {
        animator = GetComponent<Animator>();
        castingCoroutine = StartCasting();

        SetLaneSpawner();
        targetLock = false;
        isDead = false;
    }

    private void Update()
    {
        if (!targetLock && IsAttackerInLane() && !isDead)
        {
            animator.SetBool("isCasting", true);
            SetTarget();
        }
    }

    private void SetTarget()
    {
        int enemyPosX;

        if (IsAttackerInLane() == true && Mathf.RoundToInt(myLaneSpawner.transform.GetChild(0).transform.position.x) >= 4 ) // 7 is the last playable grid position
        {
            enemyPosX = 4;
        }
        else if (IsAttackerInLane() == true && Mathf.RoundToInt(myLaneSpawner.transform.GetChild(0).transform.position.x) < 4) // TOFIX, Pyszo is casting behind himself, needs to be fixed
        {
            enemyPosX = Mathf.RoundToInt(myLaneSpawner.transform.GetChild(0).transform.position.x);
        }
        else
        {
            enemyPosX = 0;
        }

        targetLock = true;
        flamestrikeTarget.transform.position = new Vector3(enemyPosX, flamestrikeTarget.transform.position.y, flamestrikeTarget.transform.position.z);

        castingCoroutine = StartCasting();
        StartCoroutine(castingCoroutine);
    }

    private IEnumerator StartCasting()
    {
        float countDown = startingCastTime;
        for (int i = 0; i < 10000; i++)
        {
            while (countDown >= 0)
            {
                countDown -= Time.smoothDeltaTime;
                yield return null;
            }
        }

        GameObject burnedGround = Instantiate(flamestrikeTarget, flamestrikeTarget.transform.position, Quaternion.identity);
        burnedGround.GetComponent<BurningGround>().SetDamage(damage);
        burnedGround.GetComponent<BurningGround>().StartBurning();
        targetLock = false;
        animator.SetTrigger("castEnd");
        animator.SetBool("isCasting", false);
    }

    public void StopCasting()
    {
        StopCoroutine(castingCoroutine);
        isDead = true;
    }

    private void SetLaneSpawner()
    {
        var EnemySpawners = FindObjectsOfType<AttackerSpawner>();

        foreach(AttackerSpawner spawner in EnemySpawners)
        {
            if (Mathf.Abs(transform.position.y - spawner.transform.position.y) <= Mathf.Epsilon)
            {
                myLaneSpawner = spawner;
            }
        }
    }

    public bool IsAttackerInLane()
    {
        if (myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
