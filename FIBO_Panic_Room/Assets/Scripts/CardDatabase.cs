using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script ID #2
// Tell The characteristics of cards 
public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList= new List<Card>();

    void Awake()
    {
        //                    ID     Card Name                  Type     Taunt atk hlt cst spl Description                                                                                                Image                                               Color             
        // Common Units
        cardList.Add(new Card(0,    "Kai",                      "unit",  false, 1,  1,  1,  0, "This unit can attack immediately after being summoned.",                                                  Resources.Load<Sprite>("kai"),                      "DarkGrey"));
        cardList.Add(new Card(1,    "Copter",                   "unit",  false, 2,  1,  1,  0, "When you play this card, draw 1 card and discard 1 card.",                                                Resources.Load<Sprite>("copter"),                   "DarkGrey"));
        cardList.Add(new Card(2,    "Wee",                      "unit",  false, 1,  3,  1,  0, "Opponent unit must attack this unit first.",                                                              Resources.Load<Sprite>("wee"),                      "DarkGrey"));
        cardList.Add(new Card(3,    "Yui",                      "unit",  false, 4,  4,  1,  0, "Can attack only leader, can not be targeted by any spells.",                                              Resources.Load<Sprite>("yui"),                      "DarkGrey"));
        cardList.Add(new Card(4,    "Earth",                    "unit",   true, 0,  5,  1,  0, "This unit can't attack. Opponent unit must attack this unit first.",                                      Resources.Load<Sprite>("earth"),                    "DarkGrey"));
        cardList.Add(new Card(5,    "Parn",                     "unit",  false, 1,  2,  1,  0, "When you play this card, give another unit +1/+1.",                                                       Resources.Load<Sprite>("parn"),                     "DarkGrey"));
        cardList.Add(new Card(6,    "Aj.Blink",                 "unit",  false, 1,  1,  1,  0, "Destroy any unit attacked by this unit.",                                                                 Resources.Load<Sprite>("blink"),                    "DarkGrey"));
        cardList.Add(new Card(7,    "Mhee",                     "unit",  false, 2,  2,  1,  0, "",                                                                                                        Resources.Load<Sprite>("mhee"),                     "DarkGrey"));
        cardList.Add(new Card(8,    "Nut",                      "unit",  false, 2,  1,  1,  0, "When you play this card, deal 1 damage to an enemy unit.",                                                Resources.Load<Sprite>("nut"),                      "DarkGrey"));
        cardList.Add(new Card(9,    "Dew",                      "unit",  false, 1,  3,  1,  0, "When attack, gain +1/0.",                                                                                 Resources.Load<Sprite>("dew"),                      "DarkGrey"));

        // Control Class Spells
        cardList.Add(new Card(10,   "P",                        "spell", false, 0,  0,  1,  1, "Deal 1 damage to enemy commander and 3 damage to an enemy follower.",                                     Resources.Load<Sprite>("P"),                        "Blue"));
        cardList.Add(new Card(11,   "I",                        "spell", false, 0,  0,  1,  2, "Deal 2 damage to enemy commander. Draw 1 card and discard 1 card.",                                       Resources.Load<Sprite>("I"),                        "Blue"));
        cardList.Add(new Card(12,   "D",                        "spell", false, 0,  0,  1,  2, "Deal 2 damage to enemy commander. Freeze an enemy follower.",                                             Resources.Load<Sprite>("D"),                        "Blue"));
        cardList.Add(new Card(13,   "PID",                      "spell", false, 0,  0,  1,  0, "Draw a card. If you already play all P I D, deal 5 damage to all enemy.",                                 Resources.Load<Sprite>("PID"),                      "Blue"));
        cardList.Add(new Card(14,   "Dynamic Dominator",        "spell", false, 0,  0,  1,  0, "Discard random enemy hand.",                                                                              Resources.Load<Sprite>("Dynamic Dominator"),        "Blue"));
        cardList.Add(new Card(15,   "System Overiding",         "spell", false, 0,  0,  1,  0, "Change a unit to 1/1.",                                                                                   Resources.Load<Sprite>("System Overiding"),         "Blue"));
        cardList.Add(new Card(16,   "LowPass Filter",           "spell", false, 0,  0,  1,  0, "Freeze all enemy unit that has more than 3 attacks.",                                                     Resources.Load<Sprite>("LowPass Filter"),           "Blue"));
        cardList.Add(new Card(17,   "HighPass Filter",          "spell", false, 0,  0,  1,  0, "Freeze all enemy unit that has less than 3 attacks.",                                                     Resources.Load<Sprite>("HighPass Filter"),          "Blue"));
        cardList.Add(new Card(18,   "State Estimator",          "spell", false, 0,  0,  1,  0, "Your opponent must reveal their hand.",                                                                   Resources.Load<Sprite>("State Estimator"),          "Blue"));

        // Machine Learning Class Spells
        cardList.Add(new Card(19,   "Data Manipulation",        "spell", false, 0,  0,  1,  0, "Reveal 2 top decks, put 1 to the bottom of your deck.",                                                   Resources.Load<Sprite>("Data Manipulation"),        "Yellow"));
        cardList.Add(new Card(20,   "OneHot-Encoding",          "spell", false, 0,  0,  1,  0, "Switch - unit:deal 2 damage to all enemies, spell: deal 4 damage to 1 unit.",                             Resources.Load<Sprite>("OneHot-Encoding"),          "Yellow"));
        cardList.Add(new Card(21,   "Tunning",                  "spell", false, 0,  0,  1,  0, "Reveal top 3 cards on your deck, you can rearrage it and put it to top or bottom deck.",                  Resources.Load<Sprite>("Hyperparameter Tunning"),   "Yellow"));
        cardList.Add(new Card(22,   "Outliner Domination",      "spell", false, 0,  0,  1,  0, "Destroy an enemy unit that have highest atk+def.",                                                        Resources.Load<Sprite>("Outliner Domination"),      "Yellow"));
        cardList.Add(new Card(23,   "Prediction",               "spell", false, 0,  0,  1,  0, "Switch - follower:deal 3 damage to enemy leader, spell: give all ally +1/0.",                             Resources.Load<Sprite>("Prediction the Result"),    "Yellow"));
        cardList.Add(new Card(24,   "\"Mai Sue Krub\"",         "spell", false, 0,  0,  1,  0, "Draw a card. Reveal top 3 cards on your deck, if it odered by spell-unit-spell-unit, you win the game!.", Resources.Load<Sprite>("Mai Sue Krub"),             "Yellow"));
    }
}
