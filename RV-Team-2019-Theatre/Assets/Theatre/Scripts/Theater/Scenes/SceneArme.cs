using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneArme : Scene
    {
        public int CardsStartIndex = 0;
        public int CardsEndIndex = 0;

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

            // Le narrateur parle, attend 2s après la fin puis ouvre les ridaux.
            gm.NarratorClip(NarratorFirstSpeech);
            PlaySoundThen(
                gm.Narrator(),
                () => gm.OpenCurtains(),
                2.0f);


            // Spawn de Merlin
            Merlin.Spawn();

            // Merlin parle
            Merlin.AudioSource.clip = MerlinCardsSpeech;
            PlaySoundThen(Merlin.AudioSource, null);

            // Attend 2s qu'il ait commencé puis fait spawn les cartes.

            WaitAndInvoke(2.0f, () => CardManager.Instance.SpawnCards(CardsStartIndex, CardsEndIndex));

            // Ne rien faire tant qu'une carte n'a pas été séléctionée.


            // Spawn du rocher
            // Rien tant que l'arme n'est pas retirée.
            // Récupération de l'arme.

            Player.AudioSource.clip = ArthurEndSpeech;
            PlaySoundThen(
                Player.AudioSource,
                () => OnEnd(),
                2.0f);


        }

        public override void OnEnd()
        {

        }
        public override void OnStart()
        {

        }
    }
}
