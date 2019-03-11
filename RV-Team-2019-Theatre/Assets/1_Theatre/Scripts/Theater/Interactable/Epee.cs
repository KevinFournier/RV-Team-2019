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
                transform.parent.gameObject.GetComponent<Rocher>().rockExplosion.Play();
                transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
                transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;

                (GameManager.Instance.GetCurrentAct().GetCurrentScene() as SceneArme).SwordTrigger = true;

                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().isTrigger = false;
                gameObject.AddComponent<Throwable>();
                Destroy(GetComponent<LinearDrive>());
                Destroy(GetComponent<LinearMapping>());
                gameObject.transform.parent = null;

            }
        }
    }
}
