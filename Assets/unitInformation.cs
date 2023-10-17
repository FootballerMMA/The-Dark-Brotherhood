using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitInformation : MonoBehaviour
{
    [SerializeField] string unitName;
    [SerializeField] int attacksSnow;
    [SerializeField] int attacksFire;
    [SerializeField] int attacksWater;

    void Awake(){
        attacksSnow = 0;
        attacksFire = 0;
        attacksWater = 0;
    }

    public void increaseElementAttack(int specifier){
        switch (specifier){
            case 0:
                attacksSnow++;
                break;
            case 1:
                attacksFire++;
                break;
            case 2:
                attacksWater++;
                break;
        }
    }

    public string getName(){
        return unitName;
    }
    public int getSnowAttacks(){
        return attacksSnow;
    }
    public int getFireAttacks(){
        return attacksFire;
    }
    public int getWaterAttacks(){
        return attacksWater;
    }

}
