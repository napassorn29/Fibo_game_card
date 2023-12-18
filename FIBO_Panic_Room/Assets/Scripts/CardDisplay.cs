using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting.FullSerializer;
using JetBrains.Annotations;
using Unity.VisualScripting;

// Script ID #3
// Define all card display and behaviour
public class CardDisplay : MonoBehaviour
{
    // Card Game Object
    public List<Card> displayCard = new List<Card> ();
    
    // ID to display
    public int displayID;

    // Card parameters
    private int id;
    private string cardName;
    public string type;
    public int power;
    public int health;
    public int cost;
    private string cardDescription;
    private Sprite image;

    // Image adjusting
    public Image thisImage;
    public Image frame;

    // Card UI
    public GameObject CardBoarder;
    public GameObject CardFrame;
    public GameObject AttackDisplay;
    public GameObject HealthDisplay;
    public GameObject NameDisplay;
    public GameObject DescriptionBox;
    public GameObject CardImage;

    [SerializeField]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI descriptionText;

    private bool ableChangeDisplay = true;

    // Cardback adjusting
    public bool cardBack;
    public static bool staticCardBack;
    public static bool checkCardBack;
    // Hand and Deck
    public GameObject hand;
    public int numberOfCardInDeck;

    // Summoning
    public GameObject summonBoarder;

    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;
    public GameObject enemyZone;
    public GameObject board;
    private int placeHolderExistance;

    // Attacking
    public GameObject attackBorder;

    public GameObject target;
    public GameObject enemy;
    public bool summoningSickness;
    public bool cantAttack;
    public bool canAttack;

    public static bool staticTargeting;
    public static bool staticTargetingEnemy;
    public static bool staticSpellTargetingEnemy;

    public bool targeting;
    public bool targetingEnemy;
    public bool spellTargetingEnemy;

    public bool onlyThisCardAttack;

    // Destroy Card
    public bool canBeDestroyed;
    public GameObject graveyard;
    public bool beInGraveyard;

    // Card injury
    public int hurted;
    public int actualHealth;

    // Be targeted by enemy
    public bool isTarget;

    // Spell damage
    public int damageDealtBySpell;
    public bool spellCanDealDamage;
    public bool stopDealDamage;

    // Taunt
    public bool isTaunt;
    public bool hasTaunt;

    void Start()
    {
        // Initiate deck and Card
        numberOfCardInDeck = PlayerDeck.deckSize;
        displayCard[0] = CardDatabase.cardList[displayID];

        // Card can't be summoned until plyer's turn.
        canBeSummon = false;
        summoned = false;

        // Card can't attack until 1 turn after being summonned (this is called summoning sickness)
        canAttack = false;
        summoningSickness = true;
        targeting = false;
        targetingEnemy = false;

        // Place Holder are not exist until player can drag thier card in thier turn
        placeHolderExistance = 0;

        // Card are not destroyed
        beInGraveyard = false;
        canBeDestroyed = true;

        // Link game objects in this script to existed gameobjects in unity
        enemy       = GameObject.Find("OpponentHealth");
        battleZone  = GameObject.Find("PlayerDropPanel");
        enemyZone   = GameObject.Find("OpponentDropPanel");
        board       = GameObject.Find("Board");
        graveyard   = GameObject.Find("PlayerGraveyard");

        // Don't have taunt at first
        hasTaunt = false;
    }

