using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour
    {
        public AudioSource AudioSource;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }
    }
}
