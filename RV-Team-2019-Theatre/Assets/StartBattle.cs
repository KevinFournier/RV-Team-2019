using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Theater;

public class StartBattle : MonoBehaviour
{
    private void OnEnable()
    {
        Combat.combatCommence = true;
    }
}
