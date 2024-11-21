using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private float minDelay, maxDelay;

    public Material SecondMaterial;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GravityDelay());
    }

    private IEnumerator GravityDelay()
    {
        while (platforms.Count > 0)
        {
            float randomWaitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomWaitTime);
            PlatformFall();
        }
    }


    private void PlatformFall()
    {
        int i = Random.Range(0, platforms.Count);
        Debug.Log(i);

        if (platforms[i].activeInHierarchy)
        {
            StartCoroutine(ChangeColorAndFall(platforms[i]));
        }
        else if (platforms[i] == null)
        {
            // Retry with a different platform if the selected one is null
            i = Random.Range(0, platforms.Count);
            PlatformFall();
        }
    }

    private IEnumerator ChangeColorAndFall(GameObject platform)
    {
        // Change the material color
        Renderer renderer = platform.GetComponent<Renderer>();
        if (renderer != null && SecondMaterial != null)
        {
            renderer.material = SecondMaterial;
        }

        // Wait for a short time before making the platform fall
        yield return new WaitForSeconds(3.0f); // Adjust the delay as needed

        // Enable gravity and make it fall
        Rigidbody rigidBody = platform.GetComponent<Rigidbody>();
        Collider collider = platform.GetComponent<Collider>();
        if (rigidBody != null)
        {
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            collider.enabled = false;
            rigidBody.drag = 0.2f;
        }

        // Destroy the platform after 5 seconds and remove it from the list
        Destroy(platform, 5);
        platforms.Remove(platform);
    }
}
