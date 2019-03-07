using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public abstract class Scene : MonoBehaviour
    {
        public int sceneNum;


        public abstract void OnStart();
        public abstract void OnEnd();


        /// <summary>
        /// Wait a certain time then invoke the given procedure.
        /// </summary>
        /// <param name="time">Time in secondes.</param>
        /// <param name="func">Procedure to invoke.</param>
        /// <returns></returns>
        protected virtual IEnumerator WaitAndInvoke(float time, System.Action func)
        {
            yield return new WaitForSeconds(time);
            func.Invoke();
        }

        /// <summary>
        /// Play a sound once then invoke the given procedure. 
        /// </summary>
        /// <param name="audioSource">AudioSource to play.</param>
        /// <param name="func">Procedure to invoke.</param>
        /// <param name="additionalTime">Time in secondes to wait after the sound ends.</param>
        protected virtual void PlaySoundThen(
            AudioSource audioSource,
            System.Action func,
            float additionalTime = 0.0f
        )
        {
            audioSource.loop = false;
            audioSource.Play();

            OnSoundEnd(audioSource, func, additionalTime);
        }

        /// <summary>
        /// Wait for the given sound to be finished, for additional time, then run the procedure argument.
        /// </summary>
        /// <param name="audioSource">AudioSource to check.</param>
        /// <param name="func">Procedure to invoke.</param>
        /// <param name="additionalTime">Time in secondes to wait after the sound ends.</param>
        /// <returns></returns>
        protected virtual IEnumerator OnSoundEnd(
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