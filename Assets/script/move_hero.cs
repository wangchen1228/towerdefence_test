using UnityEngine;
using System.Collections;

public class move_hero : MonoBehaviour {
    public Transform ds;//目的Cube的Transform
  
    private Vector3 origin;//存储导航网格代理的初始位置
    private NavMeshAgent nma;//存储导航网格代理组件
    private int health = 100;
    private int mosterhealth = 100;
    private GameObject tower;
    private GameObject sphere;
    private GameObject camera_ui;
    private float up = 0.5f;
    private bool harm_flag = true;
  
    void Start()
    {
        nma = gameObject.GetComponent<NavMeshAgent>();
       
        origin = transform.position;
        tower = GameObject.FindGameObjectWithTag("tower");
        sphere = GameObject.FindGameObjectWithTag("sp");
        camera_ui = GameObject.FindGameObjectWithTag("cameraui");
        if (camera_ui != null)
            Debug.Log("camera_ui != null");

        if (sphere != null) {
            sphere.SetActive(false);
        }
    }

    void Update()
    {
        if (sphere!=null&&tower != null &&harm_flag==true&& mosterhealth>0
            && transform.position.x <= (tower.transform.position.x + sphere.transform.localScale.x * 5)
            && transform.position.y <= (tower.transform.position.y + sphere.transform.localScale.y * 5)
            && transform.position.z <= (tower.transform.position.z + sphere.transform.localScale.z * 5))
        {
            mosterhealth -= 1;
            harm_flag = false;
            StartCoroutine(Wait());
        }


        if (Input.GetMouseButtonDown(0))
        {
            Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(pos, out hit))
            {
                if (hit.collider.gameObject.name == "Cube_tower")
                {
                    sphere.SetActive(true);
                   
                    camera_ui.SetActive(true);
                }

            }
        }
    }


    void OnGUI()
    {
        GUILayout.Label("health:" + health);
        GUILayout.Label("mosterhealth:" + mosterhealth);
        if (GUILayout.Button("Start"))
        {
            nma.SetDestination(ds.position);

            //using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            //{
            //    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            //    {
            //      //  jo.Call("pic_share", path);
            //    }
            //}


        }
        if (GUILayout.Button("Resume"))
        {
            transform.position = origin;

        }
        if (GUILayout.Button("Stop"))
        {
            nma.SetDestination(transform.position);

        }
        if ((transform.position.x == ds.position.x))
        {
            transform.position = origin;
            health -= 1;

        }
        if (GUI.Button(new Rect(200, 0, 80, 20), "Level up"))
        {
           
            sphere.transform.localScale = new Vector3(sphere.transform.localScale.x+up, sphere.transform.localScale.z+up, sphere.transform.localScale.y+up);
            Debug.Log("sphere up" + sphere.transform.localScale.x);
        }

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        harm_flag = true;
       
    }
}
