using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Theater
{
    public class Card : MonoBehaviour
    {

        public Hand LeftHand;
        public Hand RightHand;

        public ParticleSystem smokeEffect;
        public ParticleSystem Aura;
        public bool IsSelected = false;

        void Start()
        {
            smokeEffect.Play();
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                Debug.Log(gameObject.name + " is on collision with " + coll.gameObject.name);
                if (Input.GetKeyDown(KeyCode.E) || LeftHand.grabPinchAction.stateDown || RightHand.grabPinchAction.stateDown)
                {
                    Debug.Log(" On Clique");
                    Select();
                }
            }
        }

        private void Select()
        {
            IsSelected = true;
        }

        public void Spawn()
        {
            Debug.Log("Cards are spawned");
            Invoke("SetActiveCard", 0.3f);

        }
        public void Hide()
        {
            Debug.Log("Hide cards");
            gameObject.SetActive(false);
        }
        public void SetActiveCard()
        {
            gameObject.SetActive(true);
        }
    }
}
