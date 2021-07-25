 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;


public class botao_male : MonoBehaviour
{
	public Sprite br;
	public Sprite red;
	
	void Start(){
		if(menu.time==1){//br
			GetComponent<Image>().sprite=br;
		}
	}
	
}
