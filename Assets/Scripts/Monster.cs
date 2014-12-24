using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {
    public Transform ds;//目的Cube的Transform
    public GUIText healthText;
  
    private Vector3 origin;//存储导航网格代理的初始位置
    private NavMeshAgent nma;//存储导航网格代理组件
    [SerializeField]
    public int health {get; private set;}

    void Start()
    {
        nma = gameObject.GetComponent<NavMeshAgent>();
        origin = transform.position;
        health = 3;
    }

    void Update()
    {
        if (Menu.bStart && this.active)
        {
            nma.SetDestination(ds.position);

            if ((transform.position.x == ds.position.x))
            {
                GameObject.DestroyObject(this);
            }
        }
        
    }

    public void shooted(int dropHealth)
    {
        
        if (health == 0)
        {
            return;
        }

        health -= dropHealth;
        healthText.text = "" + health;
    }
}
