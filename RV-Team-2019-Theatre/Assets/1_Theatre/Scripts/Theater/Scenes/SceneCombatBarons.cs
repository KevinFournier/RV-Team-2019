using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneCombatBarons : Scene
    {
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            // Début Scene
            // Rideau s'ouvre

            // Discour Baron
            // Phrase A
            // P B
            // P A

            // selon personage choisi :
                // G : G A G
                // M : M A M
                // J : J A
                // C : A
                // R : R A
            
            // Combat
                // Deux Phrases des B
            
            // Fin:
                // Fin 1 : A B
                // Fin 2 : A B

            // Fermeture Rideau
            // Applaudissements
            // Fin Scene

        }


        public override void OnEnd() => throw new System.NotImplementedException();
        public override void OnStart() => throw new System.NotImplementedException();
    }
}
