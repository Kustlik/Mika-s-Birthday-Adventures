using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{  
    Defender defender;

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        var StarDisplay = FindObjectOfType<StarDisplay>();
        int defenderCost = defender.GetStarCost();

        if ((StarDisplay.HaveEnoughStars(defenderCost)) && (!IsThereAPlace()))
        {
            SpawnDefender(gridPos);
            StarDisplay.SpendStars(defenderCost);
        }
    }

    private bool IsThereAPlace()
    {
        var defPosition = FindObjectsOfType<Defender>();

        for(int index=0; index < defPosition.Length; index++)
        {
            float defPositionX = defPosition[index].transform.position.x;
            float defPositionY = defPosition[index].transform.position.y;

            Vector2 VecDefPos = new Vector2(defPositionX, defPositionY);

            if (VecDefPos == GetSquareClicked())
            {
                return true;
            }
        }
        
        return false;
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);

        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        return new Vector2(newX, newY);
    }

    private void SpawnDefender(Vector2 roundedPos)
    {
        Defender NewDefender = Instantiate(defender, roundedPos, Quaternion.identity);
        DefenderSorter(NewDefender);
    }

    private void DefenderSorter(Defender defender)
    {
        if(defender.transform.position.y == 2)
        {
            defender.transform.parent = GameObject.Find("Line (5)").transform;
        }
        else if(defender.transform.position.y == 1)
        {
            defender.transform.parent = GameObject.Find("Line (4)").transform;
        }
        else if (defender.transform.position.y == 0)
        {
            defender.transform.parent = GameObject.Find("Line (3)").transform;
        }
        else if (defender.transform.position.y == -1)
        {
            defender.transform.parent = GameObject.Find("Line (2)").transform;
        }
        else if (defender.transform.position.y == -2)
        {
            defender.transform.parent = GameObject.Find("Line (1)").transform;
        }
    }
}
