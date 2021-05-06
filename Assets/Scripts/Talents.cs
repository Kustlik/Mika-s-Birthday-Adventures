using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talent", menuName = "Talent")]
public class Talents : ScriptableObject
{
    [SerializeField] bool isActiveSkill = false;
    [SerializeField] string talentName;
    [SerializeField] int ranking;
    [SerializeField] Talents talentRequirement;
    [SerializeField] int talentPointsRequirement;
    [SerializeField] int cost;
    [SerializeField] string costType;
    [SerializeField] string castTime;
    [SerializeField] string[] skillRequirements;

    [SerializeField] int range;
    [SerializeField] int cooldown;

    [TextArea(minLines: 10, maxLines: 10)][SerializeField] string talentEffect;

    public bool GetIsActiveSkillType() { return isActiveSkill; }

    public string GetTalentName() { return talentName; }

    public int GetRanking() { return ranking; }

    public Talents GetTalentRequirement() { return talentRequirement; }

    public int GetTalentPointsRequirement() { return talentPointsRequirement; }

    public int GetCost() { return cost; }

    public string GetCostType() { return costType; }

    public string GetCastTime() { return castTime; }

    public string[] GetSkillRequirements() { return skillRequirements; }

    public int GetRange() { return range; }

    public int GetCooldown() { return cooldown; }

    public string GetTalentEffect() { return talentEffect; }
}
