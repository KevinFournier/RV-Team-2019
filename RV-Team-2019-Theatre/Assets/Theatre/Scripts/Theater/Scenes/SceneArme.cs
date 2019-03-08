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

        public bool PlayerHasSelectedCard = false;
        public bool PlayerHasTakenSword = false;
        public bool SceneFinished = false;

        public float CurtainsOpeningDelay = 2.0f;
        public float NarratorSpeechDelay = 2.0f;
        public float MerlinAndCardDelay = 2.0f;
        public float MerlinSpeechDelay = 2.0f;
        public float CardsSpawnDelay = 2.0f;

        public float EndSpeechDelay = 2.0f;

        public AudioClip NarratorFirstSpeech;
        public AudioClip MerlinCardsSpeech;
        public AudioClip ArthurEndSpeech;

        public Agent Merlin;

        public Player Player;


        private GameManager gm;


        private void Start()
        {
            gm = GameManager.Instance;
        }

        private void Update()
        {
            if (SceneFinished)
                OnEnd();
        }


        private void intro()
        {
            // Le narrateur parle, attend 2s après la fin puis ouvre les ridaux.
            gm.NarratorClip(NarratorFirstSpeech);
            PlaySoundThen(
                gm.Narrator(),
                () => gm.OpenCurtains(),
                CurtainsOpeningDelay);
        }

        private void whenMerlinAndCardSpawn()
        {
            CardsSpawnDelay = MerlinSpeechDelay + 2.0f;

            // Spawn de Merlin
            Merlin.Spawn();

            // Merlin commence à parler x secondes après sont apparition.
            Merlin.AudioSource.clip = MerlinCardsSpeech;
            WaitThen(MerlinSpeechDelay, () => PlaySoundThen(Merlin.AudioSource));

            // Attend 2s qu'il ait commencé puis fait spawn les cartes.
            WaitThen(
                CardsSpawnDelay,
                () => CardManager.Instance.SpawnCards(CardsStartIndex, CardsEndIndex));
        }

        private void whenSwordTaken()
        {
            waitForThen(
                () => PlayerHasTakenSword,
                () => WaitThen(EndSpeechDelay, () => endSpeech())
            );
        }

        private void endSpeech()
        {
            Player.AudioSource.clip = ArthurEndSpeech;
            PlaySoundThen(
                Player.AudioSource,
                () => OnEnd(),
                2.0f);
        }



        public override void OnEnd()
        {
            gm.NextScene();
        }

        public override void OnStart()
        {
            WaitThen(NarratorSpeechDelay, () => intro());

            MerlinAndCardDelay
                += NarratorSpeechDelay
                + gm.Narrator().clip.length
                + CurtainsOpeningDelay;
            WaitThen(MerlinAndCardDelay, () => whenMerlinAndCardSpawn());

            // Ne rien faire tant qu'une carte n'a pas été séléctionée.
            waitForThen(
                () => PlayerHasSelectedCard,
                () =>
                {
                    //TODO: Spawn du rocher

                    // Rien tant que l'arme n'est pas retirée.
                    whenSwordTaken();
                }
            );
        }

        private IEnumerator waitForThen(Func<bool> func, Action action)
        {
            yield return new WaitUntil(func);

            if (action != null)
                action.Invoke();
        }
    }
}
