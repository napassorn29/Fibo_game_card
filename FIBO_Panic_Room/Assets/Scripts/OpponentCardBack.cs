using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpponentCardBack : MonoBehaviour
{
    public GameObject OpponantcardBack;
    public static bool OpponentOpencard = false;

    // Start is called before the first frame update
    void Start()
    {
        /*cardBack.SetActive(true);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            // Access the parent GameObject (hand)
            GameObject parentObject = transform.parent.gameObject;

            // Check if the parent is a Hand
            if (parentObject.CompareTag("DropPanel")) // Use the appropriate tag or other identification method
            {
                DeleteCardBack();
                /*Debug.Log("This card is a child of a Hand.");*/
            }
            else
            {
                if (TurnSystem.isOpponentTurn && (OpponentOpencard))
                {
                    OpponantcardBack.SetActive(false);
                }
                else
                {
                    OpponantcardBack.SetActive(true);
                    OpponentOpencard = false;
                }
            }
        }
    }

    public void OpponentOpen()
    {
        OpponentOpencard = true;
    }
    void DeleteCardBack()
    {
        // Find the card back within the clone
        Transform cardBackTransform = transform.Find("CardBack"); // Assuming "CardBack" is the name of the card back GameObject

        // Check if the card back was found
        if (cardBackTransform != null)
        {
            // Destroy the card back
            Destroy(cardBackTransform.gameObject);
            this.enabled = false;
        }
        else
        {
            //Debug.LogWarning("Card back not found in the clone.");
        }
    }
}
