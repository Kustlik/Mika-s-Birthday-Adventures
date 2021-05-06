using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffTalentTree : MonoBehaviour
{
    private void OnMouseEnter()
    {
        gameObject.SetActive(false);
    }
}
