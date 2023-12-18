using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script ID #8
// UI of deck
public class DeckPanelCard : MonoBehaviour
{
    public GameObject cardBack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CardDisplay.staticCardBack) { cardBack.SetActive(true); }
        else { cardBack.SetActive(false); }
    }
}
