using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {


	public PlayerController player;





	void Start () {


		player = FindObjectOfType<PlayerController> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}




	public void makePlayerJump(){

		// player = FindObjectOfType<playerController> ();

		player.jump ();




	}


}
