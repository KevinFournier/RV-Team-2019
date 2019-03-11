using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneArme : Scene
    {
        public int CardsStartIndex = 0;
        public int CardsEndIndex = 4;


        public bool IsIntroFinish = false;
        public bool IsIntroRunning = false;

        public bool AreBaronsAndMerlinFinished = false;
        public bool AreBaronsAndMerlinRunning = false;

        public bool AreCardsSpwaned = false;
        public bool AreCardsSpawning = false;

        public bool CardTrigger = false;
        public bool IsCardSelected = false;

        public bool SwordTrigger = false;
        public bool IsSwordTaken = false;


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


        private float time = 0.0f;

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
        // Time management methods
        private void resetTime() => time = 0.0f;
        private bool itIsTime(float delay) => time >= delay;

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
                () =>
                {
                    GameManager.Instance.OpenCurtains();
                    resetTime();
                    IsIntroFinish = true;
                    IsIntroRunning = false;
                    Debug.Log("Curtains open", gameObject);
                },
                CurtainsOpeningDelay);
        }

        private void baronsAndMerlin()
        {
            if (AreBaronsAndMerlinRunning)
                return;

            AreBaronsAndMerlinRunning = true;

            Merlin.AudioSource.clip = MerlinToBaronsSpeech;

            // Play barons's replique,
            // Wait 2s (MerlinToBaronsDelay),
            // Play Merlin's replique,
            // Reset time and booleans.
            PlaySoundThen(
                Barons[0].AudioSource,
                () => PlaySoundThen(
                    Merlin.AudioSource,
                    () =>
                    {
                        resetTime();
                        AreBaronsAndMerlinFinished = true;
                        AreBaronsAndMerlinRunning = false;
                    }),
                MerlinToBaronsDelay);
        }

        private void merlinAndCards()
        {
            if (AreCardsSpawning)
                return;

            AreCardsSpawning = true;

            // Merlin commence à parler x secondes après sont apparition.
            Merlin.AudioSource.clip = MerlinCardsSpeech;
            WaitThen(
                MerlinCardsSpeechDelay,
                () =>
                {
                    PlaySoundThen(
                        Merlin.AudioSource,
                        () =>
                        {
                            Debug.LogWarning("Spawn cards gogo");
                            CardManager.Instance.SpawnCards(CardsStartIndex, CardsEndIndex);

                            resetTime();
                            AreCardsSpwaned = true;
                            AreCardsSpawning = false;
                        },
                        CardsSpawnDelay);
                
                });
        }

        private void whenCardSelected()
        {
            CardTrigger = false;
            IsCardSelected = true;
            dropSword();
            WaitThen(
                ArthurCardSpeechDelay,
                () => PlaySoundThen(Arthur.AudioSource));
        }

        private void dropSword()
        {
            // TODO: Déclancher l'apparition de l'épée
        }

        private void whenSwordTaken()
        {
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
            GameManager.Instance.CloseCurtains();
            IsFinish = true;
            IsRunning = false;
            WaitThen(2.0f, () => GameManager.Instance.NextScene());
        }

        public override void OnStart()
        {
            Merlin.Spawn();
            Barons.ForEach(b => b.Spawn());

            GameManager.Instance.NarratorClip(NarratorFirstSpeech);
            Barons[0].AudioSource.clip = BaronSpeech;
            Merlin.AudioSource.clip = MerlinToBaronsSpeech;
            Arthur.AudioSource.clip = ArthurCardsSpeech;

            IsRunning = true;
        }

    }
}
