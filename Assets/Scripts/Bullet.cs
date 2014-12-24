using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    [SerializeField]
    public GameObject Monster {get; set;}

    public MonsterFactory monsterFactorySp;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Monster != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                            Quaternion.LookRotation(Monster.transform.position - transform.position), 20.0f * Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime * 8.0f);

            float disBT = Vector3.Distance(transform.position, Monster.transform.position);
            if (disBT <= 0.5f)
            {
                monsterFactorySp.hitEnemy(Monster, 1);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
