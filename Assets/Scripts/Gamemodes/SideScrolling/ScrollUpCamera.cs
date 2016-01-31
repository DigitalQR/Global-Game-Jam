using UnityEngine;
using System.Collections;

public class ScrollUpCamera : MonoBehaviour {
    public static bool isScrolling = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isScrolling = true && GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            transform.Translate(new Vector3(0, 1.8f* Time.deltaTime, 0));
        }

    }
}