    void Update()
    {
        // Inherit card parameter from card database
        id                  = displayCard[0].id;
        cardName            = displayCard[0].cardName;
        type                = displayCard[0].type;
        power               = displayCard[0].power;
        health              = displayCard[0].health;
        cost                = displayCard[0].cost;
        cardDescription     = displayCard[0].cardDescription;
        image               = displayCard[0].image;
        damageDealtBySpell  = displayCard[0].damageDealtBySpell;
        isTaunt             = displayCard[0].isTaunt;

        // Update health if card hurted
        actualHealth = health - hurted;

        // Display card parameters on card UI
        nameText.text           = "" + cardName;
        descriptionText.text    = "" + cardDescription;
        powerText.text          = "" + power;
        healthText.text         = "" + actualHealth;
        thisImage.sprite = image;

        // Change card appearance if it's a spell type
        if (type == "spell" && ableChangeDisplay)
        {
            AttackDisplay.SetActive(false);
            HealthDisplay.SetActive(false);
            attackBorder.SetActive(false);
            thisImage.transform.localScale = new Vector3(1.27f, 1.03f, 1);
            thisImage.transform.position = new Vector3(thisImage.transform.position.x, thisImage.transform.position.y - 2.5f, thisImage.transform.position.z);
            ableChangeDisplay = false;
        }

        // Display card's frame color
        if      (displayCard[0].color == "DarkGrey")    { frame.GetComponent<Image>().color = new Color32(40, 40, 40, 255); }
        else if (displayCard[0].color == "Blue")        { frame.GetComponent<Image>().color = new Color32(25, 50, 85, 255); }
        else if (displayCard[0].color == "Red")         { frame.GetComponent<Image>().color = new Color32(84, 25, 25, 255); }
        else if (displayCard[0].color == "Yellow")      { frame.GetComponent<Image>().color = new Color32(100, 65, 0, 255); }

        // Draw to hand
        if (this.tag == "Clone") // this.tag will be equal to "Clone" when this card is drawn from deck (clone a top deck card)
        {
            numberOfCardInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            displayCard[0] = PlayerDeck.staticDeck[numberOfCardInDeck];
            cardBack = false;
            this.tag = "Untagged";
        }

        // If cards are in player's hand -> don't display cardback
        hand = GameObject.Find("Hand");
        if (this.transform.parent == hand.transform.parent) { cardBack = false; }
        staticCardBack = cardBack;

        // Check if the place holder are existed or not. Will be use to correct the amount of unit in battle zone.
        for (int i = 0; i < battleZone.transform.childCount; i++)
        {
            if (battleZone.transform.GetChild(i).name == "New Game Object") { placeHolderExistance = 1; break; }
            else { placeHolderExistance = 0; }
        }

        // Player can summon this card and when it is player's turn, when player's has mana more or equal to cost, and if unit are not summoned or destroyed.
        if (TurnSystem.currentMana >= cost && summoned == false && beInGraveyard == false && TurnSystem.isYourTurn == true)
        {
            if (type == "spell") { canBeSummon = true; }
            if (type == "unit") 
            {
                if (battleZone.transform.childCount < (4 + placeHolderExistance)) { canBeSummon = true; } // If player battle zone has 4 units already, player can't summon this unit
                else { canBeSummon = false; }
            } 
        }
        else { canBeSummon= false; }

        // Cards can be dragged and display card's summon boarder  when able to summon this card.
        if (canBeSummon)    { gameObject.GetComponent<Drag>().enabled = true;  summonBoarder.SetActive(true); }
        else                { gameObject.GetComponent<Drag>().enabled = false; summonBoarder.SetActive(false); }

        // For unit card
        if (type == "unit")
        {
            // When this unit is not already summon, but player already drag it to the battle zone, summon this unit. Summon() will deduct player's mana by 1 and set this card summoned = true;
            if (summoned == false && this.transform.parent == battleZone.transform) { Summon(); }

            if (summoned == true) { checkCardBack = true; }
            if (summoned == false) { checkCardBack = false; }

            // Change unit appearance when unit has been summonned to the battle zone.
            if ((this.transform.parent.name == "PlayerDropPanel" || this.transform.parent.name == "OpponentDropPanel") && ableChangeDisplay)
            {
                CardBoarder.GetComponent<Image>().color = new Color32(25, 25, 25, 0);
                CardFrame.SetActive(false);
                NameDisplay.SetActive(false);
                DescriptionBox.SetActive(false);
                CardImage.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
                CardImage.transform.position = new Vector3(CardImage.transform.position.x, CardImage.transform.position.y - 23.125f, CardImage.transform.position.z);
                attackBorder.transform.localScale = CardImage.transform.localScale;
                attackBorder.transform.position = new Vector3(attackBorder.transform.position.x, attackBorder.transform.position.y - 21f, attackBorder.transform.position.z); ;
                ableChangeDisplay = false;

                AttackDisplay.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
                HealthDisplay.transform.localScale = new Vector3(1.25f, 1.25f, 1f);

                AttackDisplay.transform.position = new Vector3(AttackDisplay.transform.position.x + 13f, AttackDisplay.transform.position.y - 200f, 1f);
                HealthDisplay.transform.position = new Vector3(HealthDisplay.transform.position.x - 10.7f, HealthDisplay.transform.position.y - 198f, 1f);
            }

            // Cure the summoning sickness after being summoned for 1 turn
            if (TurnSystem.isYourTurn == false && summoned)
            {
                summoningSickness = false;
                cantAttack = false;
            }

            // This unit can attack if it is player's turn, unit is cured for summoning sickness, it's on player battle zone, and it power is greater than zero
            if (TurnSystem.isYourTurn && summoningSickness == false && cantAttack == false && power > 0 && this.transform.parent == battleZone.transform)   { canAttack = true; }
            else                                                                                                                                            { canAttack = false; }

            // If this unit can attack and not been destroy yet, display the attack boarder.
            if (canAttack && beInGraveyard == false) { attackBorder.SetActive(true); }
            else { attackBorder.SetActive(false); }

            // This unit will be destroyed if it's health is below zero.
            if (actualHealth <= 0)
            {
                DestroyCard();
                hurted = 0; // hurt reset
            }

            // If this card is attacking and is targetting on something, attack.
            if (targeting && onlyThisCardAttack) { Attack(); }

            // If player is targetting the enemy commander, set target to enemy.
            if (targetingEnemy) { target = enemy; }
            else { target = null; }
        }

        // For spell card
        if (type == "spell")
        {
            // Spell can do damage if damageDealtBySpell > 0
            if (damageDealtBySpell > 0) { spellCanDealDamage = true; }

            // If this spell can do damage and being cast to the battle zone, choosing target and deal damage to the target
            if (spellCanDealDamage && this.transform.parent == board.transform)
            {
                Arrow._Show = true;
                Arrow.startPoint = transform.position;
                dealxSpellDamage(damageDealtBySpell); // this will include choosing the target as same as in unit Attack()
            }

            // If player cancle the cast, stop choosing the target.
            if (stopDealDamage) { spellCanDealDamage = false; }

            // If player is targetting the enemy commander, set target to enemy.
            if (spellTargetingEnemy) { target = enemy; }
            else { target = null; }
        }

        // Update targeting and targeting enemy from the function outside this script
        targeting = staticTargeting;
        targetingEnemy = staticTargetingEnemy;
        spellTargetingEnemy = staticSpellTargetingEnemy;
    }

