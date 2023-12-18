using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script ID #6
// Define characteristics of deck and which cards are in the deck. Shuffle algorithm, draw at the start of the game. This script also include Draw().
public class PlayerDeck : MonoBehaviour
{
    List<int> cardIdPool;

    public List<Card> deck = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();
    public List<Card> container = new List<Card>();

    private int x;
    public static int deckSize;

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;

    public GameObject cardToHand;
    public GameObject[] Clones;
    public GameObject Hand;

    public TextMeshProUGUI LoseText;
    public GameObject LoseTextGameObject;

    // Start is called before the first frame update
    void Start()
    {
        List<int> indexes = new List<int>() { };

        // Determine which deck is used based on deckID
        if (SelectPlayerDeck.deckID == 0)
        {
            cardIdPool = new List<int>() { 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 16, 17, 18, 1, 1, 4, 4, 8, 8};
        }
        else if (SelectPlayerDeck.deckID == 1)
        {
            cardIdPool = new List<int>() { 19, 19, 20, 20, 21, 21, 22, 23, 23, 24, 24, 1, 1, 2, 2, 7, 7, 3, 0, 0};
        }

        Hand = GameObject.Find("Hand");
        x = 0;
        int i = 0;
        deckSize = 20;

        // Random card sequence in deck
        while (indexes.Count < 20)
        {
            x = Random.Range(0, 20);
            if (indexes.Contains(x) == false)
            {
                indexes.Add(x);
                deck[i] = CardDatabase.cardList[cardIdPool[x]];
                i++;
            }
        }
      
        // Draw initial cards of 4
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        staticDeck = deck;

        // Deck Panel Display : amount of cards in deck panel will be change according to amount of card left in actual deck
        if (deckSize < 10) { cardInDeck1.SetActive(false); }
        if (deckSize < 5) { cardInDeck2.SetActive(false); }
        if (deckSize < 2) { cardInDeck3.SetActive(false); }
        if (deckSize < 1) { cardInDeck4.SetActive(false); }

        // At start of each turn, draw until 4
        if (TurnSystem.startTurn)
        {
            StartCoroutine(Draw(4 - Hand.transform.childCount));
            TurnSystem.startTurn = false;
        }
    }

    IEnumerator StartGame()
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
            Instantiate(cardToHand, transform.position, transform.rotation);
        }
    }

    IEnumerator Draw(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(cardToHand, transform.position, transform.rotation);
        }
    }

    public void Shuffle()
    {
        for(int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
    }
}
