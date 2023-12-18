using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Script ID #10
// Script for drop zone
// For more information https://youtu.be/bMuYUOIAdnc?si=Mo4PRCc4nfIBI7py
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }
        
        Drag d = eventData.pointerDrag.GetComponent<Drag>();
        CardDisplay cardDisplay = d.GetComponent<CardDisplay>();
        if (d != null)
        {
            if (this.name == "PlayerDropPanel")
            {
                if (this.transform.childCount < 4 && cardDisplay.type == "unit")
                { d.placeHolderParent = this.transform; }
            }
            else
            {
                d.placeHolderParent = this.transform;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        Drag d = eventData.pointerDrag.GetComponent<Drag>();
        if (d != null && d.placeHolderParent == this.transform)
        {
            d.placeHolderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Drag d = eventData.pointerDrag.GetComponent<Drag>();
        if (d != null)
        {
            if (d.placeHolderParent == this.transform)
            { d.parentToReturnTo = this.transform; }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
