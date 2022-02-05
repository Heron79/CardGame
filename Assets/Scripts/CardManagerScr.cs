using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Card
{
    public string Name;
    public string Description;
    public Sprite Logo;

    public int manaCost;
    public int Attack;
    public int Defense;
    public bool CanAttack;


    public bool IsAlive
    {
        get { return Defense > 0; }
    }
    public Card (string name,string description, string logoPath, int attack, int defense, int manacost)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defense = defense;
        manaCost = manacost;
        Description = description;
        CanAttack = false;

    }
    public void ChangeAttackState(bool can)
    {
        CanAttack = can;    
    }

    public void GetDamage(int dmg)
    {
        Defense -= dmg;
    }
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}
public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        //Card(string name, string description, string logoPath, int attack, int defense, int mana)
        CardManager.AllCards.Add(new Card("Andrew","good gue", "Sprites/Artwork/Edwin", 3,5,3));
        CardManager.AllCards.Add(new Card("Ruslana", "good gue", "Sprites/Artwork/Tirion", 1, 5, 2));
        CardManager.AllCards.Add(new Card("Zuza", "good gue", "Sprites/Artwork/Edwin", 2, 3, 1));
        CardManager.AllCards.Add(new Card("Zuza", "good gue", "Sprites/Artwork/Edwin", 2, 3, 1));
        CardManager.AllCards.Add(new Card("Zuza", "good gue", "Sprites/Artwork/Edwin", 2, 3, 1));
        CardManager.AllCards.Add(new Card("Zuza", "good gue", "Sprites/Artwork/Edwin", 2, 3, 1));

    }

}
