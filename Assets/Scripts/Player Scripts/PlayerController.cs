
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {




	Rigidbody2D rig;

	public float jumpHeight;



	public void jump(){




		rig.velocity = new Vector2 (0, jumpHeight);






	}




















	public static PlayerController instance;


	private Animator anim;


	private string jump_Animation = "PlayerJump", change_Line_Animation = "ChangeLine";


	public GameObject
		player,
		shadow;


	public Vector3
	first_PosOfPlayer,
	second_PosOfPlayer;



	[HideInInspector]
	public bool player_Died;


	[HideInInspector]
	public bool player_Jumped;


	public GameObject explosion;


	private SpriteRenderer player_Renderer;
	public Sprite player_Sprite;





	private GameObject[] start_Effect;

	private bool TRex_Trigger;



	void Awake()  {

		MakeInstance ();


		anim = player.GetComponent<Animator> ();

		player_Renderer = player.GetComponent<SpriteRenderer> ();


		start_Effect = GameObject.FindGameObjectsWithTag (MyTags.STAR_EFFECT);


	}

	void Start () {

		string path = "Sprites/Player/hero" + GameManager.instance.selected_Index + "_big";
		player_Sprite = Resources.Load<Sprite> (path);
		player_Renderer.sprite = player_Sprite;

		rig = GetComponent<Rigidbody2D> ();

	
	
	}


	

	void Update () {
		HandleChangeLine ();
		HandleJump ();



	}



	void MakeInstance() { 
		if (instance == null) {
			instance = this;

		} else if (instance != null) { 
			Destroy (gameObject);

		}
	
	}



	void HandleChangeLine() {

		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) {

			anim.Play (change_Line_Animation);
			transform.localPosition = second_PosOfPlayer;


			SoundManager.instance.PlayMoveLineSound ();





		} else if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) {

			anim.Play (change_Line_Animation);
			transform.localPosition = first_PosOfPlayer;


			SoundManager.instance.PlayMoveLineSound ();



		}


	}


	void HandleJump() { 
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!player_Jumped) {


				anim.Play (jump_Animation);
				player_Jumped = true;
				SoundManager.instance.PlayJumpSound ();

			}

		}
	
	}



	void Die() {   
	
		player_Died = true;
	
		player.SetActive (false);

		shadow.SetActive (false);

		GameplayController.instance.moveSpeed = 0f;
		GameplayController.instance.GameOver();

		SoundManager.instance.PlayDeadSound ();
		SoundManager.instance.PlayGameOverClip ();


		// PLAY SOUND GAME OVER

	}


	void DieWithObstacle(Collider2D target)   {
	
		Die ();

		explosion.transform.position = target.transform.position;

		explosion.SetActive (true);

		target.gameObject.SetActive (false);


		SoundManager.instance.PlayDeadSound ();






	
	}




	void DestroyObstacle(Collider2D target) {

		explosion.transform.position = target.transform.position;

		explosion.SetActive (false);  //turn off the explosion if its already turned on

		explosion.SetActive (true);



		target.gameObject.SetActive (false);


		SoundManager.instance.PlayDeadSound ();




	}




	void OnTriggerEnter2D(Collider2D target) {


		if (target.tag == MyTags.OBSTACLE) {
		
			if (!TRex_Trigger) {
			
				DieWithObstacle (target);
			} else {


				DestroyObstacle (target);
			
			}



		
		
		}






		if (target.tag == MyTags.STAR) { 


			for (int i = 0; i < start_Effect.Length; i++) {
			
				if (!start_Effect [i].activeInHierarchy) {
				
					start_Effect [i].transform.position = target.transform.position;

					start_Effect [i].SetActive (true);
					break;


				
				}
			
			}



			target.gameObject.SetActive (false);
			SoundManager.instance.PlayCoinSound ();



			GameplayController.instance.UpdateStarScore ();




		}





	}


}   //class























































