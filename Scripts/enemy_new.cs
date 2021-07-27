using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_new : MonoBehaviour
{
	private Rigidbody2D myrigidbody;
	private Animator myanimator;
	
	public int health;
	public float moveSpeed;
	public int genero;
	
	private bool isfighting=false;
	private bool isobstructed=false;
	
	void Start()
    {
		//Debug.Log(GameObject.Find("_scripts").GetComponent<menu>().time);
		int genero=Random.Range(0,2);//aleatorio de 0 a 1
		
		
        myrigidbody=GetComponent<Rigidbody2D>();
		myanimator=GetComponent<Animator>();
    }
	    // Update is called once per frame
    
	void Update()
    {
		if(true){//randomEvents.pausado==false){
			HandleMovement(); 
		}
    }
	private void HandleMovement(){
		if(isfighting==false && isobstructed==false){
			transform.Translate(-transform.right*moveSpeed*Time.deltaTime);
			//myrigidbody.velocity= Vector2.right;	//x -1,y=0;
			//Debug.Log();
			//myanimator.SetBool("bool_anda",true);
			handleanimacao(menu.time,"andar");
		}else{
			if(isfighting==true){
			StartCoroutine(BlinkRed());
			}
		}
	
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag=="Player"){
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=true;
			//myanimator.SetBool("bool_br_attack",true);
			handleanimacao(menu.time,"atacar");
		}
		//if(collision.tag=="Player"){
		//	isobstructed=true;
		//}
		if(collision.tag=="Out"){
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.tag=="Player"){
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=true;
			//collision.GetComponent<enemy_new>().LoseHealth();
		}

	}
	private void handleanimacao(int filtro,string acao){
		if(genero==0){//male
			if(filtro==1){//jogador escolheu verde, ativa animacao red para enemy
				if(acao=="andar"){
					myanimator.SetBool("red_ataca",false);
					myanimator.SetBool("red_anda",true);
					
				}else{
					if(acao=="atacar"){
						myanimator.SetBool("red_anda",false);
						myanimator.SetBool("red_ataca",true);
					}
				}
			}else{//time green
				if(acao=="andar"){
					myanimator.SetBool("br_ataca",false);
					myanimator.SetBool("br_anda",true);
				}else{
					if(acao=="atacar"){
						myanimator.SetBool("br_anda",false);
						myanimator.SetBool("br_ataca",true);
					}
				}
			}
		}else{//female
			if(filtro==0){//time red
				if(acao=="andar"){
					myanimator.SetBool("red_ataca",false);
					myanimator.SetBool("red_anda",true);

				}else{
					if(acao=="atacar"){
						myanimator.SetBool("red_anda",false);
						myanimator.SetBool("red_ataca",true);
					}
				}
			}else{//time green
				if(acao=="andar"){
					myanimator.SetBool("br_ataca",false);
					myanimator.SetBool("br_anda",true);
				}else{
					if(acao=="atacar"){
						myanimator.SetBool("br_anda",false);
						myanimator.SetBool("br_ataca",true);
					}
				}
			}
		}
	}
	
	
	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag=="Player"){
			//yanimator.SetBool("bool_br_attack",false);
			handleanimacao(menu.time,"andar");
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=false;
		}

	}
	
	public void LoseHealth(){
		if(true){//randomEvents.pausado==false){
			//Debug.Log("player hp: "+health+" pos hit: "+(health-1));
			//perte hp
			health--;
			//animared
			StartCoroutine(BlinkRed());
			//checa se morreu
			if(health<=0){
				Destroy(gameObject);
			}
		}
	}
	
	IEnumerator BlinkRed(){
		//change sprite renderer pra vermelho
		GetComponent<SpriteRenderer>().color=Color.red;
		//wait
		yield return new WaitForSeconds(0.2f);
		//volta ao normal
		GetComponent<SpriteRenderer>().color=Color.white;
	}
}
