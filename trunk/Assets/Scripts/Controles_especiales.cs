using UnityEngine;
using System.Collections;

public class Controles_especiales : MonoBehaviour {

	public Transform terreno;
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(0)) {
			this.animation.Play("Golpe");
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.transform.IsChildOf(this.transform.parent) || other.transform == terreno)
		    return;
        Debug.Log("Has impactado el objeto " + other.name + "!!");
    }
}
