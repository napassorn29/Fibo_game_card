using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectOpponentDeck : MonoBehaviour
{
    public static int OpdeckID;

    /* public TextMeshProUGUI TextDropPlayer;
    public TextMeshProUGUI TextDropOpponent;*/

    public void DeckInputOpponent(int opponent)
    {

        if (opponent == 0)
        {
            OpdeckID = 0;
        }
        if (opponent == 1)
        {
            OpdeckID = 0;
        }
        if (opponent == 2)
        {
            OpdeckID = 1;
        }
        Debug.Log("opponent : " + OpdeckID);
    }
}