    // Deduct player's mana by 1 and set the summoned status to true.
    public void Summon()
    {
        TurnSystem.currentMana -= cost;
        summoned = true;
    }

    // This will be used by event trigger on the enemy commander UI to tell if player is targetting enemy commander or not
    public void TargetEnemy()
    {
        battleZone = GameObject.Find("PlayerDropPanel");
        foreach (Transform child in battleZone.transform)
        {
            if (child.GetComponent<CardDisplay>().isTaunt)
            {
                hasTaunt = true;
                break;
            }
            else
            {
                hasTaunt = false;
            }
        }
        if (hasTaunt)
        {
            staticTargetingEnemy = false;
        }
        else
        {
            staticTargetingEnemy = true;
        }
    }

    public void SpellTargetEnemy()
    {
        staticSpellTargetingEnemy = true;
    }

    public void UntargetEnemy()
    {
        staticTargetingEnemy = false;
        staticSpellTargetingEnemy = false;
    }

    // This will be used by event trigger on the CardToHand prefab to tell if player are draging this unit on the battle zone to attack or not
    public void StartAttack()
    {
        staticTargeting = true;

        if (canAttack == true)
        {
            Arrow._Show = true;
            Arrow.startPoint = transform.position;
        }
    }
    public void StopAttack()
    {
        staticTargeting = false;
        Arrow._Hide = true;
    }

    // This will be used by event trigger on the CardToHand prefab to tell if this unit is going to deal damage to enemy or not
    public void OneCardAttack()
    {
        onlyThisCardAttack = true;
    }
    public void OneCardAttackStop()
    {
        onlyThisCardAttack = false;
    }

    // Deal damage to targeted enemy if this unit is attacking and already be summoned
    public void Attack()
    {
        if (canAttack && summoned)
        {
            if (target != null) // If the target id enemy commander
            {
                if (target == enemy)
                {
                    OpponentHP.staticHP -= power;
                    targeting = false;
                    cantAttack = true;

                    Arrow._Hide = true;
                }
            }
            else // If the target is enemy unit
            {
                foreach (Transform child in enemyZone.transform)
                {
                    if (child.GetComponent<OpponentCardDisplay>().isTarget)
                    {
                        child.GetComponent<OpponentCardDisplay>().hurted += power;
                        hurted += child.GetComponent<OpponentCardDisplay>().power;
                        cantAttack = true;

                        Arrow._Hide = true;
                    }
                }
            }
        }
    }


    // Deal spell damage to targeted enemy 
    public void dealxSpellDamage(int x)
    {
        if (stopDealDamage == false)
        {
            if (target != null)
            {
                if (target == enemy && Input.GetMouseButton(0)) // If the target id enemy commander
                {
                    OpponentHP.staticHP -= x;
                    stopDealDamage = true;

                    Arrow._Hide = true;
                }
            }
            else // If the target is enemy unit
            {
                foreach (Transform child in enemyZone.transform)
                {
                    if (child.GetComponent<OpponentCardDisplay>().isTarget)
                    {
                        child.GetComponent<OpponentCardDisplay>().hurted += x;
                        stopDealDamage = true;

                        Arrow._Hide = true;
                    }
                }
            }
        }
    }

    // This will be used by event trigger on the CardToHand prefab to tell if this unit is being target or not
    public void BeingTarget()
    {
        if (isTaunt)
        {
            isTarget = true;
        }
        else
        {
            foreach (Transform child in battleZone.transform)
            {
                if (child.name != "New Game Object")
                {
                    if (child.GetComponent<CardDisplay>().isTaunt)
                    {
                        hasTaunt = true;
                        break;
                    }
                    else
                    {
                        hasTaunt = false;
                    }
                }
            }
            if (hasTaunt)
            {
                isTarget = false;
            }
            else
            {
                isTarget = true;
                hasTaunt = false;
            }
        }
    }
    public void DontBeingTarget()
    {
        isTarget = false;
    }

    // Destroyed card will go to the graveyard
    public void DestroyCard()
    {
        if (canBeDestroyed)
        {
            this.transform.SetParent(graveyard.transform);
            canBeDestroyed = false;
            summoned = false;
            beInGraveyard = true;
        }
    }
}
