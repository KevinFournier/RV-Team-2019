using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Theater : MonoBehaviour
    {
        public AudioSource Narrator;

        [SerializeField] private Curtain curtainFront;
        [SerializeField] private Curtain curtainBack;

        private Queue<Act> acts;
        private Act currentAct;


        private void Start()
        {
            OnStart();
        }


        #region Private Methods

        private void OnStart()
        {
            if (acts == null)
                acts = new Queue<Act>();

            if (acts.Count > 0)
            {
                currentAct = acts.Dequeue();
                currentAct.OnStart();
            }
        }
        private void OnEnd()
        {
            // TODO: Implement
        }

        #endregion

        #region Public Methods

        public void NextAct()
        {
            currentAct.OnEnd();
            
            if (acts.Count > 0)
            {
                currentAct = acts.Dequeue();
                currentAct.OnStart();
            }
            else
            {
                OnEnd();
            }
        }


        /// <summary>
        /// Open a Curtain
        /// </summary>
        /// <param name="cn">The type of curtain to open ; Front or Back</param>
        public void OpenCurtain(CurtainType cn)
        {
            if (cn == CurtainType.Back)
                curtainBack.Open();
            else if (cn == CurtainType.Front)
                curtainFront.Open();
        }
        /// <summary>
        /// Open all curtain.
        /// </summary>
        public void OpenCurtains()
        {
            curtainBack.Open();
            curtainBack.Open();
        }

        /// <summary>
        /// Close a Curtain
        /// </summary>
        /// <param name="cn">The type of curtain to close ; Front or Back</param>
        public void CloseCurtain(CurtainType cn)
        {
            if (cn == CurtainType.Back)
                curtainBack.Close();
            else if (cn == CurtainType.Front)
                curtainFront.Close();
        }
        /// <summary>
        /// Close all curtain.
        /// </summary>
        public void CloseCurtains()
        {
            curtainBack.Close();
            curtainBack.Close();
        }

        #endregion
    }
}