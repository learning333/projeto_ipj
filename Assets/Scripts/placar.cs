using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class placar : MonoBehaviour
{

	public Text home;
	public Text away;
	public int placarhome;
	public int placaraway;
	
	public void Init(){
		placarhome=0;
		placaraway=0;
		updateUI();
	}
	
	public void golHome(){
		placarhome++;
		updateUI();
	}
	public void golAway(){
		placaraway++;
		updateUI();
	}

	void updateUI(){
		home.text=placarhome.ToString();
		away.text=placaraway.ToString();
	}
}


