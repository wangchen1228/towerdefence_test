using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterFactory : MonoBehaviour {

	public GameObject Enemy;
	public Transform startPosition;
    public Transform endPosition;
    public Castle castleSP;

    [SerializeField]
    public List<GameObject> Enemies {get; private set;}

	void Start () 
	{
        Enemies = new List<GameObject>();
		InvokeRepeating("EnemyFactory",1,1f);
	}
	
	void Update () {
        GameObject tempEnemy = null;
	    foreach(GameObject enemy in Enemies){
            float dis = Vector3.Distance(endPosition.position, enemy.transform.position);
            if (dis <= 0.5f) 
            {
                castleSP.shooted(1);
                tempEnemy = enemy;
            }
        }
        if (tempEnemy != null)
        {
            GameObject.DestroyObject(tempEnemy);
            Enemies.Remove(tempEnemy);
        }
	}

    void EnemyFactory()
	{
        GameObject enemyT = Instantiate(Enemy, startPosition.transform.position, Quaternion.identity) as GameObject;
        enemyT.SetActive(true);
        Enemies.Add(enemyT);
	}

    public void hitEnemy(GameObject enemy, int hurt)
    {
        Monster monsterSP = (Monster)enemy.GetComponent(typeof(Monster));
        monsterSP.shooted(hurt);
        if (monsterSP.health == 0)
        {
            GameObject.DestroyObject(enemy);
            Enemies.Remove(enemy);
        }
    }

}
