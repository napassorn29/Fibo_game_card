using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script ID #12
// Enemy HP display and behaviour
public class OpponentHP : MonoBehaviour
{
    public static float maxHP;
    public static float staticHP;
    public float hp;
    public Image hpCircle;
    public TextMeshProUGUI hpText;
    public Image heroImage;

    // Start is called before the first frame update
    void Start()
    {
        heroImage.sprite = Resources.Load<Sprite>("PlayerClass" + SelectOpponentDeck.OpdeckID);
        maxHP = 10;
        staticHP = 10;
    }

    // Update is called once per frame
    void Update()
    {
        hp = staticHP;
        hpCircle.fillAmount = hp / maxHP;

        if (hp >= maxHP)
        {
            hp = maxHP;
        }

        hpText.text = hp + "";
    }
}
