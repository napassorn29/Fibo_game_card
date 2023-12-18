using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script ID #15
// Arrow behaviour
public class Arrow : MonoBehaviour
{
    public GameObject[] points;
 
    public GameObject point;
    public GameObject cross;

    public GameObject canvas;

    public int numberOfPoints;

    public static Vector2 startPoint;

    public float distance;

    public Vector2 direction;
    public float force;

    public static bool _Show;
    public static bool _Hide;


    public GameObject CardToHand;
    public GameObject CardToOpponentHand;

    // Start is called before the first frame update
    void Start()
    {
        CardToHand = GameObject.Find("CardToHand");
        CardToOpponentHand = GameObject.Find("CardToOpponentHand");

        points = new GameObject[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            if (i != numberOfPoints - 1)
            {
                points[i] = Instantiate(point, transform.position, Quaternion.identity);
                points[i].SetActive(false);
            }
            else
            {
                points[i] = Instantiate(cross, transform.position, Quaternion.identity);
                points[i].SetActive(false);
            }
        }

        canvas = GameObject.Find("Board");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfPoints ; i++)
        {
            points[i].transform.position = Vector2.Lerp(startPoint, direction, i * 0.2f);

            direction = Input.mousePosition;

            distance = Vector3.Distance(startPoint, direction);

            force = distance / 10;

            if (_Show)
            {
                Show();
                _Show = false;
            }

            if (_Hide)
            {
                Hide();
                _Hide = false;
            }
        }
        //points[numberOfPoints - 1].transform.position = Input.mousePosition;
    }

    public void Show()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(true);
            points[i].transform.SetSiblingIndex(canvas.transform.childCount - i);
        }
    }

    public void Hide()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(false);
        }
    }
}
