using UnityEngine;
using System.Collections;

public class FireControl : MonoBehaviour {

    public GameObject fireball;

    bool firstLaunch = true;

    // Use this for initialization
    void OnEnable()
    {
        if (firstLaunch)
        {
            firstLaunch = false;
            return;
        }

        Invoke("SpawnFireball", frequency-1);
    }

    float frequency = 3;

    void SpawnFireball() {
        Instantiate(fireball, new Vector3(Random.Range(11, 13), -2, 0), Quaternion.identity);

        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Invoke("SpawnFireball", frequency *= 0.95f);
        }
    }
}
