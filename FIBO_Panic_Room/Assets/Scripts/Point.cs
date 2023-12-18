using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script ID #14
// Point for arrow behaviour
public class Point : MonoBehaviour
{
    public GameObject canvas;
    public bool isArrow;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Board");

        transform.SetParent(canvas.transform);
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
