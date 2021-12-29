using System;
using UnityEngine;


///
/// Suit enum type for the four types of suits.
///
public enum Suit {
    Hearts      = 100,
    Diamonds    = 80,
    Clubs       = 60,
    Spades      = 40
}


///
/// Script for cards to keep track fo their data such as suit, value, and whether trump.
///
public class CardScript : MonoBehaviour {

    //-----------------------------------------------------------------------------------------------------------------
    // Attributes:
    //-----------------------------------------------------------------------------------------------------------------
    

    /// Number value of the card used to compare it to values of the same suit:
    public int cardValue;

    /// Suit of the card:
    public Suit cardSuit;

    /// Whether or not the card is trump:
    public bool isCardTrump;   // TODO: This might not be the right place for it?...  Maybe have a isTrump() method?

    
    
    //-----------------------------------------------------------------------------------------------------------------
    // Getter and Setter Methods:
    //-----------------------------------------------------------------------------------------------------------------


    /// Simple setter method for the card's integer value.
    public void SetValue(int newValue) {
        cardValue = newValue;
    }

    /// Simple getter method for the card's integer value.
    public int GetValue() {
        return cardValue;
    }

    /// Simple setter method to set the card script to a new sprite.
    public void SetSprite(Sprite sprite) {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        // TODO: Eventually get rid of this:
        SetCardSuitAndValue(GetSpriteName());
    }

    /// Simple getter method to retrieve the sprite name of the card.
    public string GetSpriteName() {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    /// Sets the card value as an integer value and assigns the card a suit based upon the sprite name.
    public void SetCardSuitAndValue(string spriteName) {

        // Gets a substring of the sprite's name and chops off the first four characters ("card") then cuts off the
        //  last character (5 because it goes the length of the string minus the first four characters and the last
        //  character or at least, it should):
        string suitString = spriteName.Substring(4, spriteName.Length-5);
        
        // Check the special case of a "cardSuitName10" which would become "SuitName1":
        if (suitString.Contains("1")) suitString = suitString.Trim('1');


        // Parse the string and change this card's suit to the remaining string (which should be hearts, etc):
        Enum.TryParse(suitString, out cardSuit);
        // NB: If it is a Joker, the suitString will be "Joke" which doesn't match any enums and thus is
        //  skipped.


        // Set the value of the card by checking the last character of a string and assigning that:
        string valueString = spriteName.Substring(spriteName.Length-1, 1);

        // Check for Aces and face cards:
        switch (valueString.ToUpper()) {

            case "J":
                SetValue(11);
                break;
            case "Q":
                SetValue(12);
                break;
            case "K":
                SetValue(13);
                break;
            case "A":
                SetValue(14);
                break;
            case "R":
                SetValue(-1);
                break;
            
            // Otherwise, the value is the number:
            default:

                // Assign the value as the integer:
                Int32.TryParse(valueString, out cardValue);
                
                // (unless "10" which will show up as "0"):
                if (cardValue == 0) SetValue(10);


                break;
        }
         
    }

    /// Sets the cards value, suit, and sprite with an input of the assigned sprite.
    public void SetCard(Sprite sprite) {
        SetSprite(sprite);
        SetCardSuitAndValue(GetSpriteName());
    }



    //-----------------------------------------------------------------------------------------------------------------
    // Other Methods:
    //-----------------------------------------------------------------------------------------------------------------


    /// Not sure what this one is for.  TODO: will have to fix it.  Perhaps even reset all values.
    public void ResetCard() {
        
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        cardValue = 0;
    }

}
