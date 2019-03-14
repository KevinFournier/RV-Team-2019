﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Theater;

namespace Theater
{
    public class SceneCombatBarons : Scene
    {
        public Player Arthur;

        public PlayableDirector scene3director;
        public ChooseCharacter[] chooseTonPerso;
        
        
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


        public override void OnEnd()
        {
            GameManager.Instance.CloseCurtains();
            IsFinish = true;
            IsRunning = false;
            WaitThen(2.0f, () => GameManager.Instance.NextScene());
        }

        public override void OnStart()
        {
            IsRunning = true;
            foreach (ChooseCharacter item in chooseTonPerso)
            {
                item.ChooseBetweenCharacters(Arthur.Companion);

            }
            scene3director.Play();

        }
    }
}
