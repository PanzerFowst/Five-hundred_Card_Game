using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    

    //-----------------------------------------------------------------------------------------------------------------
    // Attributes:
    //-----------------------------------------------------------------------------------------------------------------
    

    // In the turtorial, he filled out lots of buttons here...
    public Button startButton;

    // Access to all Players:
    public PlayerScript playerUp;
    public PlayerScript playerDn;
    public PlayerScript playerRt;
    public PlayerScript playerLf;
    
    
    //-----------------------------------------------------------------------------------------------------------------
    // Methods:
    //-----------------------------------------------------------------------------------------------------------------


    // Start is called before the first frame update
    void Start() {
        
        // Add on-click listeners to the buttons:
        startButton.onClick.AddListener(() => StartClicked());

    }

    // Update is called once per frame
    void Update() {
        
    }


    private void StartClicked() {

        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        GameObject.Find("Deck").GetComponent<DeckScript>().DealTheCards(playerUp, playerDn, playerRt, playerLf);
        playerUp.SortHandUsingStandard();
        playerUp.DisplayHand();
        playerDn.DisplayHand();
        playerRt.DisplayHand();
        playerLf.DisplayHand();
    }

}