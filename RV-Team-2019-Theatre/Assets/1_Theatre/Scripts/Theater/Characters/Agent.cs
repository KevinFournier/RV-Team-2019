using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    [RequireComponent(typeof(AudioSource))]
    public class Agent : MonoBehaviour
    {
        public AudioSource AudioSource;

        [SerializeField]
        private Vector3 spawnPosition;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public void Spawn()
        {
            //Play anim;
            GetComponent<Animation>().Play();
            // TODO: Implement
        }
        public void AnimMove()
        {

        }
        
    }
}
