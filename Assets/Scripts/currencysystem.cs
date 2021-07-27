using UnityEngine;
using UnityEngine.UI;

public class currencysystem : MonoBehaviour
{
	public Text txt_currency;
	public int defaultCurrency;
	public int currency;
	
	public void Init(){
		currency=defaultCurrency;
		updateUI();
	}
	
	public void Gain(int val){
		currency+=val;
		Debug.Log("Ganhou "+val);
		updateUI();
	}
	public bool Use(int val){
		if(enoughCurrency(val)){
			
			currency-=val;
			updateUI();
			Debug.Log("Gastou "+val);
			return true;
		}else{
			return false;
		}
	}
	
	public bool enoughCurrency(int val){
		if(val<=currency){
			return true;
		}else{
			return false;
		}
	}
	
	
	void updateUI(){
		txt_currency.text=currency.ToString();
	}
}
