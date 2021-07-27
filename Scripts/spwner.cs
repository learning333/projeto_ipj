using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class spwner : MonoBehaviour
{
	public SpriteRenderer testSprite;
	//transform os spawning tower root object
	public Transform spawnTowerRoot;
	//lista de torres prefabs
	public List<GameObject> towersPrefabs;
	public List<Image> towersUI;
	//id of tower to spawn
	int spawnID=-1;
	
	//spaw tilemap
	public Tilemap spawnTilemap;
	
	void Update(){
		if(Canspawn()){
			
			DetectSpawnPoint();
		}
	}
	
	bool Canspawn(){
		
		if(spawnID==-1){
			return false;
		}else{
			return true;
			
		}
	}
	
	void DetectSpawnPoint(){
		//detect mouse click
		if(Input.GetMouseButtonDown(0)){
			//get pos of mouse
			var mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//get pos of cell in tilemap
			var cellPosDefault=spawnTilemap.WorldToCell(mousePos);
			
			var cel2=spawnTilemap.LocalToWorld(mousePos);
			Debug.Log(cel2);
			//get center position
			var cellPosCentered=spawnTilemap.GetCellCenterWorld(cellPosDefault);
			
			//testSprite.transform.position=cellPosCentered;
			
			//check if lugar é spawnavel
			Debug.Log("GET:"+spawnTilemap.GetColliderType(cellPosDefault)+"EXPECTED:"+Tile.ColliderType.Sprite);
			if(spawnTilemap.GetColliderType(cellPosDefault)==Tile.ColliderType.Sprite){
				Debug.Log("chegou");
				int towercost=TowerCost();//towersPrefabs[spawnID].GetComponent<tower>().cost;
				
				//checar se tem mueda suficiente
				if(GameManager.instance.currency.enoughCurrency(TowerCost())){
					//Debug.Log("towercost="+towercost);
					GameManager.instance.currency.Use(towercost);
					//spawn
					Debug.Log("placeando tower");
					SpawnTower(cellPosCentered);
					//Disable the collider?
					//spawnTilemap.SetColliderType(cellPosDefault,Tile.ColliderType.None);
					// test enable sprite
					//testSprite.enabled=true;
				}else{
					//Debug.Log("sem mueda");
				}
			}else{
				Debug.Log("nao chegou");
				//Disable test sprite
				//testSprite.enabled=false;
				//enable collider
				//spawnTilemap.SetColliderType(cellPosDefault,Tile.ColliderType.Sprite);
				
			}
			//Debug.Log(cellPosDefault);		
			
		}
		
	}
		
	int TowerCost(){
		switch(spawnID){
			//case 1 female zombie
			case 1: return towersPrefabs[1].GetComponent<female_zombie_income>().cost;
			//case 1: return towersPrefabs[0].GetComponent<tower_pinka>().cost;
			case 0: return towersPrefabs[0].GetComponent<player>().cost;
			default:return -1;
		}
	}		
	void SpawnTower(Vector3 position){
		GameObject tower=Instantiate(towersPrefabs[spawnID],spawnTowerRoot);
		tower.transform.position=position;
		
		//tower.GetComponent<tower>().Init();
		
		DeselectTower();

		//EnemySpawner.instance.StartSpawning();
	}
	
	
	public void SelectTower(int id){
		//Debug.Log(id);
		DeselectTower();
		spawnID=id;
		
		//highlight
		towersUI[spawnID].color=Color.white;
		
	}
	
	public void DeselectTower(){
		spawnID=-1;
		foreach(var t in towersUI){
			t.color=new Color(0.5f,0.5f,0.5f);
		}
	}
	
	
	
	
}
