using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class CardManager : MonoBehaviour
    {
        public static CardManager Instance;

        [SerializeField] private Card[] cards;

        public GameObject epee;

        public static bool needCardSelection = true;

        private IEnumerator spawnCardsCoroutine;
        private IEnumerator hideCardsCoroutine;

        public bool cardsSelectTime = false;
        public bool hideCards = false;

        //This variable must be in GameManager class and be called in EventManager
        public int activeScene = 0;

        //This variable must be in GameManager class and be called in EventManager
        bool starWars = false;

        // Start is called before the first frame update
        void Awake() => Instance = this;

        // Update is called once per frame
        void Update()
        {
            //FOR DEBUG ONLY///////
            if (cardsSelectTime)
            {
                SpawnCards(0,4);
                cardsSelectTime = false;
            }
            if (hideCards)
            {
                HideCards();
                hideCards = false;
            }
            ////////////////////////

            if (needCardSelection)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    if (cards[i].IsSelected && cards[i].enabled)
                    {
                        EventManager(i);
                        HideCards();
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
                    epee.GetComponent<Epee>().lame[numCardSelected].SetActive(true);
                    break;
                #endregion
                //SCENE 2 -- CHOIX DU COMPAGNON
                #region SCENE2
                case 1:
                    switch (numCardSelected)
                    {
                        case 0:
                            //Call function in game manager that match the card selected
                            Debug.Log("Choix " + numCardSelected);
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

        public void SpawnCards(int indexStart, int indexEnd)
        {
            spawnCardsCoroutine = SpawnCardsCoroutine(indexStart, indexEnd);
            StartCoroutine(spawnCardsCoroutine);
        }
        public void HideCards()
        {
            int cardSelected = 2;

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].IsSelected == true)
                {
                    cardSelected = i;
                }
            }

            hideCardsCoroutine = HideCardsCoroutine(cardSelected);
            StartCoroutine(hideCardsCoroutine);
        }


        public IEnumerator SpawnCardsCoroutine(int indexStart, int indexEnd)
        {
            for (int i = indexStart; i < indexEnd; i++)
            {
                cards[i].Spawn();
                cards[i].smokeEffect.Play();
                yield return new WaitForSeconds(0.3f);
                cards[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
                yield return new WaitForSeconds(0.3f);

            }
            needCardSelection = true;
        }

        public IEnumerator HideCardsCoroutine(int nbCardSelected)
        {
            needCardSelection = false;

            for (int i = 0; i < cards.Length; i++)
            {
                if (i != nbCardSelected)
                {
                    cards[i].GetComponent<MeshRenderer>().enabled = false;
                    cards[i].smokeEffect.Play();
                    cards[i].aura.Stop();
                }
            }
            yield return new WaitForSeconds(1.5f);
            for (int i = 0; i < cards.Length; i++)
            {
                if (i != nbCardSelected)
                {
                    cards[i].Hide();
                }
            }

            cards[nbCardSelected].GetComponent<MeshRenderer>().enabled = false;
            cards[nbCardSelected].smokeEffect.Play();
            cards[nbCardSelected].aura.Stop();
            yield return new WaitForSeconds(1.5f);
            cards[nbCardSelected].Hide();
   
        }
    }
}
