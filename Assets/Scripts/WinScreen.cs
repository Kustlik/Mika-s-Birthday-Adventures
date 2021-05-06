using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] string gameOverString = "Haha! To bylam ja Lady UwU! Dalas sie nabrac! Musimy kiedys powtorzyc ta zabawe! \n Wszystkiego najlepszego z okazji urodzin Mika!";
    bool triggerOnce = true;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Elwinga>())
        {
            var bossLife = FindObjectOfType<Elwinga>().GetComponent<Health>().GetHealth();

            if (bossLife <= 0 && triggerOnce)
            {
                StartCoroutine(TriggerWinScreen());
                StartCoroutine(SpriteFadeIn());
                triggerOnce = false;
            }
        }
    }

    private IEnumerator TriggerWinScreen()
    {
        float countDown = 3f;
        for (int i = 0; i < 10000; i++)
        {
            while (countDown >= 0)
            {
                if (GetComponentInChildren<Image>())
                {
                    GetComponentInChildren<Image>().color = new Color(1, 1, 1, ((3f - countDown) / 3f));
                }
                countDown -= Time.smoothDeltaTime;
                yield return null;
            }
        }

        StartCoroutine(TriggerTextType());
    }

    private IEnumerator SpriteFadeIn()
    {
        float countDown = 3f;
        for (int i = 0; i < 10000; i++)
        {
            while (countDown >= 0)
            {
                if (transform.GetChild(3).GetComponentInChildren<SpriteRenderer>())
                {
                    transform.GetChild(3).GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, ((3f - countDown) / 3f));
                }
                countDown -= Time.smoothDeltaTime;
                yield return null;
            }
        }
    }

    private IEnumerator TriggerTextType()
    {
        var mainWinText = transform.GetChild(1).GetComponent<Text>();

        for (int index = 0; index < gameOverString.Length; index++)
        {
            mainWinText.text = mainWinText.text + gameOverString[index];
            yield return new WaitForSeconds(0.05f);
        }

        transform.GetChild(2).gameObject.SetActive(true);

        yield return null;
    }
}
