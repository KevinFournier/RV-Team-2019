using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneTutoriel : Scene
    {
        public Animator manivelle;
        public GameObject[] lesBaronsdeLaScene3;

        public void OnPlay()
        {
            //ManivelleOpen
            manivelle.SetBool("Spawn", true);
        }

        public override void OnStart()
        {
            foreach (GameObject item in lesBaronsdeLaScene3)
            {
                item.SetActive(false);
            }
        }

        public override void OnEnd()
        {
            WaitThen(
                1.0f,
                () =>
                {
                    manivelle.SetBool("Spawn", false);
                    GameManager.Instance.ApplaudissementsLight();
                });
        }
    }
}
