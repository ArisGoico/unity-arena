using UnityEngine;
using System.Collections;

public class Controles : MonoBehaviour {

	public Transform terreno;
	public Transform espada;
	
//	private float velocidad		= 5.0f;
	
	void Start() {
		if (!this.networkView.isMine) {
			this.enabled = false;
		}
		terreno = GameObject.FindGameObjectWithTag("terreno").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (this.networkView.isMine) {
//			Vector3 direccion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//			this.transform.Translate(velocidad * direccion * Time.deltaTime);
			
			if (Input.GetMouseButtonDown(0)) {
				espada.animation.Play("Golpe");
			}
		}
		
	}
	
	void OnTriggerEnter(Collider other) {
//		Debug.Log("Impacto con " + other.name + "!!");
		if (other.transform.IsChildOf(this.transform) || other.transform == terreno)
		    return;
        Debug.Log("El objeto " + this.transform.parent.name + " ha impactado el objeto " + other.name + "!!");
    }
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		
		if (stream.isWriting) {
			Vector3 pos = this.transform.position;
			Quaternion rot = this.transform.rotation;
			stream.Serialize(ref pos);
			stream.Serialize(ref rot);
		}
		else {
			Vector3 pos = Vector3.zero;
			Quaternion rot = Quaternion.identity;
			stream.Serialize(ref pos);
			stream.Serialize(ref rot);
			this.transform.position = pos;
			this.transform.rotation = rot;
		}
		
	}
}
