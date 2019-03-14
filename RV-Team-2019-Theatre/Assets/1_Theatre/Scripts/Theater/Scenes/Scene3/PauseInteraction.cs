using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseInteraction : MonoBehaviour
{
    public PlayableDirector masterDirector;

    private void OnEnable()
    {
        masterDirector.Pause();
    }
}
