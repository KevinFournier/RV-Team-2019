using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Theater
{
    public class Epee : MonoBehaviour
    {

        public Hand rightHand;
        public Hand leftHand;

        public GameObject rocher;

        bool stuck = true;

        void Start()
        { 
        }

        void LateUpdate()
        {
            if (stuck == true)
            {
                transform.position = new Vector3(rocher.transform.position.x, transform.position.y, rocher.transform.position.z);
                transform.rotation = new Quaternion(rocher.transform.rotation.x, rocher.transform.rotation.y, rocher.transform.rotation.z,0.0f);
            }
            
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.gameObject.tag == "SwordRock")
            {
                stuck = false;
                Debug.Log("Exit the rock");
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GetComponent<BoxCollider>().isTrigger = false;
            }
        }


        void OnTriggerStay(Collider coll)
        {
            if (coll.gameObject.tag == "Player" && stuck==true)
            {
                if (leftHand.grabPinchAction.stateDown)
                {
                    transform.position = new Vector3(rocher.transform.position.x, leftHand.transform.position.y, rocher.transform.position.z);
                    transform.rotation = new Quaternion(rocher.transform.rotation.x, rocher.transform.rotation.y, rocher.transform.rotation.z, 0.0f);
                }
                if (rightHand.grabPinchAction.stateDown)
                {
                    transform.position = new Vector3(rocher.transform.position.x, rightHand.transform.position.y, rocher.transform.position.z);
                    transform.rotation = new Quaternion(rocher.transform.rotation.x, rocher.transform.rotation.y, rocher.transform.rotation.z, 0.0f);
                }
            }
            if (coll.gameObject.tag == "SwordRock")
            {
                Debug.Log("bim");
            }

        }
    }
}
