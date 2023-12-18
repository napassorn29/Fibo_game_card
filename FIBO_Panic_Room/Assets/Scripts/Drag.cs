using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Script ID #9
// Drag system to rearrange cards in hand and to summon. This will be attach to CardToHand which will be actived when it can be summonned or casted.
// For more information https://youtu.be/bMuYUOIAdnc?si=Mo4PRCc4nfIBI7py 

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;
    
    public GameObject placeHolder = null;
    public GameObject Graveyard;

    public void OnBeginDrag(PointerEventData eventData)
    {
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent); 
        RectTransform rt = placeHolder.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(this.GetComponent<LayoutElement>().preferredWidth, this.GetComponent<LayoutElement>().preferredHeight);

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex()); 

        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        CardDisplay cardDisplay = this.GetComponent<CardDisplay>();

        if (cardDisplay.type == "unit")
        {
            this.transform.position = eventData.position;

            if (placeHolder.transform.parent != placeHolderParent)
            {
                placeHolder.transform.SetParent(placeHolderParent);
            }

            int newSiblingIndex = placeHolderParent.childCount;

            for (int i = 0; i < placeHolderParent.childCount; i++)
            {
                if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
                {
                    newSiblingIndex = i;

                    if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                    {
                        newSiblingIndex--;
                    }
                    break;
                }
            }
            placeHolder.transform.SetSiblingIndex(newSiblingIndex);
        }
        else if (cardDisplay.type == "spell")
        {
            if (eventData.position.y <= 300)
            {
                this.transform.position = eventData.position;

                if (placeHolder.transform.parent != placeHolderParent)
                {
                    placeHolder.transform.SetParent(placeHolderParent);
                }

                int newSiblingIndex = placeHolderParent.childCount;

                for (int i = 0; i < placeHolderParent.childCount; i++)
                {
                    if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
                    {
                        newSiblingIndex = i;

                        if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                        {
                            newSiblingIndex--;
                        }
                        break;
                    }
                }
                placeHolder.transform.SetSiblingIndex(newSiblingIndex);
            }
            if (this.transform.parent == GameObject.Find("Board").transform && this.GetComponent<CardDisplay>().type == "spell" && this.GetComponent<CardDisplay>().spellCanDealDamage == false)
            {
                parentToReturnTo = Graveyard.transform;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CardDisplay cardDisplay = this.GetComponent<CardDisplay>();

        if (cardDisplay.type == "unit")
        {
            this.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        }
        else if (cardDisplay.type == "spell")
        {
            this.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
           
            if (parentToReturnTo == Graveyard.transform)
            {
                TurnSystem.currentMana -= cardDisplay.cost;
                this.GetComponent<CardDisplay>().canBeDestroyed = false;
                this.GetComponent<CardDisplay>().summoned = false;
                this.GetComponent<CardDisplay>().beInGraveyard = true;
            }
        }
        
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeHolder);
           
        
    }


    // Start is called before the first frame update
    void Start()
    {
        Graveyard = GameObject.Find("PlayerGraveyard");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
