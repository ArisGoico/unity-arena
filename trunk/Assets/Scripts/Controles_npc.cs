using UnityEngine;
using System.Collections;

public class Controles_npc : MonoBehaviour {

	public Transform terreno;
	public bool playAnim		= false;
	
	// Update is called once per frame
	void Update () {
	
		if (playAnim) {
			playAnim = false;
			this.animation.Play("Golpe");
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.transform.IsChildOf(this.transform.parent) || other.transform == terreno)
		    return;
        Debug.Log("El objeto " + this.transform.parent.name + " ha impactado el objeto " + other.name + "!!");
    }
}
