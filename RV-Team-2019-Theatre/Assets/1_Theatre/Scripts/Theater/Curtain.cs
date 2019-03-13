using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Theater
{

    public enum CurtainType { Front, Back }

    public class Curtain : MonoBehaviour
    {
        public bool closed=false;
        private Vector3 curtainPos;
        public GameObject manivelle;
        private float manivelleAngle = 0;
        public bool closing=false;
        public bool opening = false;
        public float hauteurOuverture = 11.0f;
        public float hauteurFermeture = 4.0f;
        public bool locked = false;

        public void Start()
        {
            curtainPos = transform.position;
            manivelleAngle = manivelle.GetComponent<CircularDrive>().outAngle;
        }

        public void Update()
        {
            //Manivelle interaction
            if (!locked)
            {
                if (manivelleAngle != manivelle.GetComponent<CircularDrive>().outAngle)
                {
                    if (manivelleAngle < manivelle.GetComponent<CircularDrive>().outAngle && hauteurOuverture >= transform.position.y)
                        transform.position += new Vector3(0, 0.005f, 0);
                    else if (manivelleAngle > manivelle.GetComponent<CircularDrive>().outAngle && hauteurFermeture <= transform.position.y)
                        transform.position += new Vector3(0, -0.005f, 0);
                }
                manivelleAngle = manivelle.GetComponent<CircularDrive>().outAngle;
                if (transform.position.y >= hauteurOuverture)
                {
                    GameManager.Instance.NextScene();
                    locked = true;
                    closed = false;
                    manivelle.GetComponent<Interactable>().enabled = false;
                }
            }
            


            //Close smoothly until the curtain hits the floor
            if (closing && !closed)
            {
                transform.position += new Vector3(0, -0.02f, 0);
            }
            //Open smoothly until the curtain reach its height
            if (opening && closed)
            {
                transform.position += new Vector3(0, 0.02f, 0);
            }

            //Check the max height for the curtain
            if (transform.position.y >= hauteurOuverture)
            {
                closed = false;
                opening = false;
            }
            //Check the min height for the curtain
            if (transform.position.y <= hauteurFermeture)
            {
                closed = true;
                closing = false;
            }

        }


        public void Open()
        {
            opening = true;
            closing = false;
        }

        public void Close()
        {
            closing = true;
            opening = false;
        }
    }
}