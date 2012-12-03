using UnityEngine;
using System.Collections;
using System;

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
	
	//Variables de red
	private string ipConexion		= "127.0.0.1";
	private int puertoConexion		= 25000;
	public bool usarNAT				= false;
	private string ipLocal			= "";
	private string puertoLocal		= "";
	
	private string nombreJugador	= "Nombre";
	
	
	// Use this for initialization
	void Start () {
//		if (jugadorPrincipal) {
//			//Yo soy el jugador que tiene el server, el otro es host
//			Object jugador1 = Instantiate(jugador_main, spawnPoint1.position, spawnPoint1.rotation);
//			jugador1.name = "Jugador_master";
//			Object jugador2 = Instantiate(jugador_npc, spawnPoint2.position, spawnPoint2.rotation);
//			jugador2.name = "Jugador_host";
//		}
//		else {
//			//Yo soy el jugador host, el otro hace de server
//			Instantiate(jugador_main, spawnPoint2.position, spawnPoint2.rotation);
//			Instantiate(jugador_npc, spawnPoint1.position, spawnPoint1.rotation);
//		}
	}
	
	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width - 150, 10, 140, 200));
		if (Network.peerType == NetworkPeerType.Disconnected) {			
			if (GUILayout.Button("Conectar")) {
				Network.Connect(ipConexion, puertoConexion);	
			}
			if (GUILayout.Button("Lanza Servidor")) {
				Network.InitializeServer(32, puertoConexion, usarNAT);
				foreach (GameObject go in FindObjectsOfType(typeof(GameObject))) {
					go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
				}
			}
			nombreJugador = GUILayout.TextField(nombreJugador);
			ipConexion = GUILayout.TextField(ipConexion);
			puertoConexion = Convert.ToInt32(GUILayout.TextField(puertoConexion.ToString()));
			
		}
		else if (Network.peerType == NetworkPeerType.Connecting) {
			GUILayout.Label("Conexion: Conectando");
		}
		else if (Network.peerType == NetworkPeerType.Client) {
			GUILayout.Label("Conexion: Cliente");
			GUILayout.Label("Ping: " + Network.GetAveragePing(Network.connections[0]));
		}
		else if (Network.peerType == NetworkPeerType.Server) {
			GUILayout.Label("Conexion: Servidor");
			GUILayout.Label("Conexiones: " + Network.connections.Length);
			if (Network.connections.Length >= 1)
				GUILayout.Label("Ping: " + Network.GetAveragePing(Network.connections[0]));
		}
		
		if (GUILayout.Button("Desconectar")) {
			Network.Disconnect(200);
		}
		
		ipLocal = Network.player.ipAddress;
		puertoLocal = Network.player.port.ToString();
		GUILayout.Label("Ip: " + ipLocal + " : " + puertoLocal);
		GUILayout.EndArea();
		if (jugadorPrincipal) {
			GUI.Label(new Rect(10, 10, 150, 20), "Mis puntos: " + puntosJugador1);
			GUI.Label(new Rect(10, 40, 150, 20), "Puntos enemigo: " + puntosJugador2);
		}
		else {
			GUI.Label(new Rect(10, 10, 150, 20), "Mis puntos: " + puntosJugador2);
			GUI.Label(new Rect(10, 40, 150, 20), "Puntos enemigo: " + puntosJugador1);
		}
	}
	
	void OnServerInitialized() {
		//Yo soy el jugador que tiene el server, el otro es host
		UnityEngine.Object jugador1 = Network.Instantiate(jugador_main, spawnPoint1.position, spawnPoint1.rotation, 0);
		jugador1.name = nombreJugador;
	}
	
	void OnConnectedToServer() {
		//Algo que quedaba pendiente de hacer, señalizar a todos los objetos del inicio del nivel en red
		foreach (GameObject go in FindObjectsOfType(typeof(GameObject))) {
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
		//Yo soy el jugador cliente, el otro es server
		UnityEngine.Object jugador2 = Network.Instantiate(jugador_main, spawnPoint2.position, spawnPoint2.rotation, 0);
		jugador2.name = nombreJugador;
	}
	
	//Desde el server se desconecta un jugador que estaba conectado
	void OnPlayerDisconnected(NetworkPlayer player) {
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	
	//Siendo cliente te desconectas
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Network.RemoveRPCs(Network.player);
		Network.DestroyPlayerObjects(Network.player);
		Application.LoadLevel(Application.loadedLevelName);
	}
	
	
	
}
