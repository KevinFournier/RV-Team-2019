using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Theater : MonoBehaviour
    {
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
        public void OpenCurtain(CurtainName cn)
        {
            if (cn == CurtainName.Back)
                curtainBack.Open();
            else if (cn == CurtainName.Front)
                curtainFront.Open();
        }
        /// <summary>
        /// Close a Curtain
        /// </summary>
        /// <param name="cn">The type of curtain to close ; Front or Back</param>
        public void CloseCurtain(CurtainName cn)
        {
            if (cn == CurtainName.Back)
                curtainBack.Close();
            else if (cn == CurtainName.Front)
                curtainFront.Close();
        }

        #endregion
    }
}