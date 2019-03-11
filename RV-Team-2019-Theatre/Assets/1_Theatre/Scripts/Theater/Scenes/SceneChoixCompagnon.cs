using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneChoixCompagnon : Scene
    {
        public int CardsStartIndex = 0;
        public int CardsEndIndex = 0;

        public Player Arthur;
        public List<Companion> Companions;

        private GameManager gm;
        private CardManager cm;

        private float time = 0.0f;

        private void Awake()
        {
            gm = GameManager.Instance;
            cm = CardManager.Instance;
        }

        void Update()
        {
            if (!IsRunning)
                return;

            time += Time.deltaTime;


        }

        override public void OnStart()
        {
        }


        override public void OnEnd()
        {

        }

    }
}