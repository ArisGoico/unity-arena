using UnityEngine;
using System.Collections;

public class Init_escena : MonoBehaviour {
	
	//Soy yo el server?
	public bool jugadorPrincipal	= true;
	
	//Los prefabs de los jugadores
	public Transform jugador_main;
	public Transform jugador_npc;
	
	//Los puntos de spawn
	public Transform spawnPoint1;
	public Transform spawnPoint2;
	
	//Los puntos de cada jugador
	private int puntosJugador1		= 0;
	private int puntosJugador2		= 0;
	
	// Use this for initialization
	void Start () {
		if (jugadorPrincipal) {
			//Yo soy el jugador que tiene el server, el otro es host
			Object jugador1 = Instantiate(jugador_main, spawnPoint1.position, spawnPoint1.rotation);
			jugador1.name = "Jugador_master";
			Object jugador2 = Instantiate(jugador_npc, spawnPoint2.position, spawnPoint2.rotation);
			jugador2.name = "Jugador_host";
		}
		else {
			//Yo soy el jugador host, el otro hace de server
			Instantiate(jugador_main, spawnPoint2.position, spawnPoint2.rotation);
			Instantiate(jugador_npc, spawnPoint1.position, spawnPoint1.rotation);
		}
	}
	
	void OnGUI() {
		if (jugadorPrincipal) {
			GUI.Label(new Rect(10, 10, 50, 20), "Mis puntos: " + puntosJugador1);
			GUI.Label(new Rect(70, 10, 50, 20), "Puntos enemigo: " + puntosJugador2);
		}
		else {
			GUI.Label(new Rect(10, 10, 50, 20), "Mis puntos: " + puntosJugador2);
			GUI.Label(new Rect(70, 10, 50, 20), "Puntos enemigo: " + puntosJugador1);
		}
	}
	
}
