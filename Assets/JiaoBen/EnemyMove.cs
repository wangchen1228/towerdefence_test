using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	public Transform[] wayPoint;
	Vector3 currentTargetPoint;
	CharacterController playerCha;
	Vector3 direction;
	float speed = 200;
	int wayPointSub = 1;
	public  float Life=100;
	void Start () {
		playerCha =gameObject.GetComponent<CharacterController>();
		currentTargetPoint = wayPoint[0].position;
	}
	void Update () {
		playerCha.SimpleMove(direction*speed*Time.deltaTime);
		if(Vector3.Distance(currentTargetPoint,transform.position) < 0.7f)
		{
			PitchNextWayPoint();
		}else{
			direction = (currentTargetPoint -transform.position).normalized;
		}
//		if(xueLiang<=0)
//		{
//			Destroy(gameObject);
//		}
		if(Vector3.Distance(wayPoint[6].position,transform.position)<1f)
		{
			Destroy(gameObject);
		}
		
	}
	
	
	void PitchNextWayPoint()
	{
		currentTargetPoint = wayPoint[wayPointSub].position;
		wayPointSub++;
	}
}
