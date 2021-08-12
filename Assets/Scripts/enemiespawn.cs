using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 using UnityEngine.UI;

public class enemiespawn : MonoBehaviour
{
	public List<GameObject> prefabs;
	public List<Transform> points;
	public List<Transform> pointsPlayer;
	public float spawnInterval;
	public static int mf;
	public static int contaSpawnGlobal=0;
	public int contaSpawn=0;
	public int contaOnda=0;
	public Text labelondas;
	
	public int filtroSpawnReforco;
	public int filtroSpawnDuplo;
	
	public void StartSpawning(){
		
		StartCoroutine(SpawnDelay());
	}
	
	IEnumerator SpawnDelay(){
		if(menu.modo==0){//modo de jogo normal
			SpawnEnemy();
		}else{//modo ondas
			if(contaOnda==0){//se e primeira chamada, seta spawnInterval maior
				spawnInterval=20;
			}
			SpawnEnemyOndas();
		}
		yield return new WaitForSeconds(spawnInterval);
		StartCoroutine(SpawnDelay());
	}


	void SpawnEnemy(){
		bool counter=false;
		int j=2;
		
		for (int i = 0; i < 3; ++i){
			if(pointsPlayer[j].childCount>0 && points[i].childCount<pointsPlayer[j].childCount){//se o jogador colocou unidade ou esta em vantagem na linha
				Debug.Log("i: "+i+" j: "+j);
				int chancereforco=Random.Range(0,11);
				int chanceduplo=Random.Range(0,11);
				if(randomEvents.pausado==false){
					StartCoroutine(spawnduplo(i,chancereforco,chanceduplo));
				}
				counter=true;
			}
			j--;
		}			
		if(counter==false){
			int randomSpawnPointID=Random.Range(0,points.Count);
			if(randomEvents.pausado==false){
				GameObject spawnedEnemy=Instantiate(prefabs[0],points[randomSpawnPointID]);
			}
		}
		contaSpawn++;
		contaSpawnGlobal++;
		//Debug.Log(contaSpawnGlobal);
		if(contaSpawnGlobal%5==0){//ativado a cada multiplo de 5
			if (spawnInterval>5){
				spawnInterval--;//reduz spawnInterval a cada 5 spawnEnemy()
			}
		}
		
	}
	IEnumerator spawnduplo(int i, int chance,int chanceduplo){
				if(contaSpawn%10==0){//ativado a cada multiplo de 10
					if(filtroSpawnDuplo>1){
						filtroSpawnDuplo--;
					}if(filtroSpawnReforco>1){
						filtroSpawnReforco--;
					}
				}
				if(chance>filtroSpawnReforco){
					GameObject spawnedEnemy=Instantiate(prefabs[0],points[i]);
				}
				yield return new WaitForSeconds(spawnInterval/2);
				
				if(chanceduplo>filtroSpawnDuplo){
					GameObject spawnedEnemy2=Instantiate(prefabs[0],points[i]);
				}
	}
	
	void SpawnEnemyOndas(){
		StartCoroutine(spawnOnda());

		//Debug.Log(contaSpawnGlobal);
		if(contaSpawnGlobal%5==0){//ativado a cada multiplo de 10
			if (spawnInterval>5){
				spawnInterval--;//reduz spawnInterval a cada 5 spawnEnemy()
			}
		}

	}
	
	IEnumerator spawnOnda(){
		
		int somaSpawnados=0;
		for (int i = 0; i < 3; ++i){
			somaSpawnados+=points[i].childCount;
		}
		Debug.Log("somaSpawnados: "+somaSpawnados);
		if(somaSpawnados==0){
			contaOnda++;
			contaSpawn++;
			contaSpawnGlobal++;
			labelondas.text="ONDA: "+contaSpawn;
			for(int i=1;i<contaSpawn+1;i++){
				GameObject spawnedEnemy=Instantiate(prefabs[0],points[0]);
				yield return new WaitForSeconds(spawnInterval/6);
				GameObject spawnedEnemy1=Instantiate(prefabs[0],points[1]);
				yield return new WaitForSeconds(spawnInterval/6);
				GameObject spawnedEnemy2=Instantiate(prefabs[0],points[2]);
				yield return new WaitForSeconds(spawnInterval/6);
			}
		}
	}

}
