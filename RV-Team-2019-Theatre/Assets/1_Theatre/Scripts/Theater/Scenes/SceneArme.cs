using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneArme : Scene
    {
        public int CardsStartIndex = 0;
        public int CardsEndIndex = 0;
        
        
        public bool IsIntroFinish = false;
        public bool IsIntroRunning = false;

        public bool AreBaronsAndMerlinFinished = false;
        public bool AreBaronsAndMerlinRunning = false;

        public bool AreCardsSpwaned = false;
        public bool AreCardsSpawning = false;

        public bool IsCardSelected = false;
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


        private GameManager gm;
        private CardManager cm;

        private bool isStarted = false;
        private bool isFinished = false;

        private float time = 0.0f;

        #region Unity Methods
        private void Start()
        {
            gm = GameManager.Instance;
            cm = CardManager.Instance;
        }

        private void Update()
        {
            if (!isStarted)
                return;

            if (isFinished)
                OnEnd();

            // choix carte
            // Arthur à la selection
            // retirage épée
            // Dernier speech merlin

            time += Time.deltaTime;

            if (!IsIntroFinish && itIsTime(NarratorSpeechDelay))
                intro();

            else if (!AreBaronsAndMerlinFinished && itIsTime(BaronsSpeechDelay))
                baronsAndMerlin();

            else if (!AreCardsSpwaned && itIsTime(MerlinAndCardDelay))
                merlinAndCards();

            else if (IsCardSelected && !IsSwordTaken)
                whenCardSelected();

            else if (IsSwordTaken)
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
            gm.NarratorClip(NarratorFirstSpeech);
            PlaySoundThen(
                gm.Narrator(),
                () =>
                {
                    gm.OpenCurtains();
                    resetTime();
                    IsIntroFinish = true;
                    IsIntroRunning = false;
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
            WaitThen(MerlinCardsSpeechDelay, () => PlaySoundThen(Merlin.AudioSource));

            // Attend 2s qu'il ait commencé puis fait spawn les cartes.
            CardsSpawnDelay += MerlinCardsSpeechDelay;
            WaitThen(
                CardsSpawnDelay,
                () =>
                {
                    cm.SpawnCards(CardsStartIndex, CardsEndIndex);

                    resetTime();
                    AreCardsSpwaned = true;
                    AreCardsSpawning = false;
                });
        }

        private void whenCardSelected()
        {
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
            gm.CloseCurtains();
            WaitThen(2.0f, () => gm.NextScene());
        }

        public override void OnStart()
        {
            Merlin.Spawn();
            Barons.ForEach(b => b.Spawn());

            gm.NarratorClip(NarratorFirstSpeech);
            Barons[0].AudioSource.clip = BaronSpeech;
            Merlin.AudioSource.clip = MerlinToBaronsSpeech;
            Arthur.AudioSource.clip = ArthurCardsSpeech;
        }

    }
}
