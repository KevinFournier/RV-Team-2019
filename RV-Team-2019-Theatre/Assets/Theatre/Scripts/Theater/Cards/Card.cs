using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private static bool needCardSelection = false;

    [SerializeField] private CardManager cardManager;

    public bool IsSelected = false;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Time to select a card");
            needCardSelection = true;
        }

        if (needCardSelection)
            fall();
    }
    
    private void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.name + " is on collision with " + coll.gameObject.name);

            //Remplace by trigger HTC controllers
            if (Input.GetKeyDown(KeyCode.Return))
            {
                select();
            }
        }
    }
    #endregion

    private void fall()
    {
        Debug.Log("Cards are coming");
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        needCardSelection = false;
    }

    private void select()
    {
        Debug.Log("Player is clicking");
        IsSelected = true;
        cardManager.PullCards();
    }

    public void Pull()
    {
        Debug.Log("Cards are pulled");
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
}
