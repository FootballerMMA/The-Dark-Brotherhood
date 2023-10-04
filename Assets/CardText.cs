using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardText : MonoBehaviour
{
    [SerializeField] private Text card0Up;
    [SerializeField] private Text card0Down;
    [SerializeField] private Text card1Up;
    [SerializeField] private Text card1Down;
    [SerializeField] private Text card2Up;
    [SerializeField] private Text card2Down;
    [SerializeField] private Text card3Up;
    [SerializeField] private Text card3Down;
    [SerializeField] private Text card4Up;
    [SerializeField] private Text card4Down;

    private Text[] cardTexts = new Text[10];

    private void Start()
    {
        setArray();
        setCardLevels();
    }
    private void setArray(){
        cardTexts[0] = card0Up;
        cardTexts[1] = card0Down;
        cardTexts[2] = card1Up;
        cardTexts[3] = card1Down;
        cardTexts[4] = card2Up;
        cardTexts[5] = card2Down;
        cardTexts[6] = card3Up;
        cardTexts[7] = card3Down;
        cardTexts[8] = card4Up;
        cardTexts[9] = card4Down;
    }
    private void setCardLevels(){
        JitsuDeck deck = JitsuDeckSingleton.Instance.GetDeck();
        int x = 0;
        for (int i = 0; i < 10; i += 2, x++)
        {
            int upIndex = i;
            int downIndex = i + 1;
            string number = "";
            int numberInt = deck.hand[x].attackLevel;

            if (numberInt < 10) {
                number += "0";
            }

            number += numberInt.ToString();
            cardTexts[upIndex].text = number;
            cardTexts[downIndex].text = number;
        }
    }
}
