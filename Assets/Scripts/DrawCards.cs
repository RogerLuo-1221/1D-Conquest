using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject card;
    public GameObject playerArea;

    public void OnClick()
    {
        for (var i = 0; i < 5; i++)
        {
            var newCard = Instantiate(card, Vector2.zero, Quaternion.identity);
            newCard.transform.SetParent(playerArea.transform, false);
        }
    }
}
