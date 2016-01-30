using UnityEngine;
using System.Collections;

public class SideScrollCamera : MonoBehaviour {
    public bool isScrolling = false;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (isScrolling = true && GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
        }

    }
}
