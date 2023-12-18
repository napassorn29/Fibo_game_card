using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpponentDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform OpparentToReturnTo = null;
    public Transform OpplaceHolderParent = null;

    public GameObject OpplaceHolder = null;
    public GameObject OpponentGraveyard;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OpplaceHolder = new GameObject();
        OpplaceHolder.transform.SetParent(this.transform.parent);
        RectTransform rt = OpplaceHolder.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(this.GetComponent<LayoutElement>().preferredWidth, this.GetComponent<LayoutElement>().preferredHeight);

        OpplaceHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        OpparentToReturnTo = this.transform.parent;
        OpplaceHolderParent = OpparentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OpponentCardDisplay OpponentcardDisplay = this.GetComponent<OpponentCardDisplay>();

        if (OpponentcardDisplay.type == "unit")
        {
            this.transform.position = eventData.position;

            if (OpplaceHolder.transform.parent != OpplaceHolderParent)
            {
                OpplaceHolder.transform.SetParent(OpplaceHolderParent);
            }

            int newSiblingIndex = OpplaceHolderParent.childCount;

            for (int i = 0; i < OpplaceHolderParent.childCount; i++)
            {
                if (this.transform.position.x < OpplaceHolderParent.GetChild(i).position.x)
                {
                    newSiblingIndex = i;

                    if (OpplaceHolder.transform.GetSiblingIndex() < newSiblingIndex)
                    {
                        newSiblingIndex--;
                    }
                    break;
                }
            }
            OpplaceHolder.transform.SetSiblingIndex(newSiblingIndex);
        }
        else if (OpponentcardDisplay.type == "spell")
        {
            Debug.Log(eventData.position.y);
            if (eventData.position.y >= 750)
            {
                this.transform.position = eventData.position;

                if (OpplaceHolder.transform.parent != OpplaceHolderParent)
                {
                    OpplaceHolder.transform.SetParent(OpplaceHolderParent);
                }

                int newSiblingIndex = OpplaceHolderParent.childCount;

                for (int i = 0; i < OpplaceHolderParent.childCount; i++)
                {
                    if (this.transform.position.x < OpplaceHolderParent.GetChild(i).position.x)
                    {
                        newSiblingIndex = i;

                        if (OpplaceHolder.transform.GetSiblingIndex() < newSiblingIndex)
                        {
                            newSiblingIndex--;
                        }
                        break;
                    }
                }
                OpplaceHolder.transform.SetSiblingIndex(newSiblingIndex);
            }
            if (this.transform.parent == GameObject.Find("Board").transform && this.GetComponent<OpponentCardDisplay>().type == "spell" && this.GetComponent<OpponentCardDisplay>().spellCanDealDamage == false)
            {
                OpparentToReturnTo = OpponentGraveyard.transform;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OpponentCardDisplay OpponentcardDisplay = this.GetComponent<OpponentCardDisplay>();

        if (OpponentcardDisplay.type == "unit")
        {
            this.transform.SetParent(OpparentToReturnTo);
            this.transform.SetSiblingIndex(OpplaceHolder.transform.GetSiblingIndex());
        }
        else if (OpponentcardDisplay.type == "spell")
        {
            this.transform.SetParent(OpparentToReturnTo);
            this.transform.SetSiblingIndex(OpplaceHolder.transform.GetSiblingIndex());

            if (OpparentToReturnTo == OpponentGraveyard.transform)
            {
                TurnSystem.currentEnemyMana -= OpponentcardDisplay.cost;
                this.GetComponent<OpponentCardDisplay>().canBeDestroyed = false;
                this.GetComponent<OpponentCardDisplay>().summoned = false;
                this.GetComponent<OpponentCardDisplay>().OpponentbeInGraveyard = true;
            }
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(OpplaceHolder);


    }


    // Start is called before the first frame update
    void Start()
    {
        OpponentGraveyard = GameObject.Find("OpponentGraveyard");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
