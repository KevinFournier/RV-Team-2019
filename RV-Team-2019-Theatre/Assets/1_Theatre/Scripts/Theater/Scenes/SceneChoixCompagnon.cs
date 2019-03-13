using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneChoixCompagnon : Scene
    {
        

        public int CardsStartIndex = 0;
        public int CardsEndIndex = 0;

        [Header("Player and possible companion")]
        public Player Arthur;
        public List<Companion> Companions;
        [SerializeField] private CompanionType chosenCompanion;

        [Header("Triggers and bools")]
        [SerializeField] private bool areCardSpwaned = false;
        [SerializeField] private bool areCardSpwaning = false;
        public bool CardTrigger = false;
        [SerializeField] private bool isCardSelected = false;

        [SerializeField] private bool areMerlinAndArthurSpeaking = false;
        [SerializeField] private bool areMerlinAndArthurDoneSpeking = false;



        [Header("Dialogue Merlin/Arthur")]
        [SerializeField] private AudioClip[] merlinAndArthurDialogue;

        [Header("Companion replica")]
        [Tooltip("AudioClip for the chosen companion.\n" +
            "In this order : Merlin, Genièvre, R2D2, Jesus.\n" +
            "The toilet brush doesn't have a replica.")]
        [SerializeField] private List<AudioClip> compagnionSpeech;

        [Header("End replica, by Arthur")]
        [SerializeField] private AudioClip ArthurEndSpeech;

        [Space]
        [Header("Speeches Delays")]
        [SerializeField] private float curtainsOpeningDelay = 2.0f;
        [SerializeField] private float curtainsClosingDelay = 2.0f;
        [Tooltip("Delays before each replica.\n" +
            "The first delay is the time before the start of the dialogue.")]
        [SerializeField] private float[] merlinAndArthurDialogueDelay;
        [Tooltip("Time between the end of the last Arthur replica in dialogue" +
            "and Merlin's movement.")]
        [SerializeField] private float merlinMovementDelay = 2.0f;

        [Space]
        [SerializeField] private float companionSpeechDelay = 2.0f;
        [SerializeField] private float arthurEndDelay = 2.0f;

        [Space]
        [Header("Cards Delays")]
        [SerializeField] private float cardsSpawnDelay = 2.0f;

        public void SetCompanion(CompanionType companion)
        {
            chosenCompanion = companion;
            Arthur.Companion = Companions[(int)companion];
            Arthur.Companion.AudioSource.clip
                = compagnionSpeech[(int)companion];
        }

        /// <summary>
        /// Dialogue entre Arthur et Merlin.
        /// Dans cet ordre : M > A > M > A > M
        /// </summary>
        /// <param name="replicaIndex"></param>
        private void merlinAndArthur(int replicaIndex)
        {
            AudioSource audioSource;

            areMerlinAndArthurSpeaking = true;

            if (replicaIndex >= merlinAndArthurDialogue.Length)
            {
                areMerlinAndArthurSpeaking = false;
                areMerlinAndArthurDoneSpeking = true;
                resetTime();
            }
            else
            {

                if (replicaIndex % 2 == 0)
                    audioSource =
                        Companions[(int)CompanionType.Merlin].AudioSource;
                else
                    audioSource = Arthur.AudioSource;

                if (replicaIndex == 4)
                    merlinMove(); // Merlin à une animation avant sa dernière réplique
                else
                    WaitThen(
                        merlinAndArthurDialogueDelay[replicaIndex],
                        playSound);
            }

            // Local functions
            void merlinMove()
            {
                WaitThen(merlinMovementDelay, () =>
                {
                    // TODO: Start Animation

                    WaitThen(
                        merlinAndArthurDialogueDelay[replicaIndex],
                        playSound);
                });
            }
            void playSound() => PlaySoundThen(audioSource, nextReplica);
            void nextReplica() => merlinAndArthur(replicaIndex + 1);

        }

        private void spawnCards()
        {
            areCardSpwaning = true;

            WaitThen(
                cardsSpawnDelay,
                () =>
                {
                    CardManager.Instance.SpawnCards(
                        CardsStartIndex,
                        CardsEndIndex);

                    resetTime();
                    areCardSpwaned = true;
                    areCardSpwaning = false;
                });
        }
        private void endDialogue()
        {
            Arthur.AudioSource.clip = ArthurEndSpeech;

            WaitThen(companionSpeechDelay, companionReplica);

            void companionReplica()
            {
                PlaySoundThen(
                        Arthur.Companion.AudioSource,
                        arthurReplica,
                        arthurEndDelay
                    );
            }
            void arthurReplica()
            {
                PlaySoundThen(Arthur.AudioSource);
                OnEnd();
            }
        }

        private void Awake()
        {
        }

        void Update()
        {
            if (!IsRunning)
                return;

            time += Time.deltaTime;


            var dialogueCondition =
                !areMerlinAndArthurSpeaking
                && !areMerlinAndArthurDoneSpeking
                && itIsTime(merlinAndArthurDialogueDelay[0]);

            var cardCondition =
                areMerlinAndArthurDoneSpeking
                && !areCardSpwaned
                && !areCardSpwaning
                && itIsTime(cardsSpawnDelay);

            var companionCondition =
                areCardSpwaned
                && CardTrigger
                && !isCardSelected;



            if (dialogueCondition)
                merlinAndArthur(0); // Dialogue Merlin/Arthur
            else if (cardCondition)
                spawnCards(); // Choix des cartes
            else if (companionCondition)
                endDialogue(); // Dialogue de fin. Appelle la methode End().
        }

        override public void OnStart()
        {
            GameManager.Instance.OpenCurtain(CurtainType.Back);
            WaitThen(curtainsOpeningDelay, () => IsRunning = true);
        }


        override public void OnEnd()
        {
            GameManager.Instance.CloseCurtain(CurtainType.Back);
            IsRunning = false;
        }

    }
}