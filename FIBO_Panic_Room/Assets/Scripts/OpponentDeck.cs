using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpponentDeck : MonoBehaviour
{
    /*public static int OpdeckID = 0;*/
    List<int> OpcardIdPool;

    public List<Card> Opdeck = new List<Card>();
    public static List<Card> OpstaticDeck = new List<Card>();
    public List<Card> Opcontainer = new List<Card>();

    private int Opx;
    public static int OpdeckSize;

    public GameObject OpcardInDeck1;
    public GameObject OpcardInDeck2;
    public GameObject OpcardInDeck3;
    public GameObject OpcardInDeck4;

    public GameObject cardToOpponentHand;
    public GameObject[] OpClones;
    public GameObject OpponentHand;

    public TextMeshProUGUI OpLoseText;
    public GameObject OpLoseTextGameObject;

    // Start is called before the first frame update
    void Start()
    {
        List<int> Opindexes = new List<int>() { };

        // Determine which deck is used based on deckID
        if (SelectOpponentDeck.OpdeckID == 0)
        {
            OpcardIdPool = new List<int>() { 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 16, 17, 18, 1, 1, 4, 4, 8, 8 };
        }
        else if (SelectOpponentDeck.OpdeckID == 1)
        {
            OpcardIdPool = new List<int>() { 19, 19, 20, 20, 21, 21, 22, 23, 23, 24, 24, 1, 1, 2, 2, 7, 7, 3, 0, 0 };
        }

        OpponentHand = GameObject.Find("OpponentHand");
        Opx = 0;
        int j = 0;
        OpdeckSize = 20;

        // Random card sequence in deck
        while (Opindexes.Count < 20)
        {
            Opx = Random.Range(0, 20);
            if (Opindexes.Contains(Opx) == false)
            {
                Opindexes.Add(Opx);
                Opdeck[j] = CardDatabase.cardList[OpcardIdPool[Opx]];
                j++;
            }
        }

        // Draw initial cards of 4
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        OpstaticDeck = Opdeck;

        // Deck Panel Display : amount of cards in deck panel will be change according to amount of card left in actual deck
        if (OpdeckSize < 10) { OpcardInDeck1.SetActive(false); }
        if (OpdeckSize < 5) { OpcardInDeck2.SetActive(false); }
        if (OpdeckSize < 2) { OpcardInDeck3.SetActive(false); }
        if (OpdeckSize < 1) { OpcardInDeck4.SetActive(false); }

        // At start of each turn, draw until 4
        if (TurnSystem.OpponentstartTurn)
        {
            StartCoroutine(Draw(4 - OpponentHand.transform.childCount));
            TurnSystem.OpponentstartTurn = false;
        }
    }

    IEnumerator StartGame()
    {
        for (int j = 0; j < 4; j++)
        {
            Debug.Log("j : " + j);
            yield return new WaitForSeconds(1);
            Instantiate(cardToOpponentHand, transform.position, transform.rotation);
        }
    }

    IEnumerator Draw(int amount)
    {
        for (int j = 0; j < amount; j++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(cardToOpponentHand, transform.position, transform.rotation);
        }
    }

    public void Shuffle()
    {
        for (int j = 0; j < OpdeckSize; j++)
        {
            Opcontainer[0] = Opdeck[j];
            int randomIndex = Random.Range(j, OpdeckSize);
            Opdeck[j] = Opdeck[randomIndex];
            Opdeck[randomIndex] = Opcontainer[0];
        }
    }
}
