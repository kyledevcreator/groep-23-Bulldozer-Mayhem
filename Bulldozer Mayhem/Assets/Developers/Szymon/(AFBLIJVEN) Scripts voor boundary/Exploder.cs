using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Exploder : MonoBehaviour
{
    public VisualEffect Explosion;

    private void Start()
    {
        Explosion.playRate = 3;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explosion.Play();
        }
    }
  
}
