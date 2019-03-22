using System.Collections;
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

        public Combat[] combatCompanion;

        public GameObject[] bajons;

        // Update is called once per frame
        void Update()
        {

            if (Combat.combatFini)
            {
                scene3director.Play();
            }
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

            foreach (Combat item2 in combatCompanion)
            {
                item2.Arthur = Arthur.Companion.transform;
            }
            scene3director.Play();
            foreach (GameObject item in bajons)
            {
                item.SetActive(false);
            }
        }
    }
}
