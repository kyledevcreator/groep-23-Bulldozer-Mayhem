using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deathboundaryalleenmijnscene : MonoBehaviour
{
    // destroys other gameobject
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
            //  Destroy(other.gameObject);
        }
    }

}
