using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    public bool IsSelected = false;
    
    void Start()
    {
        smokeEffect.Play();
    }

    private void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            //Debug.Log(gameObject.name + " is on collision with " + coll.gameObject.name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                select();
            }
        }
    }

    private void select()
    {
        IsSelected = true;
    }

    public void Spawn()
    {
        Debug.Log("Cards are spawned");
        Invoke("SetActiveCard", 0.3f);

    }
    public void SetActiveCard()
    {
        gameObject.SetActive(true);
    }
}
