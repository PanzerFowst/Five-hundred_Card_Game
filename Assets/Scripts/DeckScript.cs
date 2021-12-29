using UnityEngine;


///
/// Script used for the card deck to keep track of all the cards in play.
///
public class DeckScript : MonoBehaviour {


    //-----------------------------------------------------------------------------------------------------------------
    // Attributes:
    //-----------------------------------------------------------------------------------------------------------------
    

    /// Array of sprites that is the deck.  Should be a standard 43-card Five-hundred deck.
    public Sprite[] cardSprites;

    /// Array of card places.
    private int[] cardPlaces;

    /// Place in the deck that is being dealt from.
    private int deckIndex = 0;  // TODO probably won't need this...

    
    
    //-----------------------------------------------------------------------------------------------------------------
    // Methods:
    //-----------------------------------------------------------------------------------------------------------------


    /// Start is called before the first frame update
    void Start()
    {
        FillCardPlaces();
    }

    /// Fills the cardPlaces array with integers from 0 to the deck length.
    public void FillCardPlaces() {

        // Gather enough space:
        cardPlaces = new int[cardSprites.Length];

        // Fill the array with integers from 0 to the length of the deck:
        for (int i = 0; i < cardSprites.Length; i++)
            cardPlaces[i] = i; 
    }

    /// Method to shuffle the deck of cards.
    public void Shuffle() {

        // Shuffle technique from "https://youtu.be/Nf0kUpdWeJs?list=PLbsvRhEyGkKfl4AY3uUkocO02Vg_mhI0-&t=152":
        for (int i = cardPlaces.Length - 1; i > 0; i--) {

            int j = Mathf.FloorToInt(UnityEngine.Random.Range(0.0f, 1.0f) * cardPlaces.Length - 1) + 1;
            int temporayPlace = cardPlaces[i];
            cardPlaces[i] = cardPlaces[j];
            cardPlaces[j] = temporayPlace;

            /*
            Do this instead of switching around the Sprite[].

            Probably shouldn't mess with the Sprite[] and instead do an assignment array so that the cards
            actually are ordered?  Like an array of numbers 1-43 for the cards in the deck, swap those around,
            then go through and assign those to the players so that the cards are still in order...
            */

        }

    }

    /// Deals all four players ten cards each.
    public void DealTheCards(
        PlayerScript playerUp, PlayerScript playerDn,
        PlayerScript playerRt, PlayerScript playerLf) {

        // Reset deck position:
        deckIndex = 0;

        // Deal the cards:
        DealPlayerCards(playerUp);
        DealPlayerCards(playerDn);
        DealPlayerCards(playerRt);
        DealPlayerCards(playerLf);

        // TODO Deal the widow too...

    }

    /// Deals ten cards to a player.
    public void DealPlayerCards(
        PlayerScript playerScript) {

        // Deal ten cards from the deck:
        for (int i = 0; i < 10; i++)
            playerScript.cards[i] = cardPlaces[deckIndex++];

    }

    /// Shows a player's cards by assigning the sprites to the hand.
    public void SetCardSprites(PlayerScript playerScript) {

        // Show all cards from the player:
        for (int i = 0; i < playerScript.hand.Length; i++)
            playerScript.hand[i].GetComponent<CardScript>().SetSprite(cardSprites[playerScript.cards[i]]);

    }

    /// Deals a single card and advances the deckIndex.
    public void DealCard(CardScript cardScript) {

        cardScript.SetSprite(cardSprites[deckIndex++]);  // Deal a card and move up a spot in the deck.

    }

    /// Returns the sprite for the backside of a card.
    public Sprite GetCardBack() {
        return cardSprites[0];  // TODO: This'll return the Joker instead of a back of card; Get a separate attribute.
    }

}
