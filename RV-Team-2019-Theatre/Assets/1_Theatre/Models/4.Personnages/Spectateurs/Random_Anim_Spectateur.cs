using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Anim_Spectateur : MonoBehaviour
{
    [SerializeField] private int randomIdAnimation_Applause;

    // Start is called before the first frame update
    void Start()
    {
        randomIdAnimation_Applause = Random.Range(0, 3);

        GetComponent<Animator>().SetInteger("RandomIdAnimApplause", randomIdAnimation_Applause);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
