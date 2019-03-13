using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCurtains : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.Instance.CloseCurtains();
    }
}
