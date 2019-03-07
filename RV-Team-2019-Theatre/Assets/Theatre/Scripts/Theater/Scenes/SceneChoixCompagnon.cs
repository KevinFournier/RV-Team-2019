using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneChoixCompagnon : Scene
    {
        public int sceneState=0;
        bool conditionChecked = false;


        void Update()
        {
            if (conditionChecked == true)
            {
                sceneState++;
                TimelineManager();
                conditionChecked = false;
            }
        }

        override public void OnStart()
        {
            TimelineManager();
        }

        public void TimelineManager()
        {
            switch (sceneState)
            {
                //MERLIN
                case 0:
                    Debug.Log("Mon bon sir, il vous faut un gouvernement stable");
                    Invoke("ConditionCheck", 5.0f);
                    break;
                //ARTHUR
                case 1:
                    Debug.Log("Et les meilleurs guerriers doivent siéger à ma table");
                    Invoke("ConditionCheck", 5.0f);
                    break;
                //MERLIN
                case 2:
                    Debug.Log("Vous devriez choisir un homme de confiance");
                    Invoke("ConditionCheck", 5.0f);
                    break;
                //ARTHUR
                case 3:
                    Debug.Log("Un partenaire en somme! Qui connaîtrait la science de conduire mes armées!");
                    Invoke("ConditionCheck", 5.0f);
                    break;
                //Spawn the cards for the choice of the happy fellow
                case 4:
                    //Play sound effects?
                    CardManager.Instance.SpawnCards(4, 7);
                    break;
                //Play ...
                case 5:
                    Debug.Log("Choisissez sagement ...");

                    break;
            }
        }

        override public void OnEnd()
        {

        }


        void CheckCondition()
        {
            conditionChecked = true;
        }
    }
}
