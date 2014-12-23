
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CannonShoot : MonoBehaviour {
	List<Collider> Enemys=new List<Collider>();
	public ParticleEmitter myEmitter;
	float gameRate=1f;
	float nextshootTime;
	string Life="";
	void Start () {
	
	}
	
	void Update () 
	{
		for(int i=0;i<Enemys.Count;i++)
		{
			if(Enemys[i]==null)
			{
				Enemys.Remove(Enemys[i]);
			}
		}
//		if(Enemys.Count==0)
//		{
//			myEmitter.emit=false;
//		}else if(Enemys.Count!=0)
//		{
//			myEmitter.emit=true;
//		}
	}
	void OnTriggerEnter(Collider other)
	{
		Enemys.Add(other);
	}
	void OnTriggerStay(Collider other)
	{

		Collider enemy=Enemys[0];
		if(enemy!=null)
		{
			EnemyMove temp=enemy.GetComponent<EnemyMove>();

			transform.LookAt(enemy.transform);
			if(Time.time>nextshootTime)
			{
				nextshootTime=Time.time+gameRate;
				temp.Life-=10;
				for(int j=0;j<temp.Life/10;j++)
				{
					Life=Life+"0";
				}
				temp.transform.GetChild(0).GetComponent<TextMesh>().text=Life;
				Life="";
				myEmitter.Emit();
			}
			if(temp.Life<=0)
			{
				 Enemys.Remove(temp.collider);
				 Destroy(temp.gameObject);	
			}
		}

	}
	void OnTriggerExit(Collider other)
	{
		Enemys.Remove(other);
	}

}
