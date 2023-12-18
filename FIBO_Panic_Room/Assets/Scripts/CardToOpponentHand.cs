using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToOpponentHand : MonoBehaviour
{
    public GameObject OpponentHand;
    public GameObject OpHandCard;

    // Start is called before the first frame update
    void Start()
    {
        OpponentHand = GameObject.Find("OpponentHand");
        OpHandCard.transform.SetParent(OpponentHand.transform);
        OpHandCard.transform.localScale = Vector3.one;
        OpHandCard.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        OpHandCard.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
