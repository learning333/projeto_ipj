using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class spwner : MonoBehaviour
{
	public List<Transform> points;
	
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
			//Debug.Log(cel2);
			//get center position
			var cellPosCentered=spawnTilemap.GetCellCenterWorld(cellPosDefault);
			
			//testSprite.transform.position=cellPosCentered;
			
			//check if lugar é spawnavel
			//Debug.Log("GET:"+spawnTilemap.GetColliderType(cellPosDefault)+"EXPECTED:"+Tile.ColliderType.Sprite);
			if(spawnTilemap.GetColliderType(cellPosDefault)==Tile.ColliderType.Sprite){

				int towercost=TowerCost();//towersPrefabs[spawnID].GetComponent<tower>().cost;
				
				//checar se tem moeda suficiente
				if(GameManager.instance.currency.enoughCurrency(TowerCost()) && randomEvents.pausado==false){
					//Debug.Log("towercost="+towercost);
					GameManager.instance.currency.Use(towercost);
					//spawn
					SpawnTower(cellPosCentered);
					enemiespawn.contaSpawnGlobal++;
				}
			}			
			
		}
		
	}
		
	int TowerCost(){
		switch(spawnID){
			//case 1 female zombie
			case 1: return towersPrefabs[1].GetComponent<female_zombie_income>().cost;
			
			case 0: return towersPrefabs[0].GetComponent<player>().cost;
			default:return -1;
		}
	}		
	void SpawnTower(Vector3 position){
		//GameObject tower=Instantiate(towersPrefabs[spawnID],spawnTowerRoot);
		float cordy=position.y-1.5f;
		int cordy2=(int)cordy;
		//Debug.Log(cordy2);
		GameObject tower=Instantiate(towersPrefabs[spawnID],points[cordy2]);
		tower.transform.position=position;
		

		
		DeselectTower();


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
