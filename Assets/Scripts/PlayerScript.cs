using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
        
    //-----------------------------------------------------------------------------------------------------------------
    // Attributes:
    //-----------------------------------------------------------------------------------------------------------------
    

    // Access to card and deck scripts:
    public CardScript cardScript;
    public DeckScript deckScript;

    /// Player hand:
    public GameObject[] hand;

    /// Player's cards:
    public int[] cards = new int[13];

    // Useless stuff:
    public int handValue = 0;
    private int money;
    public int cardIndex = 0;
    List<CardScript> aceList = new List<CardScript>();

    
    
    //-----------------------------------------------------------------------------------------------------------------
    // Methods:
    //-----------------------------------------------------------------------------------------------------------------


    /// Shows a player their hand.
    public void DisplayHand() {
        
        /*foreach (GameObject gameObject in hand) {

            gameObject.GetComponent<Renderer>().enabled = true;
        }*/

        deckScript.SetCardSprites(this);
        
    }

    public void SortHandUsingStandard() {
        
        // Copy the and sort the hand:
        List<GameObject> tempHand = hand.ToList();

        HandSorter handSorter = new HandSorter { sortingOrder = HandSorter.SortBy.Standard };
        //tempHand.Sort(handSorter);

        // Reassigning the hand to the sorted hand:
        hand = tempHand.ToArray();
        
    }

    /* /// Add a hand to the player/dealer's hand
    public int GetCard()
    {
        // Get a card, use deal card to assign sprite and value to card on table
        deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // Show card on game screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // Add card value to running total of the hand
        
        cardIndex++;
        return handValue;
    } */

    /// Hides all cards, resets the needed variables
    public void ResetHand()
    {
        foreach (var card in hand)
        {
            card.GetComponent<CardScript>().ResetCard();
            card.GetComponent<Renderer>().enabled = false;
        }
        //cardIndex = 0;
        //handValue = 0;
        //aceList = new List<CardScript>();
    }

}




public class HandSorter : IComparer<CardScript> {
    
    /// Options to sort by.  TODO: Will have to work on more of this sorting by black, red, black, red later...
    public enum SortBy  
    {
        Standard,
        NoTrump,
        HeartsTrump,
        DiamondsTrump,
        ClubsTrump,
        SpadesTrump
    }
    
    /// Sets initial sort order.
    public SortBy sortingOrder = SortBy.Standard;

    /// Compares by Suit, then value.
    public int Compare(CardScript x, CardScript y) {

        // Make sure non-null objects:
        if (x == null || y == null)
            throw new ArgumentNullException();
        
        // Comparison logic:
        switch (sortingOrder)
        {

            /*case SortBy.HeartsTrump:
                
                break;
            case SortBy.DiamondsTrump:
                
                break;
            case SortBy.ClubsTrump:
                
                break;
            case SortBy.SpadesTrump:
                
                break;*/

            default:  // (For NoTrump, Standard, and Nullo)
                
                if (x.cardSuit != y.cardSuit) {

                    // Return the higher Suit.
                    return x.cardSuit.CompareTo(y.cardSuit);

                } else {

                    // Return the comparison of the card values since they are the same suit:
                    return x.cardValue.CompareTo(y.cardValue);

                }

        }

    }

}