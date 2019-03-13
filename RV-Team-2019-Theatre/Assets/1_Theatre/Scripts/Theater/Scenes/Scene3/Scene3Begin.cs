using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Begin : MonoBehaviour
{
    public GameObject[] agents;
         
    // Start is called before the first frame update
    private void OnEnable()
    {
        foreach (GameObject item in agents)
        {
            item.SetActive(false);
        }
    }
}
