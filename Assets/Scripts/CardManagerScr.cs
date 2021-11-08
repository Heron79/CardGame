using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Defense;

    public Card(string name, string logoPath, int attack, int defense)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defense = defense;

    }
}

public class Ca
public class CardManagerScr : MonoBehaviour
{

}
