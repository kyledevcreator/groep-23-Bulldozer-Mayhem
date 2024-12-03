using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class sparks : MonoBehaviour
{
    [SerializeField] private VisualEffect sparksVFX;
    

    // Start is called before the first frame update

    void Start()
    {
        sparksVFX.Stop();

    }
    IEnumerator ExplosionTime()
    {
        yield return new WaitForSeconds(1f);
        //Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ExplosionTime());
            Explosion();
        }

    }

    public void Explosion()
    {
        sparksVFX.Play();
    }
}
