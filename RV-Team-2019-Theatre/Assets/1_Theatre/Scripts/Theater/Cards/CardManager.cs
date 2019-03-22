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
        public GameObject rocher;

        public List<Companion> companions;

        public static bool needCardSelection = true;

        private IEnumerator spawnCardsCoroutine;
        private IEnumerator hideCardsCoroutine;

        public AudioClip singleCduHaut;
        int numSelected = -1;

        public bool cardsSelectTime = false;
        public bool hideCards = false;

        // Start is called before the first frame update
        void Awake() => Instance = this;

        // Update is called once per frame
        void Update()
        {

            //FOR DEBUG ONLY///////
            if (cardsSelectTime)
            {
                SpawnCards(0, 4);
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
            numSelected = numCardSelected;
            switch (GameManager.Instance.GetCurrentAct().GetCurrentScene().sceneNum)
            {
                //SCENE 1 -- CHOIX DE L'ARME
                #region SCENE1
                case 1:
                    epee.GetComponent<Epee>().lame[numCardSelected].SetActive(true);
                    (GameManager.Instance.GetCurrentAct().GetCurrentScene() as SceneArme).CardTrigger = true;
                    if (numCardSelected == 2)
                    {
                        GameManager.StarWars = true;
                    }
                    GetComponent<AudioSource>().clip = singleCduHaut;
                    GetComponent<AudioSource>().Play();

                    rocher.SetActive(true);


                    Invoke("setActiveColliders", rocher.GetComponent<Animation>().clip.length + 0.5f);

                    break;
                #endregion
                //SCENE 2 -- CHOIX DU COMPAGNON
                #region SCENE2
                case 2:
                    GetComponent<AudioSource>().clip = singleCduHaut;
                    GetComponent<AudioSource>().Play();
                    switch (numCardSelected)
                    {
                        case 4:
                            //Call function in game manager that match the card selected
                            (GameManager.Instance.GetCurrentAct().GetCurrentScene() as SceneChoixCompagnon).SetCompanion(CompanionType.Guenievre);
                            companions[0].gameObject.SetActive(true);

                            break;
                        case 5:
                            //Call function in game manager that match the card selected
                            (GameManager.Instance.GetCurrentAct().GetCurrentScene() as SceneChoixCompagnon).SetCompanion(CompanionType.Brush);
                            companions[1].gameObject.SetActive(true);
                            break;
                        case 6:
                            //Call function in game manager that match the card selected
                            (GameManager.Instance.GetCurrentAct().GetCurrentScene() as SceneChoixCompagnon).SetCompanion(CompanionType.R2D2);
                            companions[2].gameObject.SetActive(true);
                            break;
                        case 7:
                            //Call function in game manager that match the card selected
                            (GameManager.Instance.GetCurrentAct().GetCurrentScene() as SceneChoixCompagnon).SetCompanion(CompanionType.Jesus);
                            companions[3].gameObject.SetActive(true);
                            break;
                        case 8:
                            //Call function in game manager that match the card selected
                            (GameManager.Instance.GetCurrentAct().GetCurrentScene() as SceneChoixCompagnon).SetCompanion(CompanionType.Merlin);

                            break;
                    }
                    break;
                    #endregion
            }
        }

        private void setActiveColliders()
        {
            epee.GetComponent<BoxCollider>().enabled = true;
            epee.GetComponent<Epee>().lame[numSelected].GetComponent<MeshCollider>().enabled = true;
            numSelected = -1;
        }

        public void SpawnCards(int indexStart, int indexEnd)
        {
            StartCoroutine(SpawnCardsCoroutine(indexStart, indexEnd));
        }
        public void HideCards()
        {
            int cardSelected = -1;

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].IsSelected == true)
                {
                    cardSelected = i;
                }
            }

            hideCardsCoroutine = HideCardsCoroutine(cardSelected);
            StartCoroutine(hideCardsCoroutine);
            //Reset activated cards
            for (int i = 0; i < cards.Length; i++)
                cards[i].IsSelected = false;
        }


        public IEnumerator SpawnCardsCoroutine(int indexStart, int indexEnd)
        {
            Debug.Log("In Couroutine");
            if (GameManager.Instance.GetCurrentAct().GetCurrentScene().sceneNum != 2)
            {
                for (int i = indexStart; i < indexEnd; i++)
                {
                    cards[i].Spawn();
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                    cards[i].smokeEffect.Play();
                    yield return new WaitForSeconds(0.3f);
                    cards[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
                    yield return new WaitForSeconds(0.3f);
                }
            }
            else
            {
                for (int i = indexStart; i < indexEnd; i++)
                {
                    if (GameManager.StarWars && i!=7)
                    {
                        cards[i].Spawn();
                        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);

                        cards[i].smokeEffect.Play();
                        yield return new WaitForSeconds(0.3f);
                        cards[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
                        yield return new WaitForSeconds(0.3f);
                    }
                    if (!GameManager.StarWars && i != 6)
                    {
                        cards[i].Spawn();
                        GetComponent<AudioSource>().Play();

                        cards[i].smokeEffect.Play();
                        yield return new WaitForSeconds(0.3f);
                        cards[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
                        yield return new WaitForSeconds(0.3f);
                    }
                }
            }
            needCardSelection = true;
        }

        private IEnumerator waitThen(float time, System.Action func)
        {
            yield return new WaitForSeconds(time);

            if (func != null)
                func.Invoke();
        }

        public IEnumerator HideCardsCoroutine(int nbCardSelected)
        {
            needCardSelection = false;
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);

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

            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            cards[nbCardSelected].GetComponent<MeshRenderer>().enabled = false;
            cards[nbCardSelected].smokeEffect.Play();
            cards[nbCardSelected].aura.Stop();
            yield return new WaitForSeconds(1.5f);
            cards[nbCardSelected].Hide();

        }
    }
}
