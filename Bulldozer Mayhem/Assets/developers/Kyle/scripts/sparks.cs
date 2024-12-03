using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class sparks : MonoBehaviour
{
    [SerializeField] private VisualEffect sparksVFX;
    


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sparksVFX.Play();
        }
    }


}
