using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script ID #5
// Will be attach to cardToHand prefab which will be initiate when draw by playerDeck script.
public class CardToHand : MonoBehaviour
{
    public GameObject Hand;
    public GameObject HandCard;

    // Start is called before the first frame update
    void Start()
    {
        Hand = GameObject.Find("Hand");
        HandCard.transform.SetParent(Hand.transform);
        HandCard.transform.localScale = Vector3.one;
        HandCard.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        HandCard.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
