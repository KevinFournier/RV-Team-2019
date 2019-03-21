using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Theater : MonoBehaviour
    {
        public AudioSource Narrator;
        public AudioSource Public;

        [SerializeField] private List<AudioClip> applaudissement;

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

        public void ApplaudissementsLight()
        {
            Public.clip = applaudissement[0];
            Public.Play();
        }

        public void ApplaudissementsMedium()
        {
            Public.clip = applaudissement[1];
            Public.Play();
        }

        public void ApplaudissementsHigh()
        {
            Public.clip = applaudissement[2];
            Public.Play();
        }

        public void ApplaudissementsWoohoo()
        {
            Public.clip = applaudissement[2];
            Public.Play();
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
            curtainFront.Open();
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
            curtainFront.Close();
        }

        #endregion
    }
}