using UnityEngine;
using System.Collections;

public class SideScrollController : MonoBehaviour {

    bool firstLaunch = true;
    // Use this for initialization
    void OnEnable()
    {
        if (firstLaunch)
        {
            firstLaunch = false;
            return;
        }

        Invoke("MakeScroll", 3);
    }

    void Start () {
        

    }

    // Update is called once per frame
    void Update () {

	
	}

    void MakeScroll()
    {
        SideScrollCamera.isScrolling = true;
    }
}
