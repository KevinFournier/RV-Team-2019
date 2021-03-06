﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater {
    public class Soldats : MonoBehaviour
    {
        public AudioClip[] hit;
        public ParticleSystem explosionCartons;
        public bool dead = false;
        
     
        // Start is called before the first frame update

        void Update()
        {
            if(dead)
            {
                StartCoroutine(AnimDestruction());
                dead = false;
            }
                 
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "SwordBlade")
            {

                StartCoroutine(AnimDestruction());

            }
        }

        IEnumerator AnimDestruction()
        {
            GetComponent<AudioSource>().PlayOneShot(hit[CardManager.EpeeChosen]);
            GetComponent<Collider>().enabled = false;
            //Play particle
            yield return new WaitForSeconds(0.2f);
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.GetComponent<MeshRenderer>() != null)
                {
                    child.GetComponent<MeshRenderer>().enabled = false;

                }
            }
            explosionCartons.Play();
            yield return new WaitForSeconds(4.0f);
            gameObject.SetActive(false);
            dead = true;
            //Increment soldats to go 
        }
    }
}
