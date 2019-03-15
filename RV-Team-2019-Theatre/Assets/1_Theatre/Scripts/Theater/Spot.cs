using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Spot : MonoBehaviour
    {

        public static Transform target;

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(target);
        }
    }
}
