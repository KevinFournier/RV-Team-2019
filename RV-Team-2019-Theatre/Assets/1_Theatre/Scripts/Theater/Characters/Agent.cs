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

        public void Spawn()
        {
            transform.position = spawnPosition;
            // TODO: Implement
        }
    }
}
