using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_effect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 4f);
	}
	
	
}
