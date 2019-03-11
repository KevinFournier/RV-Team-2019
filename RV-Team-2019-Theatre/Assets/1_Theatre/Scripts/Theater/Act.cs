using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Act : MonoBehaviour
    {

        [SerializeField] private List<Scene> scenes;
        private Queue<Scene> _scenes;
        private Scene currentScene;

        private void Awake()
        {
            if (scenes == null)
                _scenes = new Queue<Scene>();
            else
                _scenes = new Queue<Scene>(scenes);
        }

        #region Public Methods

        public void OnStart()
        {

            if (_scenes.Count > 0)
            {
                currentScene = _scenes.Dequeue();
                currentScene.OnStart();
            }
        }
        public void OnEnd()
        {
            // TODO: Implement
        }

        public void NextScene()
        {
            currentScene.OnEnd();

            if (_scenes.Count > 0)
            {
                currentScene = _scenes.Dequeue();
                currentScene.OnStart();
            }
            else
            {
                OnEnd();
            }
        }
        
        #endregion

    }
}