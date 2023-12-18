using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

// Script ID #1
// Define cards parameters 
public class Card
{
    public int id;                  // Card id (unique of each card)
    public string cardName;         // Card Name 
    public string type;             // Type of cards ("unit" or "spell")
    public bool isTaunt;            // Is it a taunt unit
    public int power;               // Attack power for unit cards
    public int health;              // Health for unit cards
    public int cost;                // Cost (every cards have a cost of 1)
    public string cardDescription;  // Card Description
    public Sprite image;            // Card image
    public string color;            // Card frame color
        
    public int damageDealtBySpell;  // Damage the spell card can do
    public Card(){}

    // Card Constructor
    public Card(int id, string cardName, string type, bool isTaunt, int power, int health, int cost, int damageDealtBySpell, string cardDescription, Sprite image, string color)
    {
        this.id = id;
        this.cardName = cardName;
        this.type = type;
        this.power = power;
        this.health = health;
        this.cost = cost;
        this.cardDescription = cardDescription;
        this.image = image;
        this.color = color;
        this.damageDealtBySpell = damageDealtBySpell;
        this.isTaunt = isTaunt;
    }
}
