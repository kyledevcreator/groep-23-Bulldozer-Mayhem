using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerText : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = parent.transform.position;
    }
}
