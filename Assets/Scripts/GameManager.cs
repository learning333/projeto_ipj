using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
void Awake(){ instance=this;}


	public spwner enemiespawn;
	public currencysystem currency;

	void Start()
	{
		GetComponent<enemiespawn>().StartSpawning();
		GetComponent<currencysystem>().Init();
	}





}
