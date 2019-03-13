using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCurtains : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.Instance.OpenCurtains();
    }
}
