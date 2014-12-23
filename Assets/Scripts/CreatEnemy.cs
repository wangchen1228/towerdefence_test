using UnityEngine;
using System.Collections;

public class CreatEnemy : MonoBehaviour {

	public GameObject Enemy;
	GameObject Enemytemp;
	public Transform startPosition;
	void Start () 
	{
		InvokeRepeating("EnemyFactory",1,3f);
	}
	
	
	void Update () {
	
	}
	void EnemyFactory()
	{  
		Enemytemp =Instantiate(Enemy,startPosition.transform.position,Quaternion.identity)as GameObject;
	}
}
