using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite snowCard;
    [SerializeField] Image fireCard;
    [SerializeField] Image waterCard;
    void Start()
    {
        image.sprite = snowCard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
