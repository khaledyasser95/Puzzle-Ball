using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WrongWayScript : MonoBehaviour {


	
    
    // Use this for initialization
	void Awake () {
        GetComponent<MeshRenderer>().enabled = false;
	}
	
	void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Ball")
        {
            //Level 3
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}//class


















