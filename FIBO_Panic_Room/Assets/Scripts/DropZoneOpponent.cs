using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneOpponent : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        OpponentDrag Opd = eventData.pointerDrag.GetComponent<OpponentDrag>();
        OpponentCardDisplay OpponentcardDisplay = Opd.GetComponent<OpponentCardDisplay>();

        if (Opd != null)
        {
            if (this.name == "OpponentDropPanel")
            {
                if (this.transform.childCount < 4 && OpponentcardDisplay.type == "unit")
                { Opd.OpplaceHolderParent = this.transform; }
            }
            else
            {
                Opd.OpplaceHolderParent = this.transform;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        OpponentDrag Opd = eventData.pointerDrag.GetComponent<OpponentDrag>();
        if (Opd != null && Opd.OpplaceHolderParent == this.transform)
        {
            Opd.OpplaceHolderParent = Opd.OpparentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        OpponentDrag Opd = eventData.pointerDrag.GetComponent<OpponentDrag>();
        if (Opd != null)
        {
            if (Opd.OpplaceHolderParent == this.transform)
            { Opd.OpparentToReturnTo = this.transform; }
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
