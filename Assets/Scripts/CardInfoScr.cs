using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScr : MonoBehaviour {

	public Card Selfcard;

	public Text nameText;
	public Text descriptionText;

	public Image Logo;

	public Text manaText;
	public Text attackText;
	public Text defenseText;

	public GameObject HideObj,HighlightedObj;
	public bool IsPlayered;

	public void ShowCardInfo (Card card, bool isPlayered) {
		
		IsPlayered = isPlayered;
		Selfcard = card;
		HideObj.SetActive(false);
		HighlightedObj.SetActive(false);
		nameText.text = card.Name;
		descriptionText.text = card.Description;

		Logo.sprite = card.Logo;
		Logo.preserveAspect = true;	

		manaText.text = Selfcard.manaCost.ToString();
		RefreshDate();
	

		
	}
	public void HideCardInfo(Card card)
	{
		Selfcard = card;
		HideObj.SetActive(true);
		IsPlayered = false;
		manaText.text = "";
	}

	public void HighLightCard()
    {
		HighlightedObj.SetActive(true);
    }
	public void UnLightCard()
    {
	
		HighlightedObj.SetActive(false);
	}


	public void RefreshDate()
    {
		attackText.text = Selfcard.Attack.ToString();
		defenseText.text = Selfcard.Defense.ToString();
		manaText.text = Selfcard.manaCost.ToString();

	}

}
