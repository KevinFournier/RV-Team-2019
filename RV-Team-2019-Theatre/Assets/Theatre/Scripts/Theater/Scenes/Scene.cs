﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{
    public abstract class Scene : MonoBehaviour
    {
        public abstract void OnStart();
        public abstract void OnEnd();
    }
}