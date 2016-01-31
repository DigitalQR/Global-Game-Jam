using UnityEngine;
using System.Collections;

public class TwoSideCameraFireControl : MonoBehaviour {

    public GameObject fireballleft;
    public GameObject fireballright;

    bool firstLaunch = true;
    float frequency = 1.1f;

    // Use this for initialization
    void OnEnable()
    {
        if (firstLaunch)
        {
            firstLaunch = false;
            return;
        }

        Invoke("SpawnFireballRandom", frequency - 1);
    }

    

    void SpawnFireballRandom()
    {
        if (Random.Range(0,2)==0)
        {
            Instantiate(fireballright, Camera.main.transform.position + new Vector3(-10, Random.Range(0, 6), 10), Quaternion.identity);
        }
        else
        {
            Instantiate(fireballleft, Camera.main.transform.position + new Vector3(10, Random.Range(0, 6), 10), Quaternion.identity);
        }

        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Invoke("SpawnFireballRandom", frequency *= 0.95f);
        }
    }
}
