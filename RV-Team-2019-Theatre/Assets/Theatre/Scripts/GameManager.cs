﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Theater;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Theater.Theater currentTheater;
    [SerializeField] private Act currentAct;

    // Theater related methods
    public void SetTheater(Theater.Theater theater) => currentTheater = theater;
    public void NextAct() => currentTheater.NextAct();
    public void OpenCurtain(CurtainType cn) => currentTheater.OpenCurtain(cn);
    public void OpenCurtains() => currentTheater.OpenCurtains();
    public void CloseCurtain(CurtainType cn) => currentTheater.CloseCurtain(cn);
    public void CloseCurtains() => currentTheater.CloseCurtains();
    /// <summary>
    /// Get the Theater's narrator AudioSource.
    /// </summary>
    /// <returns>The AudioSource of the narrator.</returns>
    public AudioSource Narrator() => currentTheater.Narrator;
    /// <summary>
    /// Define the next AudioClip to play by the narrator. (Not a list !)
    /// </summary>
    /// <param name="clip">The AudioClip to play.</param>
    public void NarratorClip(AudioClip clip) => currentTheater.Narrator.clip = clip;

    // Act related methods
    public void SetAct(Act act) => currentAct = act;
    public void NextScene() => currentAct.NextScene();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
