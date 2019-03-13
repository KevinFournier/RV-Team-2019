using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    [RequireComponent(typeof(AudioSource))]
    public class Agent : MonoBehaviour
    {
        public AudioSource AudioSource;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public void Spawn()
        {
            //Play anim;
            //GetComponent<Animation>().Play();
            // TODO: Implement
            // GetComponentInChildren<Animator>().SetBool("FightMode", true);
            GetComponentInChildren<Animator>().SetBool("FightMode", true);
            GetComponentInChildren<Animator>().SetBool("isEnnemyAttacking", true);
        }
        public void Update()
        {

        }
        public void AnimMove()
        {

        }
        
    }
}
