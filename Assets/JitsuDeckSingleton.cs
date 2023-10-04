using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JitsuDeckSingleton : MonoBehaviour
{
    private static JitsuDeckSingleton instance;
    private JitsuDeck deck;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);

            deck = new JitsuDeck();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static JitsuDeckSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("JitsuDeckSingleton").AddComponent<JitsuDeckSingleton>();
                //DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public JitsuDeck GetDeck()
    {
        return deck;
    }
}
