using UnityEngine;
using System.Collections;

public class Controles_main : MonoBehaviour {

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
        Debug.Log("El objeto " + this.transform.parent.name + " ha impactado el objeto " + other.name + "!!");
    }
}
