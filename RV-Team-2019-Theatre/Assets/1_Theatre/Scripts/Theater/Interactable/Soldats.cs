using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater {
    public class Soldats : MonoBehaviour
    {
        private Vector2 velocity = Vector2.zero;
        public ParticleSystem explosionCartons;
        public bool dead = false;
        
     
        // Start is called before the first frame update


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
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            //Play particle
            explosionCartons.Play();
            yield return new WaitForSeconds(4.0f);
            gameObject.SetActive(false);
            dead = true;
            //Increment soldats to go 
        }
    }
}
