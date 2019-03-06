using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public class Act : MonoBehaviour
    {
        private Queue<Scene> scenes;
        private Scene currentScene;

        private void Start()
        {
            OnStart();
        }

        public void OnStart()
        {
            if (scenes == null)
                scenes = new Queue<Scene>();

            if (scenes.Count > 0)
            {
                currentScene = scenes.Dequeue();
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

            if (scenes.Count > 0)
            {
                currentScene = scenes.Dequeue();
                currentScene.OnStart();
            }
            else
            {
                OnEnd();
            }
        }
    }
}