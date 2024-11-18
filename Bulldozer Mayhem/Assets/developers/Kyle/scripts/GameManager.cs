using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private float minDelay, maxDelay;
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
            platforms[i].GetComponent<Rigidbody>().useGravity = true;
            Destroy(platforms[i], 5);
            platforms.Remove(platforms[i]);
        }
        else if (platforms[i] == null)
        {
            i = Random.Range(0, platforms.Count);
            PlatformFall();
        }


    }
}
