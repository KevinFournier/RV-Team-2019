using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Theater;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool StarWars = false;
    [SerializeField] private Theater.Theater Theater;
    [SerializeField] private Act currentAct;

    // Theater related methods
    public void SetTheater(Theater.Theater theater) => Theater = theater;
    public void NextAct() => Theater.NextAct();
    public void OpenCurtain(CurtainType cn) => Theater.OpenCurtain(cn);
    public void OpenCurtains() => Theater.OpenCurtains();
    public void CloseCurtain(CurtainType cn) => Theater.CloseCurtain(cn);
    public void CloseCurtains() => Theater.CloseCurtains();
    public Act GetCurrentAct() => currentAct;
    
    /// <summary>
    /// Get the Theater's narrator AudioSource.
    /// </summary>
    /// <returns>The AudioSource of the narrator.</returns>
    public AudioSource Narrator() => Theater.Narrator;
    /// <summary>
    /// Define the next AudioClip to play by the narrator. (Not a list !)
    /// </summary>
    /// <param name="clip">The AudioClip to play.</param>
    public void NarratorClip(AudioClip clip) => Theater.Narrator.clip = clip;

    // Act related methods
    public void SetAct(Act act) => currentAct = act;
    public void NextScene() => currentAct.NextScene();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
}
