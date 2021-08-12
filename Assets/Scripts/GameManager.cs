using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
void Awake(){ instance=this;}


	public spwner enemiespawn;
	public currencysystem currency;
	public placar placar;
	public randomEvents randomEvents;

	void Start()
	{
		GetComponent<enemiespawn>().StartSpawning();
		GetComponent<currencysystem>().Init();
		GetComponent<placar>().Init();
		GetComponent<randomEvents>().Init();
	}





}
