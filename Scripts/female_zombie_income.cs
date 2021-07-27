using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class female_zombie_income : MonoBehaviour
{
	public int health;
	public int cost;
	
	//income value
	public int incomeValue;
	//interval for income
	public float interval;
	//muedinha
	public GameObject obj_coin;
	private Animator myanimator;
	
	
	
	//methods
	//init
	public void Start(){
		myanimator=GetComponent<Animator>();  
		if(menu.time==0){//red
			//myanimator.SetBool("red_fem_spawned",true);
			//Debug.Log("red shouldbe true");
		}else{
			myanimator.SetBool("br_idle",true);
			//Debug.Log("br shouldbe true");
		}
		StartCoroutine(Interval());
	}
	//interval ienumerator
	IEnumerator Interval(){
		yield return new WaitForSeconds(interval);
		//triger the income increase
		IncreaseIncome();
		StartCoroutine(Interval());
	}
	//Trigger income increase
	public void IncreaseIncome(){
		//if(randomEvents.pausado==false){
			GameManager.instance.currency.Gain(incomeValue);
		//}
		StartCoroutine(CoinIndication());
	}
	//show coin indication over the tower for short time (0.5 second)
	IEnumerator CoinIndication(){
		obj_coin.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		obj_coin.SetActive(false);
	}
	//lose health
	public void LoseHealth(){
		health--;
		if(health<=0){
			morre();
		}
	}
	//die
	public void morre(){
			//Debug.Log("morreu");
			Destroy(gameObject);
	}
	
	
}
