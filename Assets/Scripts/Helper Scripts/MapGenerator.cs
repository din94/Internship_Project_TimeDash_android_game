using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public static MapGenerator instance;

	public GameObject
	roadPrefab,
	grass_Prefab,
	officePrefab_3,
	officePrefab_9,
	groundPrefab_3,
	officePrefab_4,
	grass_Bottom_Prefab,
	land_Prefab_1,
	land_Prefab_2,
	land_Prefab_3,
	land_Prefab_4,
	land_Prefab_5,
	big_Grass_Prefab,
	big_Grass_Bottom_Prefab,
	officePrefab_5,
	officePrefab_6,
	officePrefab_7,
	officePrefab_2;


	public GameObject
	road_Holder,
	top_Near_Side_Walk_Holder,
	top_Far_Side_Walk_Holder,
	bottom_Near_Side_Walk_Holder,
	bottom_Far_Side_Walk_Holder;


	public int
	start_Road_Tile,        // initialization number of 'road' tiles
	start_Grass_Tile,       // initialization number of 'grass' tiles
	start_Ground3_Tile,     // initialization number of 'ground3' tiles
	start_Land_Tile;        // initialization number of 'land' tiles


	public List<GameObject>
		road_Tiles,
		top_Near_Grass_Tiles,
		top_Far_Grass_Tiles,
		bottom_Near_Grass_Tiles,
		bottom_Far_Land_F1_Tiles,
		bottom_Far_Land_F2_Tiles,
		bottom_Far_Land_F3_Tiles,
		bottom_Far_Land_F4_Tiles,
		bottom_Far_Land_F5_Tiles;

	 

	//	positions for office_building3 on top from 0 to startGround3Tile
	public int[] pos_For_Top_Office_3;



	// positions for office9 on top from 0 to startGround3Tile
	public int[] pos_For_Top_Office_9;


	// positions for office4 on top from 0 to startGround3Tile
	public int[] pos_For_Top_Office_4;


	// positions for big grass with tree on top near grass from 0 to startGrassTile
	public int[] pos_For_Top_Big_Grass;


	// positions for office5 on top near grass from 0 to startGrassTile
	public int[] pos_For_Top_Office_5;

	// positions for office6 on top near grass from 0 to startGrassTile
	public int[] pos_For_Top_Office_6;

	// postions for office7 on top near grass from 0 to startGrassTile
	public int[] pos_For_Top_Office_7;


	 // FROM HERE

	// position for road tile on road from 0 to startRoadTile
	public int pos_For_Road_Tile_1;

	// position for road tile on road from 0 to startRoadTile
	public int pos_For_Road_Tile_2;

	// position for road tile on road from  0 to startRoadTile
	public int pos_For_Road_Tile_3;


	// position for big grass with tree on bottom near grass from 0 to startGrassTile
	public int[] pos_For_Bottom_Big_Grass;


	// position for office5 on bottom near grass from 0 to startGrassTile
	public int[] pos_For_Bottom_Office5;


	// position for office6 on bottom near grass from 0 to startGrassTile
	public int[] pos_For_Bottom_Office6;


	// position for office7 on bottom near grass from 0 to startGrassTile
	public int[] pos_For_Bottom_Office7;



	[HideInInspector]
	public Vector3
		last_Pos_Of_Road_Tile,
		last_Pos_Of_Top_Near_Grass,
		last_Pos_Of_Top_Far_Grass,
		last_Pos_Of_Bottom_Near_Grass,
		last_Pos_Of_Bottom_Far_Land_F1,
		last_Pos_Of_Bottom_Far_Land_F2,
		last_Pos_Of_Bottom_Far_Land_F3,
		last_Pos_Of_Bottom_Far_Land_F4,
		last_Pos_Of_Bottom_Far_Land_F5;



	[HideInInspector]
	public int
	last_Order_Of_Road,
	last_Order_Of_Top_Near_Grass,
	last_Order_Of_Top_Far_Grass,
	last_Order_Of_Bottom_Near_Grass,
	last_Order_Of_Bottom_Far_Land_F1,
	last_Order_Of_Bottom_Far_Land_F2,
	last_Order_Of_Bottom_Far_Land_F3,
	last_Order_Of_Bottom_Far_Land_F4,
	last_Order_Of_Bottom_Far_Land_F5;







	void Awake(){
		MakeInstance ();
	}


	void Start () {
		Initialize ();
	}
	

	void MakeInstance () {
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
	}


	void Initialize() {

		InitializePlatform (roadPrefab, ref last_Pos_Of_Road_Tile, roadPrefab.transform.position,
			start_Road_Tile, road_Holder, ref road_Tiles, ref last_Order_Of_Road,
			new Vector3(1.5f, 0f, 0f));


		InitializePlatform (grass_Prefab, ref last_Pos_Of_Top_Near_Grass, grass_Prefab.transform.position,
			start_Grass_Tile, top_Near_Side_Walk_Holder, ref top_Near_Grass_Tiles,
			ref last_Order_Of_Top_Near_Grass, new Vector3(1.2f, 0f, 0f));


		InitializePlatform(groundPrefab_3, ref last_Pos_Of_Top_Far_Grass, groundPrefab_3.transform.position,
			start_Ground3_Tile, top_Far_Side_Walk_Holder, ref top_Far_Grass_Tiles, ref last_Order_Of_Top_Far_Grass,new Vector3(4.8f,0f,0f));

		InitializePlatform (grass_Bottom_Prefab, ref last_Pos_Of_Bottom_Near_Grass, new Vector3 (2.0f, grass_Bottom_Prefab.transform.position.y, 0f),
			start_Grass_Tile, bottom_Near_Side_Walk_Holder, ref bottom_Near_Grass_Tiles,
			ref last_Order_Of_Bottom_Near_Grass, new Vector3 (1.2f, 0f, 0f)); 


		InitializeBottomFarLand ();
		    


	}  // Initialize 



	void InitializePlatform(GameObject prefab, ref Vector3 last_Pos, Vector3 last_Pos_Of_Tile,
		int amountTile, GameObject holder, ref List<GameObject> list_Tile, ref int last_Order, Vector3 offset) {


		int orderInLayer = 0;
		last_Pos = last_Pos_Of_Tile;

		for (int i = 0; i < amountTile; i++) {

			GameObject clone = Instantiate (prefab, last_Pos, prefab.transform.rotation) as GameObject;

			clone.GetComponent<SpriteRenderer> ().sortingOrder = orderInLayer;


			if (clone.tag == MyTags.TOP_NEAR_GRASS) {

				SetNearScene (big_Grass_Prefab, ref clone, ref orderInLayer, pos_For_Top_Big_Grass, pos_For_Top_Office_5, pos_For_Top_Office_6, pos_For_Top_Office_7);
					

			} else if (clone.tag == MyTags.BOTTOM_NEAR_GRASS) {

				SetNearScene (big_Grass_Bottom_Prefab, ref clone, ref orderInLayer,
					pos_For_Bottom_Big_Grass, pos_For_Bottom_Office5, pos_For_Bottom_Office6, pos_For_Bottom_Office7);




			} else if (clone.tag == MyTags.BOTTOM_FAR_LAND_2) {


				if (orderInLayer == 5) {

					CreateTreeOrGround (officePrefab_2, ref clone, new Vector3 (-0.57f, -1.34f, 0f));



				}

			} else if (clone.tag == MyTags.TOP_FAR_GRASS) {

				CreateGround (ref clone, ref orderInLayer);


			}
				 

			clone.transform.SetParent (holder.transform);
			list_Tile.Add (clone);


			orderInLayer += 1;
			last_Order = orderInLayer;

			last_Pos += offset;




		}  // FOR LOOP


	}   // InitializePlatform
	 
	void CreateScene(GameObject bigGrassPrefab, ref GameObject tileClone, int orderInLayer) {


		GameObject clone = Instantiate (big_Grass_Prefab, tileClone.transform.position,
			big_Grass_Prefab.transform.rotation) as GameObject;



		clone.GetComponent<SpriteRenderer> ().sortingOrder = orderInLayer;
		clone.transform.SetParent (tileClone.transform); 
		clone.transform.localPosition = new Vector3 (-0.183f, 0.106f, 0f);


		CreateTreeOrGround (officePrefab_5, ref clone, new Vector3 (0f, 1.52f, 0f));



		// Turn off parent tile to show child tile
		tileClone.GetComponent<SpriteRenderer> ().enabled = false;


	}   // Create Scene


	void CreateTreeOrGround(GameObject prefab, ref GameObject tileClone, Vector3 localPos) {


		GameObject clone = Instantiate (prefab, tileClone.transform.position, prefab.transform.rotation)
			as GameObject;


		SpriteRenderer tileCloneRenderer = tileClone.GetComponent<SpriteRenderer> ();
		SpriteRenderer cloneRenderer = clone.GetComponent<SpriteRenderer> ();

		cloneRenderer.sortingOrder = tileCloneRenderer.sortingOrder;

		clone.transform.SetParent (tileClone.transform);


		clone.transform.localPosition = localPos;
		  

		if (prefab == officePrefab_3 || prefab == officePrefab_9 || prefab == officePrefab_4) {

			tileCloneRenderer.enabled = false;


		}



	}   // CreateTreeOrGround



	void CreateGround(ref GameObject clone, ref int orderInLayer) {

		for (int i = 0; i < pos_For_Top_Office_3.Length; i++) {

			if (orderInLayer == pos_For_Top_Office_3 [i]) {

				CreateTreeOrGround (officePrefab_3, ref clone, Vector3.zero);
				break;


				 

			}

		}



		for (int i = 0; i < pos_For_Top_Office_9.Length; i++) {

			if (orderInLayer == pos_For_Top_Office_9 [i]) {

				CreateTreeOrGround (officePrefab_9, ref clone, Vector3.zero);
				break;




			}

		}




		for (int i = 0; i < pos_For_Top_Office_4.Length; i++) {

			if (orderInLayer == pos_For_Top_Office_4 [i]) {

				CreateTreeOrGround (officePrefab_4, ref clone, Vector3.zero);
				break;




			}

		}


	}  // Create Office

	void SetNearScene(GameObject bigGrassPrefab, ref GameObject clone, ref int orderInLayer,
		int[] pos_For_BigGrass, int[] pos_For_Office5, int[] pos_For_Office6, int[] pos_For_Office7)  {

		for (int i = 0; i < pos_For_BigGrass.Length; i++) {

			if(orderInLayer == pos_For_BigGrass[i]) {
				CreateScene (big_Grass_Prefab, ref clone, orderInLayer);
				break;

			}

		}



				for(int i = 0; i < pos_For_Office5.Length; i++) {
			if(orderInLayer == pos_For_Office5[i]) {

						CreateTreeOrGround(officePrefab_5, ref clone, new Vector3(0f, 1.15f, 0f));
						break;


					}

				}



						for(int i = 0; i < pos_For_Office6.Length; i++) {
			if(orderInLayer == pos_For_Office6[i]) {

								CreateTreeOrGround(officePrefab_6, ref clone, new Vector3(0f, 1.15f, 0f));
								break;


							}

								}




								for(int i = 0; i < pos_For_Office7.Length; i++) {
			if(orderInLayer == pos_For_Office7[i]) {

										CreateTreeOrGround(officePrefab_7, ref clone, new Vector3(0f, 1.15f, 0f));
										break;


									}

										}





	}  // SetNearScene


	void InitializeBottomFarLand(){

		InitializePlatform (land_Prefab_1, ref last_Pos_Of_Bottom_Far_Land_F1, land_Prefab_1.transform.position,
			start_Land_Tile, bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F1_Tiles,
			ref last_Order_Of_Bottom_Far_Land_F1, new Vector3(1.6f, 0f, 0f));


		InitializePlatform (land_Prefab_2, ref last_Pos_Of_Bottom_Far_Land_F2, land_Prefab_2.transform.position,
			start_Land_Tile - 3, bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F2_Tiles,
			ref last_Order_Of_Bottom_Far_Land_F2, new Vector3(1.6f, 0f, 0f));



		InitializePlatform (land_Prefab_3, ref last_Pos_Of_Bottom_Far_Land_F3, land_Prefab_3.transform.position,
			start_Land_Tile - 4, bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F3_Tiles,
			ref last_Order_Of_Bottom_Far_Land_F3, new Vector3(1.6f, 0f, 0f));



		InitializePlatform (land_Prefab_4, ref last_Pos_Of_Bottom_Far_Land_F4, land_Prefab_4.transform.position,
			start_Land_Tile - 7, bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F4_Tiles,
			ref last_Order_Of_Bottom_Far_Land_F4, new Vector3(1.6f, 0f, 0f));



		InitializePlatform (land_Prefab_5, ref last_Pos_Of_Bottom_Far_Land_F5, land_Prefab_5.transform.position,
			start_Land_Tile - 10, bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F5_Tiles,
			ref last_Order_Of_Bottom_Far_Land_F5, new Vector3(1.6f, 0f, 0f));
		



	}  // InitializeBottomFarLand


}   //class




























