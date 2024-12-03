using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingplayersounds : MonoBehaviour
{
    public AudioSource Playermovingsound;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.D)))))
        {
            Playermovingsound.enabled = true;
        }
        else
        {
            Playermovingsound.enabled = false;
        }
    }
}
