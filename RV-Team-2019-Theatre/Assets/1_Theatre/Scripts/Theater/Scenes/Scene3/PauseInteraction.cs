using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Theater;

public class PauseInteraction : MonoBehaviour
{
    public PlayableDirector masterDirector;
    public Player player;

   

    private void OnEnable()
    {
        masterDirector.Pause();
        Combat.combatCommence = true;
        if (player.Companion.Type == CompanionType.Guenievre)
        {
            player.Companion.GetComponentInChildren<Animator>().SetBool("FightMode", true);
            player.Companion.GetComponentInChildren<Animator>().SetBool("isEnnemyAttacking", true);
        }
    }
}
