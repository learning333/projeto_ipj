using UnityEngine;
 using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class randomEvents : MonoBehaviour
{
	public Text evento;
	public float eventosInterval;
	public int callzero;
	public int valorEvento;
	public static bool pausado;
	private bool pausado2;
	public GameObject[] button_list;
	public Text labelbotao;
	
	public void Init(){
		pausado=false;
		pausado2=false;
		
		callzero=0;
		//updateUI();
		StartCoroutine(randomEventsDelay());
		
		button_list = GameObject.FindGameObjectsWithTag("popup_random");
		//buttons_time = GameObject.FindGameObjectsWithTag("popup_random");
		
		//buttons_time.SetActive(false);
		foreach(GameObject button in button_list){
			button.SetActive(false);
		}
	}

	private bool eventoAcontece(){
		int chance=Random.Range(0,11);//random de 0 a 10 
		//Debug.Log(chance);
		if(chance>5){//50% de chance de chamar o evento aleatorio a cada eventosInterval
			return true;
		}else{
			return false;
		}
	}
			
		
	
	IEnumerator randomEventsDelay(){
		string[] listafrases = new string[] {"Dolar Acima de todos!!!","Preco da gasolina passa o do feijao!!!","Real acima de todos","Ibov acima de todos"};

		
		yield return new WaitForSeconds(eventosInterval);      //espera tempo pra tentar o primeiro evento
		if(eventoAcontece()){
			foreach(GameObject button in button_list){//mostra botao
				button.SetActive(true);
			}
			pausado=true;
			pausado2=true;
			int idEvento=Random.Range(0,4);//random de 0 a 3 
			//GetComponent<Image>().sprite=eventos[idEvento];
			//Time.timeScale = 0;
			evento.text=listafrases[idEvento];

			if(menu.time==0 ){//usuario com time red
				if(idEvento<2){//evento bom para o red
					valorEvento=6;
					labelbotao.text="+6";
					StartCoroutine(BlinkTextRed());
				}else{//evento ruim
					valorEvento=-6;
					labelbotao.text="-6";
					StartCoroutine(BlinkText());
				}
			}else{//usuario com time green
				if(idEvento>1){//evento bom para o green
					valorEvento=6;
					labelbotao.text="+6";
					StartCoroutine(BlinkText());
				}else{//evento ruim
					valorEvento=-6;
					labelbotao.text="-6";
					StartCoroutine(BlinkTextRed());
				}
			}
			
			
			
		}else{
			StartCoroutine(randomEventsDelay());
		}
		


	}
	IEnumerator BlinkText(){
		//for(int i=0;i<10;i++){
		while(pausado2==true){
			yield return new WaitForSeconds(0.5f);
			evento.color= Color.green;
			if(pausado2==true){
				yield return new WaitForSeconds(0.5f);
				evento.color= Color.yellow;
			}
		}
		pausado=false;	
	}
	
	IEnumerator BlinkTextRed(){
		//for(int i=0;i<10;i++){
		while(pausado2==true){
			yield return new WaitForSeconds(0.5f);
			evento.color= Color.red;
			if(pausado2==true){
				yield return new WaitForSeconds(0.5f);
				evento.color= Color.white;
			}
		}
		pausado=false;	
	}
	
	private void escondebotao(){
		foreach(GameObject button in button_list){
			button.SetActive(false);
		}
	}
	public void botaoOk(){

		GameManager.instance.currency.Gain(valorEvento);
		//Time.timeScale = 1;
		pausado2=false;
		evento.text="";
		escondebotao();
		StopCoroutine(BlinkText());
		StartCoroutine(randomEventsDelay());// so chama de novo depois de sair do atual
	}
}
