using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemiespawn : MonoBehaviour
{
	public List<GameObject> prefabs;
	public List<Transform> points;
	public float spawnInterval;
	
	
	public void StartSpawning(){
		StartCoroutine(SpawnDelay());
	}
	
	IEnumerator SpawnDelay(){
		SpawnEnemy();
		yield return new WaitForSeconds(spawnInterval);
		StartCoroutine(SpawnDelay());
	}
	void SpawnEnemy(){
		//randomize genero
		int randomPrefabID=Random.Range(0,prefabs.Count);
		//randomize spawnpoint
		int randomSpawnPointID=Random.Range(0,points.Count);
		
		Debug.Log("INSTANCIANDO EM :"+randomSpawnPointID);
		GameObject spawnedEnemy=Instantiate(prefabs[randomPrefabID],points[randomSpawnPointID]);
	}
	
}
