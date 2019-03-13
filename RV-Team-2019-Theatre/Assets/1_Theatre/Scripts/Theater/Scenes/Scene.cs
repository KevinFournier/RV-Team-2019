using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public abstract class Scene : MonoBehaviour
    {
        public int sceneNum;
        public bool IsRunning;
        public bool IsFinish;

        public abstract void OnStart();
        public abstract void OnEnd();

        protected float time = 0.0f;

        // Time management methods
        protected void resetTime() => time = 0.0f;
        protected bool itIsTime(float delay) => time >= delay;



        /// <summary>
        /// Wait a certain time then invoke the given procedure.
        /// </summary>
        /// <param name="time">Time in secondes.</param>
        /// <param name="func">Procedure to invoke.</param>
        private IEnumerator waitThen(float time, System.Action func)
        {
            yield return new WaitForSeconds(time);

            if (func != null)
                func.Invoke();
        }
        /// <summary>
        /// Wait a certain time then invoke the given procedure.
        /// </summary>
        /// <param name="time">Time in secondes.</param>
        /// <param name="func">Procedure to invoke.</param>
        protected void WaitThen(float time, System.Action func)
            => StartCoroutine(waitThen(time, func));

        /// <summary>
        /// Play a sound once then invoke the given procedure. 
        /// </summary>
        /// <param name="audioSource">AudioSource to play.</param>
        /// <param name="func">Procedure to invoke.</param>
        /// <param name="additionalTime">Time in secondes to wait after the sound ends.</param>
        protected void PlaySoundThen(
            AudioSource audioSource,
            System.Action func = null,
            float additionalTime = 0.0f
        )
        {
            audioSource.loop = false;
            audioSource.Play();

            StartCoroutine(onSoundEnd(audioSource, func, additionalTime));
        }

        /// <summary>
        /// Wait for the given sound to be finished, for additional time, then run the procedure argument.
        /// </summary>
        /// <param name="audioSource">AudioSource to check.</param>
        /// <param name="func">Procedure to invoke.</param>
        /// <param name="additionalTime">Time in secondes to wait after the sound ends.</param>
        private IEnumerator onSoundEnd(
            AudioSource audioSource,
            System.Action func,
            float additionalTime = 0.0f
        )
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            yield return new WaitForSeconds(additionalTime);

            if (func != null)
                func.Invoke();
        }

    }
}