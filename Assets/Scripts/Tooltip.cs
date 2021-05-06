using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] Text rankText;
    [SerializeField] GameObject highlight;
    [SerializeField] GameObject tooltip;
    [SerializeField] float positionWhileStretching = 0.071f;
    [SerializeField] float stretchValue = 0.044f;
    [SerializeField] Talents[] talents;
    [SerializeField] Tooltip checkForMaxRank;

    [SerializeField] GameObject talentIconSlot;
    [SerializeField] Sprite talentIcon;
    [SerializeField] Sprite talentIconGrey;
 
    [SerializeField] GameObject[] arrowMainSprite;
    [SerializeField] Sprite[] arrowGrey;
    [SerializeField] Sprite[] arrowYellow;
    [SerializeField] Sprite frameGrey;
    [SerializeField] Sprite frameYellow;
    [SerializeField] Sprite frameGreen;

    GameObject tempTooltip;
    [SerializeField] int rank; // TO DELETE SERIALFIELD

    //Cache
    float startingTooltipPosY;
    float startingTooltipScaleY;

    float startingPropertiesPosY;

    private void Start()
    {
        rank = 0;
        UpdateRankText();
        ChangeFrameState();
    }

    public void ResetTalents()
    {
        Start();
        FindObjectOfType<TalentPoints>().ResetTalentPoints();
    }

    private void OnMouseEnter()
    {
        highlight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        tempTooltip = Instantiate(tooltip, transform.position, Quaternion.identity);
        CacheTooltipTransform();
        GenerateTooltip();
    }

    private void OnMouseExit()
    {
        highlight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        Destroy(tempTooltip);
    }

    private void OnMouseDown()
    {
        Talents currentRank;
        int talentPoints = FindObjectOfType<TalentPoints>().GetTalentPoints();
        int talentsLeft = FindObjectOfType<TalentPoints>().GetTalentsLeft();

        if (rank == 0) //Temporary workaround for displaying descriptions in 0->1 interval
        {
            currentRank = talents[rank];
        }
        else
        {
            currentRank = talents[rank - 1];
        }

        if (IsAvailableToLearn(currentRank, talentPoints, talentsLeft) == true)
        {
            FindObjectOfType<TalentPoints>().SpendTalentPoints();
            rank++;
            UpdateRankText();
        }

        GenerateTooltip();
        UpdateAllObjects();
    }

    private void GenerateTooltip()
    {
        GameObject description = tempTooltip.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        GameObject skillProperties = tempTooltip.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        int talentPoints = FindObjectOfType<TalentPoints>().GetTalentPoints();
        int talentsLeft = FindObjectOfType<TalentPoints>().GetTalentsLeft();
        Talents currentRank;

        skillProperties.GetComponent<Text>().text = "";

        if (talents[0].GetIsActiveSkillType() == true)
        {
            if (talents[0].GetRange() == 0)
            {
                skillProperties.GetComponent<Text>().text = "Melee Range";
            }
            else
            {
                skillProperties.GetComponent<Text>().text = talents[0].GetRange().ToString() + " yd range";
            }
            if (talents[0].GetCooldown() > 0 && talents[0].GetCooldown() < 60)
            {
                skillProperties.GetComponent<Text>().text = skillProperties.GetComponent<Text>().text + "\n" + talents[0].GetCooldown().ToString() + " sec cooldown";
            }
            else if (talents[0].GetCooldown() >= 60)
            {
                float tempCdValue = talents[0].GetCooldown() / 60;
                skillProperties.GetComponent<Text>().text = skillProperties.GetComponent<Text>().text + "\n" + tempCdValue.ToString("F1").Replace(",", ".") + " min cooldown";
            }
        }

        if (rank == 0) //Temporary workaround for displaying descriptions in 0->1 interval
        {
            currentRank = talents[rank];
        }
        else
        {
            currentRank = talents[rank - 1];
        }

        description.GetComponent<Text>().text =
            "<size=24><b>" + currentRank.GetTalentName() + "</b></size> \n" +
            "Rank " + rank + "/" + talents.Length + "\n";

        GenerateTalentRequirements(currentRank, talentPoints, description);

        if(currentRank.GetIsActiveSkillType() == true)
        {
            description.GetComponent<Text>().text = description.GetComponent<Text>().text + currentRank.GetCost() + " " + currentRank.GetCostType() + "\n" +
            currentRank.GetCastTime() + "\n";
        }

        GenerateSkillRequirements(currentRank, description);

        if (rank != 0)
        {
            description.GetComponent<Text>().text = description.GetComponent<Text>().text + "<color=#FFDB00>" + currentRank.GetTalentEffect() + "</color>";
        }

        if (rank < talents.Length)
        {
            if (rank != 0)
            {
                description.GetComponent<Text>().text = description.GetComponent<Text>().text + "\n\n" + "Next rank:" + "\n";
            }
            description.GetComponent<Text>().text = description.GetComponent<Text>().text + "<color=#FFDB00>" + talents[rank].GetTalentEffect() + "</color>";
        }

        if (IsAvailableToLearn(currentRank, talentPoints, talentsLeft) == true)
        {
            description.GetComponent<Text>().text = description.GetComponent<Text>().text + "\n\n" + "<color=#34FF00>Click to Learn</color>";
        }

        AdjustBackground();
        AdjustSkillPropertiesText();
    }

    private void GenerateTalentRequirements(Talents currentRank, int talentPoints, GameObject description)
    {
        if (talentPoints < currentRank.GetTalentPointsRequirement())
        {
            description.GetComponent<Text>().text = description.GetComponent<Text>().text + "<color=#FF0000>Requires " + currentRank.GetTalentPointsRequirement().ToString() + " points in Talents </color> \n";
        }
        if (currentRank.GetTalentRequirement() != null && checkForMaxRank != null)
        {
            if (checkForMaxRank.IsMaxRanking() == false && (checkForMaxRank.talents.Length - checkForMaxRank.GetRank()) == 1)
            {
                description.GetComponent<Text>().text = description.GetComponent<Text>().text + "<color=#FF0000>Requires " + (checkForMaxRank.talents.Length - checkForMaxRank.GetRank()).ToString() + " point in " + currentRank.GetTalentRequirement().GetTalentName() + " </color> \n";
            }
            if (checkForMaxRank.IsMaxRanking() == false && (checkForMaxRank.talents.Length - checkForMaxRank.GetRank()) != 1)
            {
                description.GetComponent<Text>().text = description.GetComponent<Text>().text + "<color=#FF0000>Requires " + (checkForMaxRank.talents.Length - checkForMaxRank.GetRank()).ToString() + " points in " + currentRank.GetTalentRequirement().GetTalentName() + " </color> \n";
            }
        }
    }

    private void GenerateSkillRequirements(Talents currentRank, GameObject description)
    {
        if (currentRank.GetSkillRequirements().Length > 0)
        {
            foreach (string requirement in currentRank.GetSkillRequirements())
            {
                description.GetComponent<Text>().text = description.GetComponent<Text>().text + "<color=#FF0000>" + requirement + " </color> \n";
            }
        }
    }

    private bool IsAvailableToLearn(Talents currentRank, int talentPoints, int talentsLeft)
    {
        bool talentCheck = false;

        if (talentPoints >= currentRank.GetTalentPointsRequirement() && (rank < talents.Length) && talentsLeft > 0)
        {
            talentCheck = true;

            if (currentRank.GetTalentRequirement() != null && checkForMaxRank != null)
            {
                if (checkForMaxRank.IsMaxRanking() == true)
                {
                    talentCheck = true;
                }
                else
                {
                    talentCheck = false;
                }
            }
        }

        return talentCheck;
    }

    private int HowManyRequirementsNotMet(int talentPoints)
    {
        int notMet = 0;

        if (talentPoints < talents[0].GetTalentPointsRequirement() && (rank < talents.Length))
        {
            notMet++;
        }
        if (talents[0].GetTalentRequirement() != null && checkForMaxRank != null)
        {
            if (checkForMaxRank.IsMaxRanking() == false)
            {
                notMet++;
            }
        }

        return notMet;
    }

    public void UpdateRankText()
    {
        rankText.text = rank + "/" + talents.Length;
    }

    public bool IsMaxRanking()
    {
        if (rank == talents.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetRank()
    {
        return rank;
    }

    public void ChangeFrameState()
    {
        int talentPoints = FindObjectOfType<TalentPoints>().GetTalentPoints();
        int talentsLeft = FindObjectOfType<TalentPoints>().GetTalentsLeft();

        if (rank < talents.Length)
        {
            if (IsAvailableToLearn(talents[rank], talentPoints, talentsLeft) == false && rank == 0)
            {
                GetComponent<SpriteRenderer>().sprite = frameGrey;
                talentIconSlot.GetComponent<SpriteRenderer>().sprite = talentIconGrey;
                rankText.color = new Color(0.5f, 0.5f, 0.5f);
                ChangeArrowToGrey();
            }
            else if (IsAvailableToLearn(talents[rank], talentPoints, talentsLeft) == true && rank < talents.Length)
            {
                GetComponent<SpriteRenderer>().sprite = frameGreen;
                talentIconSlot.GetComponent<SpriteRenderer>().sprite = talentIcon;
                rankText.color = new Color(1f, 1f, 1f);
                ChangeArrowToYellow();
            }
        }
        else if (rank == talents.Length)
        {
            GetComponent<SpriteRenderer>().sprite = frameYellow;
            talentIconSlot.GetComponent<SpriteRenderer>().sprite = talentIcon;
            rankText.color = new Color(1f, 1f, 1f);
            ChangeArrowToYellow();
        }
    }

    public void ChangeArrowToYellow()
    {
        if (arrowMainSprite.Length == 0)
        {
            return;
        }
        else
        {
            if (arrowMainSprite[0].GetComponent<SpriteRenderer>().sprite == arrowGrey[0])
            {
                for (int i = 0; i < arrowMainSprite.Length; i++)
                {
                    arrowMainSprite[i].GetComponent<SpriteRenderer>().sprite = arrowYellow[i];
                }
            }
        }
    }

    public void ChangeArrowToGrey()
    {
        if (arrowMainSprite.Length == 0)
        {
            return;
        }
        else
        {
            if (arrowMainSprite[0].GetComponent<SpriteRenderer>().sprite == arrowYellow[0])
            {
                for (int i = 0; i < arrowMainSprite.Length; i++)
                {
                    arrowMainSprite[i].GetComponent<SpriteRenderer>().sprite = arrowGrey[i];
                }
            }
        }
    }

    public void UpdateAllObjects()
    {
        Tooltip[] tooltipGameObjects = FindObjectsOfType<Tooltip>();

        foreach (Tooltip tooltipObject in tooltipGameObjects)
        {
            tooltipObject.GetComponent<Tooltip>().ChangeFrameState();
        }
    }

    public void AdjustBackground()
    {
        Text description = tempTooltip.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        GameObject tooltipBg = tempTooltip.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

        Canvas.ForceUpdateCanvases();
        int linesOfText = description.cachedTextGenerator.lines.Count;

        tooltipBg.transform.localScale = new Vector3(tooltipBg.transform.localScale.x, startingTooltipScaleY + (stretchValue * linesOfText), tooltipBg.transform.localScale.z);
        tooltipBg.transform.localPosition = new Vector3(tooltipBg.transform.localPosition.x, startingTooltipPosY + (positionWhileStretching * linesOfText), tooltipBg.transform.localPosition.z);
    }

    public void AdjustSkillPropertiesText()
    {
        GameObject description = tempTooltip.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        GameObject skillPropertiesText = tempTooltip.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        int talentPoints = FindObjectOfType<TalentPoints>().GetTalentPoints();

        Canvas.ForceUpdateCanvases();
        int linesOfText = description.GetComponent<Text>().cachedTextGenerator.lines.Count;
        
        if(linesOfText > 3)
        {
            //Magic numbers - 0.208f it's in position between each text line, subtracting its position by 3 allows it to be always positioned below Talent Name and Ranking;
            skillPropertiesText.transform.localPosition = new Vector3(skillPropertiesText.transform.localPosition.x, startingPropertiesPosY + (0.208f * (linesOfText - 3 - HowManyRequirementsNotMet(talentPoints))), skillPropertiesText.transform.localPosition.z);
        }
    }

    private void CacheTooltipTransform()
    {
        startingTooltipPosY = tempTooltip.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.localPosition.y;
        startingTooltipScaleY = tempTooltip.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.localScale.y;

        startingPropertiesPosY = tempTooltip.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.localPosition.y;
    }
}
