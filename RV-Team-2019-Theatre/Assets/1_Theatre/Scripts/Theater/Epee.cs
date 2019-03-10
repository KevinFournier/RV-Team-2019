using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Theater
{
    public class Epee : MonoBehaviour
    {
        public List<GameObject> lame;

        void OnTriggerExit(Collider coll)
        {
            if (coll.gameObject.tag == "SwordRock")
            {

                Debug.Log("Exit the rock");
           
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().isTrigger = false;
                gameObject.AddComponent<Throwable>();
                Destroy(GetComponent<LinearDrive>());
                Destroy(GetComponent<LinearMapping>());

            }
        }
    }
}
