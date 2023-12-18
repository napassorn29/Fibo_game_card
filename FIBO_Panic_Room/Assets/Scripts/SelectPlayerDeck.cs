using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectPlayerDeck : MonoBehaviour
{
    public static int deckID;

    public void DeckInputPlayer(int val)
    {
        if (val == 0)
        {
            deckID = 0;
        }
        if (val == 1)
        {
            deckID = 0;
        }
        if (val == 2)
        {
            deckID = 1;
        }
    }
}
