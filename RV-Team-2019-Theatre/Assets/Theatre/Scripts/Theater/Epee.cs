using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Theater
{
    public class Epee : MonoBehaviour
    {
   

        void OnTriggerExit(Collider coll)
        {
            if (coll.gameObject.tag == "SwordRock")
            {

                Debug.Log("Exit the rock");
                
                Destroy(GetComponent<LinearDrive>());
                Destroy(GetComponent<LinearMapping>());
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().isTrigger = false;
                gameObject.AddComponent<Throwable>();

            }
        }
    }
}
