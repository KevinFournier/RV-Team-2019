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

        [SerializeField] private List<Act> acts;
        private Queue<Act> _acts;
        private Act currentAct;


        private void Awake()
        {
            if (acts == null)
                _acts = new Queue<Act>();
            else
                _acts = new Queue<Act>(acts);
        }

        private void Start()
        {
            OnStart();
        }


        #region Private Methods

        private void OnStart()
        {
            if (_acts.Count > 0)
            {
                currentAct = _acts.Dequeue();
                GameManager.Instance.SetAct(currentAct);
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
            
            if (_acts.Count > 0)
            {
                currentAct = _acts.Dequeue();
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