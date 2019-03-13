using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{

    public enum CompanionType
    {
        Merlin = 0,
        Genievre = 1,
        R2D2 = 2,
        Jesus = 3,
        Brush = 4
    }

    public class Companion : Agent
    {
        [SerializeField] private CompanionType type;

        public CompanionType GetCompanionIndex() => type;
        public int GetIndex() => (int)type;
    }
}
