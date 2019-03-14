using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Theater;

public class ChooseCharacter : MonoBehaviour
{
    
    public PlayableDirector playableDirector;
    public PlayableAsset[] TimelinesCharacter;

    public void ChooseBetweenCharacters(Companion character)
    {
        playableDirector.playableAsset = TimelinesCharacter[(int)character.Type];
    }
    
   
}
