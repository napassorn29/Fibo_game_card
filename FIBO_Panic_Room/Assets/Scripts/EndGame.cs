using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script ID #13
// Win condition and result display
public class EndGame : MonoBehaviour
{
    public GameObject textObject;
    public TextMeshProUGUI resultText;

    // Start is called before the first frame update
    void Start()
    {
        textObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHP.staticHP <= 0 || PlayerDeck.deckSize <= 0)
        {
            textObject.SetActive(true);
            resultText.text = "Player2 WIN!!";
        }
        else if (OpponentHP.staticHP <= 0)
        {
            textObject.SetActive(true);
            resultText.text = "Player1 WIN!!";
        }
    }
}
