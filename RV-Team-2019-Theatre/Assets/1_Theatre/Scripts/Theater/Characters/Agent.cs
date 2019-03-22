using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    [RequireComponent(typeof(AudioSource))]
    public class Agent : MonoBehaviour
    {
        public AudioSource AudioSource;
        public Animator animationMobile;
        public Animator animationStatique;

        public ParticleSystem fire;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public void Spawn(bool b)
        {
            animationMobile.SetBool("Spawn", b);
        }

        public void Fight(bool b)
        {
            animationStatique.SetBool("FightMode", b);
        }
        public void Walk(bool b)
        {
            animationStatique.SetBool("isWalking", b);
        }
        public void Attack(bool b)
        {
            animationStatique.SetBool("isEnnemyAttacking", b);
        }
        public void Talk1(bool b)
        {
            animationStatique.SetBool("isTalking_1", b);
        }
        public void Talk2(bool b)
        {
            animationStatique.SetBool("isTalking_2", b);
        }
        public void Applause(bool b)
        {
            animationStatique.SetBool("isApplause", b);
        }
        public void Dead(bool b)
        {
            animationStatique.SetBool("isDead", b);
        }
        public void Fire(bool b)
        {
            if (b)
            {
                fire.Play();
            }
            else
            {
                fire.Stop();
            }

        }
    }
}
