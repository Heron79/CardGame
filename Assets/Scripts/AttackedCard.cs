using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(!GetComponent<CardMovementScr>().GameManager.IsPlayerTurn)
            return;

        CardInfoScr card = eventData.pointerDrag.GetComponent<CardInfoScr>();
        if (card && card.Selfcard.CanAttack &&
            transform.parent == GetComponent<CardMovementScr>().GameManager.EnemyField )
        {
            card.Selfcard.ChangeAttackState(false) ;
            if (card.IsPlayered)
                card.UnLightCard();


            GetComponent<CardMovementScr>().GameManager.CardsFight(card, GetComponent<CardInfoScr>());

        }
    }


}
