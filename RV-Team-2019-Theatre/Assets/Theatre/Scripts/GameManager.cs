﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Theater;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Theater.Theater currentTheater;
    private Act currentAct;

    // Theater related methods
    public void SetTheater(Theater.Theater theater) => currentTheater = theater;
    public void NextAct() => currentTheater.NextAct();
    public void OpenCurtain(CurtainName cn) => currentTheater.OpenCurtain(cn);
    public void CloseCurtain(CurtainName cn) => currentTheater.CloseCurtain(cn);

    // Act related methods
    public void SetAct(Act act) => currentAct = act;
    public void NextScene() => currentAct.NextScene();

    private void Awake()
    {
        Instance = this;
    }
}
