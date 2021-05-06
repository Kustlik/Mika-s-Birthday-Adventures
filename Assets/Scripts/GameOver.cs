using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] string gameOverString = "Wrozki urzadzily sobie uczte, kradnac caly twoj tort, chyba im na to nie pozwolisz?";
    bool triggerOnce = true;

    // Update is called once per frame
    void Update()
    {
        var playerLife = FindObjectOfType<PlayerLife>().GetLife();

        if(playerLife <= 0 && triggerOnce)
        {
            StartCoroutine(TriggerGameOverScreen());
            triggerOnce = false;
        }
    }

    private IEnumerator TriggerGameOverScreen()
    {
        float countDown = 3f;
        for (int i = 0; i < 10000; i++)
        {
            while (countDown >= 0)
            {
                if (GetComponentInChildren<Image>())
                {
                    GetComponentInChildren<Image>().color = new Color(0, 0, 0, ((3f - countDown) / 3f));
                }
                countDown -= Time.smoothDeltaTime;
                yield return null;
            }
        }
        
        StartCoroutine(TriggerTextType());
    }

    private IEnumerator TriggerTextType()
    {
        var mainGameOverText = transform.GetChild(1).GetComponent<Text>();

        for(int index = 0; index < gameOverString.Length; index++)
        {
            mainGameOverText.text = mainGameOverText.text + gameOverString[index];
            yield return new WaitForSeconds(0.05f);
        }

        transform.GetChild(2).gameObject.SetActive(true);

        yield return null;
    }
}
