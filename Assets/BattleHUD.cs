using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text snowCount;
    public Text fireCount;
    public Text waterCount;
    int snowC = 1;
    int waterC = 1;
    int fireC = 1;
    public void SetHUD(unitInformation unit){
        nameText.text = unit.getName();
    }
    public void addAttack(int specifier){
        string s = "x";
        switch (specifier)
        {
            case 0:
                s+= (snowC++);
                snowCount.text = s;
                break;
            case 1:
                s+= (fireC++);
                fireCount.text = s;
                break;
            case 2:
                s+= (waterC++);
                waterCount.text = s;
                break;
        }
    }
}
