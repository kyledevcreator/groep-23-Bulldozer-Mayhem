using UnityEngine;
using UnityEngine.VFX;

public class Exploder : MonoBehaviour
{
    public VisualEffect Explosion;

    private void Start()
    {
        Explosion.playRate = 3;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Front") || other.gameObject.CompareTag("Back") || other.gameObject.CompareTag("Left") || other.gameObject.CompareTag("Right"))
        {
            Explosion.gameObject.transform.position = transform.position;
            Explosion.Play();
        }
    }

}
