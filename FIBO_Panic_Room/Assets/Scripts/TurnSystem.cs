using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script ID #4
// Turn system, random start turn, and describe the bahaviour of mana
public class TurnSystem : MonoBehaviour
{
    public static bool isYourTurn;
    public static bool isOpponentTurn;
    public int yourTurn;
    public int opponentTurn;
    public TextMeshProUGUI turnText;

    public int maxMana;
    public int oldMana;
    public static int currentMana;
    public TextMeshProUGUI manaText;

    public static bool startTurn;
    public static bool OpponentstartTurn;

    public int random;

    public static int maxEnemyMana;
    public static int oldEnemyMana;
    public static int currentEnemyMana;
    public TextMeshProUGUI enemyManaText;

    public Image OpponentHome;
    public Image PlayerHome;

    public GameObject playerBattleZone;
    public GameObject opponentBattleZone;

    public GameObject playerHand;
    public GameObject opponentHand;

    // Start is called before the first frame update
    void Start()
    {
        StartGame(); // Random to figured out who will start first
        OpponentHome = GameObject.Find("OpponentHome").GetComponent<Image>();
        PlayerHome = GameObject.Find("PlayerHome").GetComponent<Image>();

        playerBattleZone = GameObject.Find("PlayerDropPanel");
        opponentBattleZone = GameObject.Find("OpponentDropPanel");

        playerHand = GameObject.Find("Hand");
        opponentHand = GameObject.Find("OpponentHand");
    }

    // Update is called once per frame
    void Update()
    {
        // Display turn text
        if (isYourTurn)
        {
            turnText.text = "Player1 Turn";
            PlayerHome.color = Color.black;
            OpponentHome.color = new Color(0.435f, 0f, 0.004f); // Assuming RGB values between 0 and 1
                                                                // OpponentHomeOuterFrame.color = Color.default; // Commented out as it's not clear what you intend here
            DropZone playerDrop = playerBattleZone.GetComponent<DropZone>();
            DropZoneOpponent opponentDrop = opponentBattleZone.GetComponent<DropZoneOpponent>();

            DropZone playerHandDrop = playerHand.GetComponent<DropZone>();
            DropZoneOpponent opponentHandDrop = opponentHand.GetComponent<DropZoneOpponent>();

            playerDrop.enabled = true;
            opponentDrop.enabled = false;

            playerHandDrop.enabled = true;
            opponentHandDrop.enabled = false;
        }
        if(isOpponentTurn)
        {
            turnText.text = "Player2 Turn";
            OpponentHome.color = Color.black;
            PlayerHome.color = new Color(0f, 0.24f, 0.43f); // Assuming RGB values between 0 and 1

            DropZone playerDrop = playerBattleZone.GetComponent<DropZone>();
            DropZoneOpponent opponentDrop = opponentBattleZone.GetComponent<DropZoneOpponent>();

            DropZone playerHandDrop = playerHand.GetComponent<DropZone>();
            DropZoneOpponent opponentHandDrop = opponentHand.GetComponent<DropZoneOpponent>();

            playerDrop.enabled = false;
            opponentDrop.enabled = true;

            playerHandDrop.enabled = false;
            opponentHandDrop.enabled = true;
        }

        // Display mana text
        manaText.text = currentMana + "/" + maxMana;
        enemyManaText.text = currentEnemyMana+ "/" + maxEnemyMana;
    }

    public void EndYourTurn()
    {
        isYourTurn = false;
        isOpponentTurn = true;

        opponentTurn += 1;

        oldEnemyMana = currentEnemyMana;
        if (maxEnemyMana < 2) { maxEnemyMana += 1; }

        currentEnemyMana = maxEnemyMana + oldEnemyMana;
        if (currentEnemyMana > 5) { currentEnemyMana = 5; }

        OpponentstartTurn = true;
    }

    public void EndOpponentTurn()
    {
        isYourTurn= true;
        isOpponentTurn = false;

        yourTurn += 1;

        oldMana = currentMana;
        if (maxMana < 2) { maxMana += 1; }
 
        currentMana = maxMana + oldMana;
        if (currentMana > 5) { currentMana = 5; }

        startTurn = true;
       

    }

    // Random to figured out who will start first
    public void StartGame()
    {
        random = Random.Range(0, 2);
        if (random == 0)
        {
            isYourTurn = true;
            isOpponentTurn = false;

            yourTurn = 1;
            opponentTurn = 0;

            oldMana = 0;
            maxMana = 1;
            currentMana = 1;

            oldEnemyMana = 0;
            maxEnemyMana = 0;
            currentEnemyMana = 0;

            startTurn = false;
     
        }
        else
        {
            isYourTurn = false;
            isOpponentTurn = true;

            yourTurn = 0;
            opponentTurn = 1;

            oldMana = 0;
            maxMana = 0;
            currentMana = 0;

            oldEnemyMana = 0;
            maxEnemyMana = 1;
            currentEnemyMana = 1;

            oldEnemyMana = 0;
            OpponentstartTurn = false;
        }
    }
}
