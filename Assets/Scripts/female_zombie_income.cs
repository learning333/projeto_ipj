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
	private bool isfighting=false;
	
	
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
	
	void Update()
    {
		if(health<=0){
			Destroy(gameObject);
		}
		//Debug.Log(health);
		if(randomEvents.pausado==false){ 
			myanimator.speed=1;
		}else{//pausado=true
			myanimator.speed=0;//pausa animacao
		}
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
		if(randomEvents.pausado==false){
			GameManager.instance.currency.Gain(incomeValue);
		}
		StartCoroutine(CoinIndication());
	}
	//show coin indication over the tower for short time (0.7 second)
	IEnumerator CoinIndication(){
		obj_coin.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		obj_coin.SetActive(false);
	}
	
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if(collision.tag=="Enemy"){
			isfighting=true;
			StartCoroutine(BlinkRed());

		}
	}

	
	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag=="Enemy"){
			isfighting=false;
		}

	}
	

	IEnumerator BlinkRed(){
		if(health<=0){
			Destroy(gameObject);
		}
		if(isfighting==true && randomEvents.pausado==false){
			int hit_tomado=Random.Range(3,9);//random 
			
			if(hit_tomado>0){
				health-=hit_tomado;
				//change sprite renderer pra vermelho
				GetComponent<SpriteRenderer>().color=Color.red;
				//wait
				yield return new WaitForSeconds(0.5f);
				//volta ao normal
				GetComponent<SpriteRenderer>().color=Color.white;
				
			}
			yield return new WaitForSeconds(3f);
			
		}
		StartCoroutine(BlinkRed()); //calcula hit tomado a cada 3 segundos
	}
	
	
}
