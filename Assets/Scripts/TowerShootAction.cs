using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerShootAction : MonoBehaviour {
    public GameObject BulletPreb;
    public MonsterFactory monsterFactorySp;

    private GameObject Monster;
    private GameObject sphere;
    private float shootRadius;
    private bool bulletCreatedOn = false;

    void Start()
    {
        sphere = GameObject.FindGameObjectWithTag("sp");

        if (sphere != null) {
            sphere.SetActive(false);
            shootRadius = sphere.transform.localScale.x * 5;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(pos, out hit))
            {
                if (hit.collider.gameObject.name == "Cube_tower")
                {
                    sphere.SetActive(true);
                }
            }
            else
                sphere.SetActive(false);
        }

        if (Monster != null)
        {
            float dis = Vector3.Distance(Monster.transform.position, transform.position);
            if (dis > shootRadius)
            {
                Monster = null;
            }
        }

        if (Monster == null)
        {
            List<GameObject> enemies = monsterFactorySp.Enemies;
            if (enemies != null && enemies.Count > 0)
            {
                 foreach (GameObject enemy in enemies)
                 {
                     float dis = Vector3.Distance(enemy.transform.position, transform.position);
                     if (dis <= shootRadius)
                     {
                         Monster = enemy;
                         break;
                     }
                 }
            }
        }

        if (Monster != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(Monster.transform.position - transform.position), 20.0f * Time.deltaTime);

            if (!bulletCreatedOn)
            {
                bulletCreatedOn = true;
                InvokeRepeating("shootBullet", 0.5f, 1f);
            }
        }
        else {
            if (bulletCreatedOn)
            {
                CancelInvoke("shootBullet");
                bulletCreatedOn = false;
            }

        }

    }

    void shootBullet()
    {
        List<GameObject> enemies = monsterFactorySp.Enemies;
        if (enemies != null && enemies.Count > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                float dis = Vector3.Distance(enemy.transform.position, transform.position);
                if (dis <= shootRadius)
                {
                    GameObject bullet = (GameObject)GameObject.Instantiate(BulletPreb);
                    Bullet bulletSP = (Bullet)bullet.GetComponent(typeof(Bullet));
                    bulletSP.Monster = enemy;
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);
                }
            }
        }

        
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(200, 0, 80, 20), "Level up"))
        {
            shootRadius += 1f;
            sphere.transform.localScale = new Vector3(shootRadius/5, shootRadius/5, shootRadius/5);
        }
    }

}
