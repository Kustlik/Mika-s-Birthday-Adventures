using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    [SerializeField] float howLong = 1;
    [SerializeField] float wave1 = 0.2f;
    [SerializeField] float wave2 = 0.6f;
    [SerializeField] float moveSpawnersBy = -3f;

    bool[] Waves;
    float t;

    private void Start()
    {
        t = 0;
        Waves = new bool[] { false, false, false , false};
    }

    private void Update()
    {
        var playerLife = FindObjectOfType<PlayerLife>().GetLife();

        if (playerLife > 0)
        {
            t += Time.deltaTime;
            var barFilling = GetComponent<Image>().fillAmount;

            GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, (t / howLong));

            if (barFilling >= wave1 && barFilling < wave2)
            {
                Waves[1] = true;
            }
            else if (barFilling >= wave2 && barFilling < 1)
            {
                Waves[2] = true;
            }
            else if (barFilling >= 1)
            {
                Waves[1] = false;
                Waves[2] = false;

                if (FindObjectsOfType<Attacker>().Length <= 0)
                {
                    Waves[3] = true;
                }
            }
        }
    }

    public bool[] GetWaveStatus()
    {
        return Waves;
    }

    public IEnumerator StartDialog()
    {
        var mika = FindObjectOfType<Mika>().GetDialog();
        var elwi = FindObjectOfType<Elwinga>().GetDialog();
        var index = 0;

        List<GameObject> dialog = new List<GameObject>();
        dialog.Add(mika[0]);
        dialog.Add(elwi[0]);
        dialog.Add(elwi[1]);
        dialog.Add(mika[1]);
        dialog.Add(elwi[2]);

        while (dialog.Count > index)
        {
            dialog[index].SetActive(true);

            float countDown = 5;
            for (int i = 0; i < 10000; i++)
            {
                while (countDown >= 0)
                {
                    countDown -= Time.smoothDeltaTime;
                    yield return null;
                }

                dialog[index].SetActive(false);
            }

            index++;
        }
    }
}
