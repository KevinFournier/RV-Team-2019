using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneArme : Scene
    {
        public AudioSource NarratorFirstSpeech;
        public AudioSource MerlinCardsSpeech;
        public AudioSource ArthurEndSpeech;

        public Agent Merlin;

        public Player Player;


        private GameManager gm;

        private void Start()
        {
            gm = GameManager.Instance;
            // CardManager.Instance.SetCards();
        }

        private void Update()
        {

            // Le narrateur parle, attend 2s après la fin puis ouvre les ridaux.
            PlaySoundThen(
                NarratorFirstSpeech,
                () => gm.OpenCurtains(),
                2.0f);


            // Spawn de Merlin
            Merlin.Spawn();

            // Merlin parle
            PlaySoundThen(MerlinCardsSpeech, null);

            // Attend 2s qu'il ait commencé puis fait spawn les cartes.
            WaitAndInvoke(2.0f, () => CardManager.Instance.SpawnCards());
            // Ne rien faire tant qu'une carte n'a pas été séléctionée.


            // Spawn du rocher
            // Rien tant que l'arme n'est pas retirée.
            // Récupération de l'arme.

            PlaySoundThen(
                ArthurEndSpeech,
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
