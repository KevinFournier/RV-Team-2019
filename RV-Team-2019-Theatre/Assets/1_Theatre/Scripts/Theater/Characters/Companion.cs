using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theater
{

    public enum CompanionType
    {
        None = -1,
        Merlin = 0,
        Guenievre = 1,
        R2D2 = 2,
        Jesus = 3,
        Brush = 4
    }

    public class Companion : Agent
    {
        [SerializeField] private CompanionType type;

        public CompanionType Type => type;
        public int Index() => (int)type;
        
        void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }
    }
    
}
