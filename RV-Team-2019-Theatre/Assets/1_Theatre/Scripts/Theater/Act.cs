﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Act : MonoBehaviour
    {
        [SerializeField] private float timeBetweenScene = 15.0f;
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

        private IEnumerator sceneTransition(float time)
        {
            yield return new WaitForSeconds(time);

            
            currentScene.UnloadDecors();      // Supprime les décors de la scene actuelle.
            currentScene = _scenes.Dequeue(); // Change la scene actuelle
            currentScene.LoadDecors();        // Génère les décors de la scene actuelle.

            currentScene.OnStart();
        }

        #region Public Methods

        public void OnStart()
        {

            if (_scenes.Count > 0)
            {
                currentScene = _scenes.Dequeue();
                currentScene.LoadDecors(); // Génère les décors de la scene actuelle.
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
                StartCoroutine(sceneTransition(timeBetweenScene));
            }
            else
            {
                OnEnd();
            }
        }

        public Scene GetCurrentScene() => currentScene;

        #endregion

    }
}