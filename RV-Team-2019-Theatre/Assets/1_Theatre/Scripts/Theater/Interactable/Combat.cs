using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Combat : MonoBehaviour
    {
        public AudioClip musicCombatSW;
        public AudioClip musicCombat;

        public Player player;

        public Transform Arthur;
        public List<Soldats> soldats;
        public List<Soldats> stormTrooper;
        public static Combat Instance;
        private int currentSoldat = 0;

        public static int currentDeath = 0;

        public float soldierSpeed = 0.005f;
        public static bool combatCommence = false;

        public static bool combatFini = false;

        public bool once=false;



        private Vector3 target;

    
        void Awake()
        {
            Instance = this;
        }
        void Start()
        {

        }
        void Update()
        {
            Debug.Log("Combat commence value : " + combatCommence);
            if (GameManager.Instance.GetCurrentAct().GetCurrentScene().sceneNum == 3)
            {
                if (!combatFini && combatCommence)
                {
                    if (GetComponent<AudioSource>() != null)
                    {
                        if (once == false)
                        {
                            if (GameManager.StarWars == true)
                            {
                                GetComponent<AudioSource>().PlayOneShot(musicCombatSW);
                            }
                            else
                            {
                                GetComponent<AudioSource>().PlayOneShot(musicCombat);
                            }
                            once = true;
                        }
                    }

                    var foes = this.soldats;
                    if (GameManager.StarWars && player.Companion.Type == CompanionType.R2D2)
                        foes = stormTrooper;
                    if (currentSoldat < foes.Capacity)
                    {
                        target = new Vector3(Arthur.transform.position.x, foes[currentSoldat].transform.position.y, Arthur.transform.position.z);
                        if (player.Companion.Type==CompanionType.Jesus || player.Companion.Type == CompanionType.R2D2)
                        {
                            target = new Vector3(player.transform.position.x, foes[currentSoldat].transform.position.y, player.transform.position.z);
                            if (player.Companion.Type == CompanionType.Jesus)
                                player.Companion.gameObject.GetComponent<Animator>().SetBool("isDead", true);
                            /*else
                            {
                                player.Companion.gameObject.transform.Find("Explosion").GetComponent<ParticleSystem>().Play();
                                player.Companion.gameObject.GetComponent<MeshRenderer>().enabled = false;
                            }*/
                                
                        }
                        foes[currentSoldat].transform.position = Vector3.Lerp(foes[currentSoldat].transform.position, target, soldierSpeed);

                        if (foes[currentSoldat].dead == true)
                        {
                            foes[currentSoldat].gameObject.GetComponent<AudioSource>().Play();

                            currentSoldat++;
                            currentDeath++;
                        }
                    }

                    if (combatFini)
                    {
                        if (GetComponent<AudioSource>() != null)
                        {
                            GetComponent<AudioSource>().Stop();
                        }
                    }


                    if (currentDeath >= 8)
                    {
                        combatFini = true;
                        Debug.Log("combatFini");
                    }
                }
            }
            
        }
    }
}

