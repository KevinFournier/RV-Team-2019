﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocher : MonoBehaviour
{
    public ParticleSystem rockExplosion;

    public void PlayLeSFX()
    {
        GetComponent<AudioSource>().Play();
    }
}
