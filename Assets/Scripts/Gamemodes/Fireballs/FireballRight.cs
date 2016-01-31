using UnityEngine;
using System.Collections;

public class FireballRight : MonoBehaviour {

    // Use this for initialization
    void Update()
    {
        transform.Translate(new Vector3(3 * Time.deltaTime, 0, 0));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            collider.gameObject.GetComponent<RawPlayerController>().kill();
        }
    }
}
