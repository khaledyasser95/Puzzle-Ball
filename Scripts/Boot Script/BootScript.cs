﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootScript : MonoBehaviour {

    public float force = 150f;

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Ball")
        {
            target.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * -force,ForceMode.Impulse);
        }

    }
}//class


























