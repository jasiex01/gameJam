using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMaster : MonoBehaviour
{
    public RectTransform anchor;
    public Image card;
    
    // Start is called before the first frame update
    void Start()
    {
        // Example of instantiating cards
        for (int i = 0; i < 5; i++)
        {
            Image newCard = Instantiate(card, anchor);
            newCard.transform.localPosition += new Vector3(i * 100, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
