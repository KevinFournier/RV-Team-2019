using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Card[] cards;

    private static bool needCardSelection = false;

    //This variable must be in GameManager class and be called in EventManager
    public int activeScene = 0;

    //This variable must be in GameManager class and be called in EventManager
    bool starWars = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Trigger spawn cards at the start of a scene
        if (Input.GetKeyDown(KeyCode.Space) && needCardSelection==false)
        {
            Debug.Log("Time to select a card");
            SpawnCards();
            needCardSelection = true;
        }

        if (needCardSelection)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if(cards[i].IsSelected == true)
                {
                    EventManager(i);
                    needCardSelection = false;
                }
            }
        }
    }

    public void EventManager(int numCardSelected)
    {
        switch (activeScene)
        {
            //SCENE 1 -- CHOIX DE L'ARME
            #region SCENE1
            case 0:
                switch (numCardSelected)
                {
                    case 0:
                        //Call function in game manager that match the card selected
                        Debug.Log("Choix " + numCardSelected + " : Excalibur");
                        break;
                    case 1:
                        //Call function in game manager that match the card selected
                        Debug.Log("Choix " + numCardSelected + " : Sabre Laser");
                        break;
                    case 2:
                        //Call function in game manager that match the card selected
                        Debug.Log("Choix " + numCardSelected + " : Baguette Magique");
                        break;
                    case 3:
                        //Call function in game manager that match the card selected
                        Debug.Log("Choix " + numCardSelected + " : Brosse à Chiotte");
                        break;
                }
                break;
            #endregion
            //SCENE 2 -- CHOIX DU COMPAGNON
            #region SCENE2
            case 1:
                switch (numCardSelected)
                {
                    case 0:
                        //Call function in game manager that match the card selected
                        Debug.Log("Choix " + numCardSelected );
                        break;
                    case 1:
                        //Call function in game manager that match the card selected
                        Debug.Log("Choix " + numCardSelected);
                        break;
                    case 2:
                        //Call function in game manager that match the card selected
                        Debug.Log("Choix " + numCardSelected);
                        break;
                    case 3:
                        //Call function in game manager that match the card selected
                        Debug.Log("Choix " + numCardSelected);
                        break;
                }
                break;
                #endregion
        }

    }

    public void SpawnCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].Spawn();
        }
    }

    public void SetCards(Card[] cardsArray)
    {
        cards = cardsArray;
    }
}
