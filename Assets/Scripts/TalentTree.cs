using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTree : MonoBehaviour
{
    [SerializeField] GameObject talentTreeWindow;

    private void OnMouseOver()
    {
        talentTreeWindow.SetActive(true);
    }
}
