using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentPoints : MonoBehaviour
{
    [SerializeField] int talentPointsLeft = 31;
    [SerializeField] Text talentPointsLeftText;

    int talentPoints;

    private void Start()
    {
        talentPoints = 0;
        UpdateTalentPoints();
    }

    private void UpdateTalentPoints()
    {
        gameObject.GetComponent<Text>().text = talentPoints.ToString() + " Points";
        talentPointsLeftText.text = talentPointsLeft.ToString() + " Points Left";
    }

    public void SpendTalentPoints()
    {
        if (talentPointsLeft > 0)
        {
            talentPoints++;
            talentPointsLeft--;
            UpdateTalentPoints();
        }
    }

    public int GetTalentPoints()
    {
        return talentPoints;
    }

    public int GetTalentsLeft()
    {
        return talentPointsLeft;
    }

    public void ResetTalentPoints()
    {
        talentPoints = 31;
        Start();
    }
}
