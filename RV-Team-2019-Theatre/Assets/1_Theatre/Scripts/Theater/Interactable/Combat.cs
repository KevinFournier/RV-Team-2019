using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater {
    public class Combat : MonoBehaviour
    {
        //public Player Arthur;
        public List<Soldats> soldats;
        public static Combat Instance;
        public static int currentSoldat=0;

        public static bool combatFini = false;

        void Awake()
        {
            Instance = this;
        }
        void Update()
        {

            if (!combatFini)
            {
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
