using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Combat : MonoBehaviour
    {
        public Player Arthur;
        public List<Soldats> soldats;
        public static Combat Instance;
        public static int currentSoldat = 0;

        public float soldierSpeed = 0.005f;

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
            
            if (!combatFini)
            {
                target = new Vector3(Arthur.transform.position.x, soldats[currentSoldat].transform.position.y, Arthur.transform.position.z);
                soldats[currentSoldat].transform.position = Vector3.Lerp(soldats[currentSoldat].transform.position, target, soldierSpeed);

                if (soldats[currentSoldat].dead == true)
                {
                    currentSoldat++;
                }


                if (currentSoldat >= soldats.Capacity)
                {
                    combatFini = true;
                    Debug.Log("combatFini");
                }
            }
        }



    }
}

