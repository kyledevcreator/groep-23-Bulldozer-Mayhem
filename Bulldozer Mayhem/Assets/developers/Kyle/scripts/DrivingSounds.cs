using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingSounds : MonoBehaviour
{

    public AudioSource TrackSound;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.D) &&
            (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.RightArrow)))))))))
        {
            TrackSound.enabled = true;
        }
        else
        {
            TrackSound.enabled = false;
        }
    }
}
