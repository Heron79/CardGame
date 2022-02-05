using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Game
{
    public List<Card> EnemyDeck, PlayerDeck,
                      EnemyHand, PlayerHand,
                      EnemyField, PlayerField;
    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();



    }

    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        for(int i = 0;i<10;i++)
            list.Add(CardManager.AllCards[Random.Range(0,CardManager.AllCards.Count)]);
        return list;

    }

}

public class GameManagerScript : MonoBehaviour
{
    public Game currentGame;
    public Transform EnemyHand, PlayerHand, PlayerField, EnemyField;
    public GameObject Card;
    int Turn, TurnTime = 30;
    public TextMeshProUGUI TurnTimeTxt;
    public Button EndTurnButton;

    public int PlayerMana = 10, EnemyMana = 10;
    public TextMeshProUGUI PlayerManaText,EnemyManaText;

    public List<CardInfoScr> PlayerHandCards = new List<CardInfoScr>(),
                             PlayerFieldCards = new List<CardInfoScr>(),
                             EnemyHandCards = new List<CardInfoScr>(),
                             EnemyFieldCards = new List<CardInfoScr>();

    public bool IsPlayerTurn
    {
        get { return Turn % 2 == 0;}
    }

    void Start()
    {
        Turn = 0;


        currentGame = new Game();
       
       GiveHandCard(currentGame.EnemyDeck,EnemyHand);
       GiveHandCard(currentGame.PlayerDeck, PlayerHand);
       ShowMana();
       StartCoroutine(TurnFunc());

    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text =  TurnTime.ToString();

        if (IsPlayerTurn)
        {
            foreach (var card in PlayerFieldCards)
            {
                card.Selfcard.ChangeAttackState(true);
                card.HighLightCard();
            }

            while(TurnTime--> 0)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }

        }
        else
        {
            foreach (var card in EnemyFieldCards)
                card.Selfcard.ChangeAttackState(true);

            while (TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }

            if (EnemyHandCards.Count > 0)
                EnemyTurn(EnemyHandCards);

           
        }

        ChangeTurn();
    }

    void EnemyTurn(List<CardInfoScr> cards)
    {
       int count = cards.Count == 1?1:
            Random.Range(0, cards.Count);

        for(int i = 0; i < count; i++)
        {
            if (EnemyFieldCards.Count > 5)
                return;
            cards[0].ShowCardInfo(cards[0].Selfcard,false);
            cards[0].transform.SetParent(EnemyField);

            EnemyFieldCards.Add(cards[0]);
            EnemyHandCards.Remove(cards[0]);
        }

        foreach(var activeCard in EnemyFieldCards.FindAll( x => x.Selfcard.CanAttack))
        {
            if (PlayerFieldCards.Count == 0)
                return;
            var enemy = PlayerFieldCards[Random.Range(0, PlayerFieldCards.Count)];
            Debug.Log(activeCard.Selfcard.Name + "(" + activeCard.Selfcard.Attack + ";"+ activeCard.Selfcard.Defense +") --> " + 
                enemy.Selfcard.Name + "("+ enemy.Selfcard.Attack + ";"+enemy.Selfcard.Defense + ")");
            activeCard.Selfcard.ChangeAttackState(false);
            CardsFight(enemy,activeCard);
        }

    }

    void GiveHandCard(List<Card> deck,Transform hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;
        Card card = deck[0];
        GameObject cardGo = Instantiate(Card, hand, false);

        if (hand == EnemyHand)
        {
            cardGo.GetComponent<CardInfoScr>().HideCardInfo(card);
            EnemyHandCards.Add(cardGo.GetComponent<CardInfoScr>()); 
        }
        else
        {
            cardGo.GetComponent<CardInfoScr>().ShowCardInfo(card,true);
            PlayerHandCards.Add(cardGo.GetComponent<CardInfoScr>());
            cardGo.GetComponent<AttackedCard>().enabled = false;

        }
        deck.RemoveAt(0);
        
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;
        EndTurnButton.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
        {
            GiveNewCards();
            PlayerMana = EnemyMana = 10;
            ShowMana();
        }
        StartCoroutine(TurnFunc());
    }

     void GiveNewCards()
    {
        GiveCardToHand(currentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(currentGame.PlayerDeck, PlayerHand);

    }

    public void CardsFight(CardInfoScr playerCard, CardInfoScr enemyCard)
    {
        playerCard.Selfcard.GetDamage(enemyCard.Selfcard.Attack);
        enemyCard.Selfcard.GetDamage(playerCard.Selfcard.Attack);

        if(!playerCard.Selfcard.IsAlive)
            DestroyCard(playerCard);
        else
            playerCard.RefreshDate();

        if (!enemyCard.Selfcard.IsAlive)
            DestroyCard(enemyCard);
        else
            enemyCard.RefreshDate();

    }

    void DestroyCard(CardInfoScr card)
    {
        card.GetComponent<CardMovementScr>().OnEndDrag(null);
        if(EnemyFieldCards.Exists(x => x == card))
            EnemyFieldCards.Remove(card);
        if(PlayerFieldCards.Exists(x => x == card))
            PlayerFieldCards.Remove(card);

        Destroy(card.gameObject);
    }

   void ShowMana()
    {
        PlayerManaText.text = PlayerMana.ToString();
        EnemyManaText.text = EnemyMana.ToString();

    }
   
    public void ReduceMana(bool playerMana,int manacost)
    {
        if (playerMana)
        {
            PlayerMana = Mathf.Clamp(PlayerMana - manacost, 0, int.MaxValue);

        }
        else
        {
            EnemyMana = Mathf.Clamp(EnemyMana - manacost, 0, int.MaxValue);

        }
        ShowMana();
    }


}