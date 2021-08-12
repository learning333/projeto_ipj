using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_new : MonoBehaviour
{
	private Rigidbody2D myrigidbody;
	private Animator myanimator;
	
	public int health;
	public float moveSpeed;
	private int local_mf;
	
	private bool isfighting=false;
	private bool isobstructed=false;
	private bool mudou=false;
	private int ataquePlayer;

	void Start()
    {
		//Debug.Log(GameObject.Find("_scripts").GetComponent<menu>().time);
		local_mf=Random.Range(0,2);//enemiespawn.mf;
		
        myrigidbody=GetComponent<Rigidbody2D>();
		myanimator=GetComponent<Animator>();
		mudou=true;
    }
	    // Update is called once per frame
    
	void Update()
    {
		if(health<=0){
			Destroy(gameObject);
		}
		if(randomEvents.pausado==false){
			HandleMovement(); 
			myanimator.speed=1;
		}else{//pausado=true
			myanimator.speed=0;//pausa animacao
		}
    }
	private void HandleMovement(){
		if(isfighting==false && isobstructed==false){
			transform.Translate(-transform.right*moveSpeed*Time.deltaTime);
			//myrigidbody.velocity= Vector2.right;	//x -1,y=0;
			//Debug.Log();
			//myanimator.SetBool("bool_anda",true);
			if(mudou==true){
				
			handleanimacao(menu.time,"andar");
				mudou=false;
			}
		}else{
			if(isfighting==true){
			//transform.Translate(transform.right*moveSpeed*Time.deltaTime);
			//,,,StartCoroutine(BlinkRed());
			}
		}
	
	}
	

	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if(collision.tag=="Player"){
			ataquePlayer=collision.GetComponent<player>().attackPower;
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=true;
			StartCoroutine(BlinkRed());
			//myanimator.SetBool("bool_br_attack",true);
			handleanimacao(menu.time,"atacar");
		}
		if(collision.tag=="playerFemale"){
			
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=true;
			//StartCoroutine(BlinkRed());
			//myanimator.SetBool("bool_br_attack",true);
			handleanimacao(menu.time,"atacar");
		}
		if(collision.tag=="Enemy"){
			isobstructed=true;
		}
		if(collision.tag=="spawntravado" ){
			if(isobstructed==true){
				health=0;
			}
		}
		if(collision.tag=="Out"){
			GameManager.instance.placar.golAway();
			
			Destroy(gameObject);
		}
	}
	/*
	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.tag=="Player"){
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=true;
			//collision.GetComponent<enemy_new>().LoseHealth();
		}

	}*/

			
	
	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag=="Player" || collision.tag=="playerFemale"){
			//yanimator.SetBool("bool_br_attack",false);
			handleanimacao(menu.time,"andar");
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=false;
			isobstructed=false;
		}if(collision.tag=="Enemy"){
			isobstructed=false;
		}

	}
	
	
	
	private void handleanimacao(int filtro,string acao){

		if(local_mf==1){//male
			
			if(filtro==1){//jogador escolheu verde, ativa animacao red para enemy
				if(acao=="andar"){
					//Debug.Log("cheogu aqui");
					myanimator.SetBool("red_ataca",false);
					myanimator.SetBool("red_anda",true);
					
				}else{
					if(acao=="atacar"){
						myanimator.SetBool("red_anda",false);
						myanimator.SetBool("red_ataca",true);
					}
				}
			}else{//jogador de red, enemy de green
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
		}else{//female - local_mf=1
			//Debug.Log(local_mf+"female");
			if(filtro==1){//jogador verde, enemy time red
				if(acao=="andar"){
					myanimator.SetBool("fred_ataca",false);
					myanimator.SetBool("fred_anda",true);

				}else{
					if(acao=="atacar"){
						myanimator.SetBool("fred_anda",false);
						myanimator.SetBool("fred_ataca",true);
					}
				}
			}else{//jogador red, enemy green
				if(acao=="andar"){
					myanimator.SetBool("fbr_ataca",false);
					myanimator.SetBool("fbr_anda",true);
				}else{
					if(acao=="atacar"){
						myanimator.SetBool("fbr_anda",false);
						myanimator.SetBool("fbr_ataca",true);
					}
				}
			}
		}
	}
	
	

	
	/*public void LoseHealth(){
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
	}*/
	
	IEnumerator BlinkRed(){
		if(isfighting==true){
			int hit_tomado=Random.Range(0,6);//random entre 0 e 10;
			
			if(hit_tomado>0){
				if(randomEvents.pausado==false){ //se nao estiver pausado toma hit e faz animacao
					health-=hit_tomado+ataquePlayer;
					//change sprite renderer pra vermelho
					GetComponent<SpriteRenderer>().color=Color.red;
					//wait
					yield return new WaitForSeconds(0.5f);
					//volta ao normal
					GetComponent<SpriteRenderer>().color=Color.white;
				}
			}
			yield return new WaitForSeconds(3f);
			StartCoroutine(BlinkRed()); //calcula hit tomado a cada 3 segundos
		}
	}
}
