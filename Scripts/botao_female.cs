 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;

public class botao_female : MonoBehaviour
{
	public Sprite br;
	public Sprite red;
	
		void Start(){
		if(menu.time==1){//br
			GetComponent<Image>().sprite=br;
		}
	}
	
}
