using System;
using System.Collections.Generic;
using static JitsuElementSet;
using static JitsuCard;
using UnityEngine;
public class JitsuDeck
{
    JitsuElementSet snowSet, fireSet, waterSet;
    List<JitsuCard> deck;
    public List<JitsuCard> hand;
    List<JitsuCard> queue;

    public JitsuDeck()
    {
        snowSet = new JitsuElementSet(0);
        fireSet = new JitsuElementSet(1);
        waterSet = new JitsuElementSet(2);
        deck = new List<JitsuCard>();
        hand = new List<JitsuCard>();
        queue = new List<JitsuCard>();
        System.Random rnd = new System.Random();
        for (int i = 0; i < 3; i++)
        {
            createDeck(rnd, i);
        }
        shuffleDeck(rnd);
        createHand();
        createQueue();
    }

    private void createDeck(System.Random rnd, int specifier)
    {
        int i;
        int x = 0;
        JitsuCard[] tempCardSet = new JitsuCard[0];
        switch (specifier)
        {
            case 0:
                tempCardSet = snowSet.getSet();
                break;
            case 1:
                tempCardSet = fireSet.getSet();
                break;
            case 2:
                tempCardSet = waterSet.getSet();
                break;
        }
        for (i = 0; i < 4; i++, x++)
        {
            int randomNumber = rnd.Next(0, 15);
            JitsuCard temp = tempCardSet[randomNumber];
            if (temp.getDrawnToDeck())
            {
                i--;
                continue;
            }
            temp.setDrawnToDeck();
            deck.Add(temp);
        }
    }

    private void shuffleDeck(System.Random rnd)
    {
        for (int i = 0; i < 12; i++)
        {
            int randomNumber = rnd.Next(0, 12);
            swap(i, randomNumber);
        }
    }

    private void swap(int index1, int index2)
    {
        JitsuCard temp = deck[index1];
        deck[index1] = deck[index2];
        deck[index2] = temp;
    }

    private void printDeck()
    {
        foreach (JitsuCard card in deck)
        {
            Debug.Log(card);
        }
    }

    private void createHand()
    {
        for (int i = 0; i < 5; i++)
        {
            hand.Add(deck[i]);
        }
    }

    private void createQueue()
    {
        int size = deck.Count;
        for (int i = 5; i < 12; i++)
        {
            queue.Add(deck[i]);
        }
    }

    private void printHand()
    {
        foreach (JitsuCard card in hand)
        {
            Debug.Log(card);
        }
    }

    private void printQueue()
    {
        foreach (JitsuCard card in queue)
        {
            Debug.Log(card);
        }
    }

    public void printHAQ()
    {
        Debug.Log("The player hand is:");
        printHand();
        Debug.Log("The current queue is:");
        printQueue();
    }

    public JitsuCard chooseCard(int index)
    {
        JitsuCard cardChosen = hand[index];
        JitsuCard cardReplacement = popHeadQueue();
        hand[index] = cardReplacement;
        pushTailQueue(cardChosen);
        return cardChosen;
    }

    public JitsuCard getCardDetails(int index)
    {
        JitsuCard cardChosen = hand[index];
        return cardChosen;
    }

    private JitsuCard popHeadQueue()
    {
        JitsuCard card = queue[0];
        queue.RemoveAt(0);
        return card;
    }

    private void pushTailQueue(JitsuCard card)
    {
        queue.Add(card);
    }
}