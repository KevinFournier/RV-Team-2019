using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour
    {
        public AudioSource AudioSource;
        public AudioListener AudioListener;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioListener = GetComponent<AudioListener>();
        }
    }
}
