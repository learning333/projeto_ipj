using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	private Rigidbody2D myrigidbody;
   
    private Animator myanimator;
	public float moveSpeed;
	private bool isfighting=false;
	private bool isobstructed=false;
	
	//public gerenciador gerenciador;
	private string controle_anim;
	
	public int health,attackPower,cost;
	
	 // Start is called before the first frame update
	void Start()
    {
		//Debug.Log(GameObject.Find("_scripts").GetComponent<menu>().time);
		
        myrigidbody=GetComponent<Rigidbody2D>();
		myanimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if(health<=0){
			Destroy(gameObject);
		}
		//Debug.Log(health);
		if(randomEvents.pausado==false){
			HandleMovement(); 
			myanimator.speed=1;
		}else{//pausado=true
			myanimator.speed=0;//pausa animacao
		}
    }
	
	private void HandleMovement(){
		
		if(isfighting==false && isobstructed==false){
			transform.Translate(transform.right*moveSpeed*Time.deltaTime);//se nao ta lutando nem obstruido, anda

			handleanimacao(menu.time,"andar");
		}else{
			if(isfighting==true){
			
			//
			}
		}
	
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag=="Enemy"){
			
			isfighting=true;
			StartCoroutine(BlinkRed());//se esta lutando chama animacao de losehealth
			//myanimator.SetBool("bool_br_attack",true);
			handleanimacao(menu.time,"atacar");
		}
		if(collision.tag=="Player"){
			isobstructed=true;
		}
		if(collision.tag=="Out"){
			GameManager.instance.placar.golHome();
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.tag=="Enemy"){
			//Debug.Log("lutando");
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=true;
			//collision.GetComponent<enemy_new>().LoseHealth();
		}

	}
	
	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag=="Enemy"){
			//yanimator.SetBool("bool_br_attack",false);
			handleanimacao(menu.time,"andar");
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=false;
			isobstructed=false;
		}if(collision.tag=="Player"){
			
			isobstructed=false;
		}

	}
	/*
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if(collision.tag=="Enemy"){
			
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=true;
			//myanimator.SetBool("bool_br_attack",true);
			handleanimacao(menu.time,"atacar");
			//Debug.Log("ENTER Fighting: "+isfighting+" Obstruceed:"+isobstructed);
		}
		if(collision.tag=="Player"){
			isobstructed=true;
		}
		if(collision.tag=="Out"){
			Destroy(gameObject);
		}
	}
	
	/*
	private void OnTriggerStay2D(Collider2D collision)
	{
		
		if(collision.tag=="Enemy"){
			collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=true;
			Debug.Log("STAY Fighting: "+isfighting+" Obstruceed:"+isobstructed);
		}

	}
	
	
	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag=="Enemy"){
			Debug.Log("enemy morreu");
			//yanimator.SetBool("bool_br_attack",false);
			handleanimacao(menu.time,"andar");
			//collision.GetComponent<enemy_new>().LoseHealth();
			isfighting=false;
			Debug.Log("TRIGER EXIT Fighting: "+isfighting+" Obstruceed:"+isobstructed);
		}
		if(collision.tag=="Player"){
			isobstructed=false;
		}
		
	}*/
	
	private void handleanimacao(int filtro,string acao){
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
	
	
	/*public void LoseHealth(){
		if(true){//randomEvents.pausado==false){
			//Debug.Log("player hp: "+health+" pos hit: "+(health-1));
			//perte hp
			health--;
			//animared
			StartCoroutine(BlinkRed());
			//checa se morreu

		}
	}
	*/
	
	IEnumerator BlinkRed(){
		if(isfighting==true){
			int hit_tomado=Random.Range(0,6);//random entre 0 e 5;
			
			if(hit_tomado>0){
				if(randomEvents.pausado==false){ //se nao estiver pausado toma hit e faz animacao
					health-=hit_tomado;
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
