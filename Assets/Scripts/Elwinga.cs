using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elwinga : MonoBehaviour
{
    [SerializeField] GameObject tarczoPrefab;
    [SerializeField] float summonCooldown = 5;
    [SerializeField] GameObject[] dialog;
    Animator animator;

    IEnumerator summoning;

    private void Start()
    {
        animator = GetComponent<Animator>();
        summoning = SummonTarczo();
        StartCoroutine(summoning);
    }

    public IEnumerator SummonTarczo()
    {
        while(GetComponent<Health>().GetHealth() > 0)
        {
            float countDown = summonCooldown;
            for (int i = 0; i < 10000; i++)
            {
                while (countDown >= 0)
                {
                    countDown -= Time.smoothDeltaTime;
                    yield return null;
                }

                animator.SetTrigger("summon");
            }
        }
    }

    private Transform[] RandomizeSpawn()
    {
        var spawners = FindObjectsOfType<AttackerSpawner>();

        AttackerSpawner spawner1;

        do
        {
            spawner1 = spawners[Random.Range(0, spawners.Length)];
        }
        while (spawner1.transform.position.y == 0);

        AttackerSpawner spawner2;

        do
        {
            spawner2 = spawners[Random.Range(0, spawners.Length)];
        }
        while (spawner2.transform.position.y == spawner1.transform.position.y || spawner2.transform.position.y == 0);

        return new Transform[] { spawner1.transform, spawner2.transform };
    }

    public void InstantiateTarczo()
    {
        var spawnValues = RandomizeSpawn();

        var Tarczo0 = Instantiate(tarczoPrefab, spawnValues[0].position, Quaternion.identity);
        AttachAddToSpawners(Tarczo0);

        var Tarczo1 = Instantiate(tarczoPrefab, spawnValues[1].position, Quaternion.identity);
        AttachAddToSpawners(Tarczo1);
    }

    private void AttachAddToSpawners(GameObject Add)
    {
        if(Add.transform.position.y == -2)
        {
            Add.transform.parent = GameObject.Find("Spawner (1)").transform;
        }
        else if (Add.transform.position.y == -1)
        {
            Add.transform.parent = GameObject.Find("Spawner (2)").transform;
        }
        else if (Add.transform.position.y == 1)
        {
            Add.transform.parent = GameObject.Find("Spawner (4)").transform;
        }
        else if (Add.transform.position.y == 2)
        {
            Add.transform.parent = GameObject.Find("Spawner (5)").transform;
        }
    }

    public GameObject[] GetDialog()
    {
        return dialog;
    }
}
