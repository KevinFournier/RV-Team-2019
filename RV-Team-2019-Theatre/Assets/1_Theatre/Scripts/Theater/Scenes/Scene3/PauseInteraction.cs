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
            player.Companion.Fight(true);
            player.Companion.Attack(true);
        }
        if (player.Companion.Type == CompanionType.Jesus)
        {
            player.Companion.Dead(true);
        }
        if (player.Companion.Type == CompanionType.R2D2)
        {
            //Jouer explosion
        }
        if (player.Companion.Type == CompanionType.Merlin)
        {
            player.Companion.Fight(true);
            player.Companion.Attack(true);
            player.Companion.Fire(true);

        }
    }
}
