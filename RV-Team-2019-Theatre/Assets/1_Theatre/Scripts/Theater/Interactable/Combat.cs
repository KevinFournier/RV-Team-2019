using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Combat : MonoBehaviour
    {
        public Transform Arthur;
        public List<Soldats> soldats;
        public static Combat Instance;
        private int currentSoldat = 0;

        public static int currentDeath = 0;

        public float soldierSpeed = 0.005f;
        public static bool combatCommence = false;

        public static bool combatFini = false;



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
            
            if (!combatFini && combatCommence)
            {
                if (currentSoldat < soldats.Capacity)
                {
                    target = new Vector3(Arthur.transform.position.x, soldats[currentSoldat].transform.position.y, Arthur.transform.position.z);
                    soldats[currentSoldat].transform.position = Vector3.Lerp(soldats[currentSoldat].transform.position, target, soldierSpeed);

                    if (soldats[currentSoldat].dead == true)
                    {
                        currentSoldat++;
                        currentDeath++;
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

