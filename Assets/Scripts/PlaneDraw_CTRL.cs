using UnityEngine;
using System.Collections;

public class PlaneDraw_CTRL : MonoBehaviour
{
    public GameObject planeDraw;//这里是Mesh Filter组件所在的物体，要拖进去~
    Mesh planeDraw_mesh;


    void Start()
    {
        planeDraw_mesh= planeDraw.GetComponent<MeshFilter>().mesh;
        //获得MeshFilter组件的mesh属性
        Vector3[] test = new Vector3[8];
        test[0] = new Vector3(4f, 0, 0);
        test[1] = new Vector3(10f, 0, 0);
        test[2] = new Vector3(2f, 0, 0);
        test[3] = new Vector3(6f, 0, 0);
        test[4] = new Vector3(3f, 1f, 0);
        test[5] = new Vector3(2f, 2f, 0);
        test[6] = new Vector3(1f, 5f, 0);
        test[7] = new Vector3(1f, 20f, 0f);

        drawPlane(test);
    }

    //Update is called once per frame
    void Update()
    {

    }

    public void drawPlane(Vector3[] vertices)//入口参数：存放顶点信息的vector3数组
    {
        int numberOfTriangles = vertices.Length - 2;//三角形的数量等于顶点数减2
        int[] triangles = new int [numberOfTriangles*3];//triangles数组大小等于三角形数量乘3

        int f = 0, b = vertices.Length - 1;//f记录前半部分遍历位置，b记录后半部分遍历位置
        for(int i=1; i <= numberOfTriangles; i++)//每次给 triangles数组中的三个元素赋值，共赋值
        { //numberOfTriangles次
            if(i%2==1)
            {
                triangles[3*i-3] = f++;
                triangles[3*i-2] = f;
                triangles[3*i-1] = b;//正向赋值，对于i=1赋值为：0,1,2
            }
            else
            {
                triangles[3*i-1] = b--;
                triangles[3*i-2] = b;
                triangles[3*i-3] = f;//逆向赋值，对于i=2赋值为：1,5,6
            }
        }

        planeDraw_mesh.vertices = vertices;
        planeDraw_mesh.triangles = triangles;//最后给mesh这个属性的vertices和triangles数组赋 值就ok~
    }
}


