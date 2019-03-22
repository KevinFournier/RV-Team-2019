using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class SceneTutoriel : Scene
    {
        public Animator manivelle;
        public GameObject[] lesBaronsdeLaScene3;
        public GameObject panneauTutoriel;

        public AudioClip chuchotements;

        public AudioSource spectators;

        public void OnPlay()
        {
            //ManivelleOpen
            manivelle.SetBool("Spawn", true);
        }

        public override void OnStart()
        {
            spectators.clip = chuchotements;
            spectators.loop = true;
            spectators.Play();
            foreach (GameObject item in lesBaronsdeLaScene3)
            {
                item.SetActive(false);
            }
        }

        public override void OnEnd()
        {
            spectators.loop = false;

            if (panneauTutoriel != null)
            {
                panneauTutoriel.SetActive(false);
            }
            
            WaitThen(
                1.0f,
                () =>
                {
                    manivelle.SetBool("Spawn", false);
                    GameManager.Instance.ApplaudissementsMedium();
                    WaitThen(
                        3.0f,
                        () => panneauTutoriel.SetActive(false));
                });

            
        }
    }
}
