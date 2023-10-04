using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSprite : MonoBehaviour
{
    [SerializeField] private Image card0;
    [SerializeField] private Image card1;
    [SerializeField] private Image card2;
    [SerializeField] private Image card3;
    [SerializeField] private Image card4;
    [SerializeField] private Sprite snowSprite;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private Sprite waterSprite;

    private Image[] cardImages;

    private void Awake()
    {
        cardImages = new Image[5];
        cardImages[0] = card0;
        cardImages[1] = card1;
        cardImages[2] = card2;
        cardImages[3] = card3;
        cardImages[4] = card4;
    }

    private void Start()
    {
        setImageSprites();
    }

    private void setImageSprites()
    {
        JitsuDeck deck = JitsuDeckSingleton.Instance.GetDeck();

        for (int i = 0; i < 5; i++)
        {
            switch (deck.hand[i].element)
            {
                case 0:
                    cardImages[i].sprite = snowSprite;
                    break;
                case 1:
                    cardImages[i].sprite = fireSprite;
                    break;
                case 2:
                    cardImages[i].sprite = waterSprite;
                    break;
            }
        }
    }
}
