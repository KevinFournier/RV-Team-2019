﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneArme : Scene
    {
        public int CardsStartIndex = 0;
        public int CardsEndIndex = 4;


        [SerializeField] private bool IsIntroFinish = false;
        [SerializeField] private bool IsIntroRunning = false;

        [SerializeField] private bool AreBaronsAndMerlinFinished = false;
        [SerializeField] private bool AreBaronsAndMerlinRunning = false;

        [SerializeField] private bool AreCardsSpwaned = false;
        [SerializeField] private bool AreCardsSpawning = false;

        public bool CardTrigger = false;
        [SerializeField] private bool IsCardSelected = false;

        public bool SwordTrigger = false;
        [SerializeField] private bool IsSwordTaken = false;


        [Space(30f)]
        public Player Arthur;

        public Agent Merlin;
        public List<Agent> Barons;


        [Space(30f)]
        // Intro
        public AudioClip NarratorFirstSpeech;
        // Barons et merlin
        public AudioClip BaronSpeech;
        public AudioClip MerlinToBaronsSpeech;
        // Sélection des card
        public AudioClip MerlinCardsSpeech;
        public AudioClip ArthurCardsSpeech;
        //
        public AudioClip MerlinEndSpeech;

        [Space(30f)]
        public float NarratorSpeechDelay = 2.0f;
        public float CurtainsOpeningDelay = 2.0f;

        public float BaronsSpeechDelay = 2.0f;
        public float MerlinToBaronsDelay = 2.0f;
        // Mouvement de merlin
        public float MerlinAndCardDelay = 2.0f;
        public float MerlinCardsSpeechDelay = 2.0f;
        public float CardsSpawnDelay = 2.0f;
        // Arthur choisi une carte
        public float ArthurCardSpeechDelay = 2.0f;
        // Retirage de l'épée.
        public float MerlinEndSpeechDelay = 2.0f;

        #region Unity Methods

        private void Update()
        {
            if (!IsRunning)
                return;

            // choix carte
            // Arthur à la selection
            // retirage épée
            // Dernier speech merlin

            time += Time.deltaTime;

            var introCondition =
                !IsIntroFinish
                && itIsTime(NarratorSpeechDelay);

            var baronAndMerlinCondition =
                IsIntroFinish
                && !AreBaronsAndMerlinFinished
                && itIsTime(BaronsSpeechDelay);

            var cardsCondition =
                AreBaronsAndMerlinFinished
                && !AreCardsSpwaned
                && !AreCardsSpawning
                && itIsTime(MerlinAndCardDelay);

            var swordCondition =
                AreCardsSpwaned
                && CardTrigger
                && !IsCardSelected;

            if (introCondition)
                intro();

            else if (baronAndMerlinCondition)
                baronsAndMerlin();

            else if (cardsCondition)
                merlinAndCards();

            else if (swordCondition)
                whenCardSelected();

            else if (SwordTrigger && !IsSwordTaken)
                whenSwordTaken();
        }
        #endregion

        #region Private Methods 
        // Sequences methods
        private void intro()
        {
            if (IsIntroRunning)
                return;

            IsIntroRunning = true;
            // Le narrateur parle, attend 2s après la fin puis ouvre les ridaux.
            GameManager.Instance.NarratorClip(NarratorFirstSpeech);
            PlaySoundThen(
                GameManager.Instance.Narrator(),
                endIntro,
                CurtainsOpeningDelay);

            // Local method passed as Action
            void endIntro()
            {
                GameManager.Instance.OpenCurtains();
                resetTime();
                IsIntroFinish = true;
                IsIntroRunning = false;
                Debug.Log("Curtains open", gameObject);
            }
        }

        private void baronsAndMerlin()
        {
            if (AreBaronsAndMerlinRunning)
                return;

            AreBaronsAndMerlinRunning = true;

            Merlin.AudioSource.clip = MerlinToBaronsSpeech;

           var animDuration = Merlin.GetComponent<Animation>().clip.length;
            // Joue l'anim
            foreach (Agent agents in Barons)
                agents.Spawn(true);

            Merlin.Spawn(true);
            Merlin.Walk(true);
            //

            WaitThen(animDuration, beginDialogue);

            // Play barons's replique,
            // Wait 2s (MerlinToBaronsDelay),
            // Play Merlin's replique,
            // Reset time and booleans.
            void beginDialogue()
            {
                Merlin.Walk(false);
                
                PlaySoundThen(
                    Barons[0].AudioSource,
                    () =>
                    {
                        Merlin.Talk1(true);
                        PlaySoundThen(
                            Merlin.AudioSource,
                            endDialogue,
                            MerlinToBaronsDelay);
                    });
            }

            // Local method passed as Action
            void endDialogue()
            {
                Merlin.Talk1(false);
                resetTime();
                AreBaronsAndMerlinFinished = true;
                AreBaronsAndMerlinRunning = false;
            }
        }


        private void merlinAndCards()
        {
            AreCardsSpawning = true;

            // Merlin commence à parler x secondes après sont apparition.
            Merlin.Talk2(true);
            Merlin.AudioSource.clip = MerlinCardsSpeech;
            WaitThen(MerlinCardsSpeechDelay, merlinSpeech);


            // Local methods passed as Action
            void merlinSpeech()
            {
                PlaySoundThen(
                        Merlin.AudioSource,
                        spawnCards,
                        CardsSpawnDelay);
            }
            void spawnCards()
            {
                Merlin.Talk2(false);
                CardManager.Instance.SpawnCards(CardsStartIndex, CardsEndIndex);

                resetTime();
                AreCardsSpwaned = true;
                AreCardsSpawning = false;
            }
        }

        private void whenCardSelected()
        {
            foreach (Agent agents in Barons)
                agents.Spawn(false);

            CardTrigger = false;
            IsCardSelected = true;
            WaitThen(
                ArthurCardSpeechDelay,
                () => PlaySoundThen(Arthur.AudioSource));
        }

        private void whenSwordTaken()
        {
            Merlin.Applause(true);
            IsSwordTaken = true;
            SwordTrigger = false;
            WaitThen(MerlinEndSpeechDelay, () => endSpeech());
        }

        private void endSpeech()
        {
            Merlin.AudioSource.clip = MerlinEndSpeech;
            PlaySoundThen(
                Merlin.AudioSource,
                () => OnEnd(),
                2.0f);
        }
        #endregion


        public override void OnEnd()
        {
            Merlin.Applause(false);
            Merlin.Spawn(false);
            GameManager.Instance.CloseCurtains();
            IsFinish = true;
            IsRunning = false;
            WaitThen(2.0f, () => GameManager.Instance.NextScene());
        }

        public override void OnStart()
        {
            GameManager.Instance.NarratorClip(NarratorFirstSpeech);
            Barons[0].AudioSource.clip = BaronSpeech;
            Merlin.AudioSource.clip = MerlinToBaronsSpeech;
            Arthur.AudioSource.clip = ArthurCardsSpeech;

            IsRunning = true;
        }

    }
}
