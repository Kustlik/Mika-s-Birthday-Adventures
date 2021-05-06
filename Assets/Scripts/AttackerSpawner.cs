using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] Attacker[] attackerPrefab;
    [SerializeField] Attacker boss;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
 
    bool spawn = true;
    float randomSpawnTime;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            randomSpawnTime = Random.Range(minSpawnDelay, maxSpawnDelay);

            yield return new WaitForSeconds(randomSpawnTime);
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        bool[] waveStatus = FindObjectOfType<TimeBar>().GetWaveStatus();

        if (waveStatus[1] && !waveStatus[2] && !waveStatus[3])
        {
            if(attackerPrefab.Length != 0)
            {
                Spawn(attackerPrefab[0]);
            }
        }
        else if(waveStatus[1] && waveStatus[2] && !waveStatus[3])
        {
            if (attackerPrefab.Length > 0)
            {
                int randomEnemyIndex = Random.Range(0, attackerPrefab.Length);

                Spawn(attackerPrefab[randomEnemyIndex]);
            }
        }
        else if (!waveStatus[1] && !waveStatus[2] && waveStatus[3])
        {
            if (boss)
            {
                var dialogScript = FindObjectOfType<TimeBar>();

                Spawn(boss);
                spawn = false;

                if (FindObjectOfType<Mika>())
                {
                    StartCoroutine(dialogScript.StartDialog());
                }
            }
        }
    }

    private void Spawn(Attacker enemy)
    {
        Attacker newAttacker = Instantiate(enemy, transform.position, Quaternion.identity);
        newAttacker.transform.parent = transform;
    }

    public void SetBossSpawnTime()
    {
        minSpawnDelay = 0;
        maxSpawnDelay = 0;
    }
}
