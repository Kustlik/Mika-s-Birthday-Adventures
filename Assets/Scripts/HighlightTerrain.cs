using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightTerrain : MonoBehaviour
{
    private void OnMouseOver()
    {
        GetComponent<Image>().color = new Color32(255,255,255,130);
    }

    private void OnMouseExit()
    {
        GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }
}
