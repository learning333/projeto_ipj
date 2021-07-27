using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class menu : MonoBehaviour
{
	public static int time=0;//default time red
	public static int modo=0;//default modo 0-normal       1-ondas
	public bool escolheutime=false;
	public bool startavel=false;
	
	public static bool pausado=false;
	
	public GameObject[] buttons;
	public GameObject[] buttons_time;
	
	public void Start(){
		buttons = GameObject.FindGameObjectsWithTag("menu_modo");
		buttons_time = GameObject.FindGameObjectsWithTag("menu_time");
		
		foreach(GameObject button in buttons){
			button.SetActive(false);
		}
		 
		 /*for (int i = 0; i < GameObject.Find("botao_modo_manager").transform.childCount; i++){
			GameObject.Find("botao_modo_manager").transform.getChild(i).gameObject.SetActive(false);
		}*/
	}
	public void gerenciador_ui(bool gatilho){
		//gatilho=escolheutime
		bool gatilhoreverso=!gatilho;
		//esconde os botoes de escolha time
		foreach(GameObject button in buttons_time){
			button.SetActive(gatilhoreverso);
		}
		//mostra os botoes de escolha modo
		foreach(GameObject button in buttons){
			button.SetActive(gatilho);
		}
	}	
	
	public void ChangeScenered(string sceneName){
		
		escolheutime=true;
		gerenciador_ui(escolheutime);
		//SceneManager.LoadScene(sceneName);
		
	}
	public void ChangeScenegreen(string sceneName){
		escolheutime=true;
		gerenciador_ui(escolheutime);
		
		time++;
	}
	public void voltaFromModo(){
		startavel=false;
		escolheutime=false;
		time=0;//retorna time para o default
		gerenciador_ui(escolheutime);
	}
	public void btNormal(){
		startavel=true;
		//to-do: deixar o botao em destaque
	}
	public void btOndas(){
		startavel=true;
		modo++;
		//to-do: deixar o botao em destaque, impedir soma infinita do ++
	}
	public void btStart(string sceneName){
		if(startavel==true){
			SceneManager.LoadScene(sceneName);
		}
	}
}
