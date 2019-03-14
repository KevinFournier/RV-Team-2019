using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Theater;

public class ChooseFin : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableAsset[] TimelinesFin;

    public void ChooseBetweenCharacters(Companion character)
    {
        playableDirector.playableAsset = TimelinesFin[(int)character.Type];
    }
}
