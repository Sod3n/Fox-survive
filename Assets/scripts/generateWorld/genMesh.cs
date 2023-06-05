using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genMesh : MonoBehaviour
{
    /*float width = 2;
    float height = 2;
    private List<Ground> grounds = new List<Ground>();
    // Start is called before the first frame update
    void Start()
    {
        var mf = gameObject.GetComponent<MeshFilter>();
        var mesh = new Mesh();
        mf.mesh = mesh;
        mf.mesh = addTri(mesh, width, 0, height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Mesh addTri(Mesh mesh, float x, float y, float z)
    {
        Vector3[] vertices = new Vector3[3];

        vertices[0] = new Vector3(-x, y, -z);
        vertices[1] = new Vector3(x, y, -z);
        vertices[2] = new Vector3(-x, y, z);

        mesh.vertices = vertices;

        int[] tri = new int[3];

        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;

        mesh.triangles = tri;

        Vector3[] normals = new Vector3[3];

        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;

        mesh.normals = normals;

        Vector2[] uv = new Vector2[3];

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);

        mesh.uv = uv;

        return mesh;
    }
    private Mesh genGroundMesh()
    {
        int id = 0;
        while(grounds[id].connections.Length > 0)
        {

        }
    }
    private Mesh addGround(Mesh mesh, int id, int dirx, int dirz)
    {
        Ground ground = new Ground();
        if(grounds.Count > 0)
        {
            
            
            for(int i = 0; i < 10; i++)
            {
                for(int q = 0; q < 10; q++)
                {
                    if (dirx == 1)
                    {
                        if(grounds[id].connections[1] == 0)
                        {

                        }
                    }
                    else if (dirx == -1)
                    {
                        if (grounds[id].connections[3] == 0)
                        {

                        }
                    }
                    else if (dirz == 1)
                    {
                        if (grounds[id].connections[0] == 0)
                        {

                        }
                    }
                    else if (dirz == -1)
                    {
                        if (grounds[id].connections[2] == 0)
                        {

                        }
                    }
                    vertices[i] = 
                }
            }
        }
        else
        {

        }
        
    }
    private Ground genGround(Vector3[] side, int dirx, int dirz)
    {
        Ground ground = new Ground();
        ground.id = grounds[grounds.Count - 1].id + 1;
        Vector3[] vert = new Vector3[100];
        if (dirx == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                vert[i] = side[i];
            }
            for (int i = 1; i < 10; i++)
            {
                for (int q = 0; q < 10; q++)
                {
                    vert[q+(i*10)] = new Vector3(vert[0].x + (width * i), 0, vert[0].z + (height * q));
                }
            }
        }
        else if (dirx == -1)
        {
            for (int i = 90; i < 100; i++)
            {
                vert[i] = side[i-90];
            }
            for (int i = 0; i < 9; i++)
            {
                for (int q = 0; q < 10; q++)
                {
                    vert[q + (i * 10)] = new Vector3(vert[90].x - (width * (10-i)), 0, vert[90].z + (height * q));
                }
            }
        }
        else if (dirz == 1)
        {
            int l = 0;
            for (int i = 0; i < 100; i += 10)
            {
                vert[i] = side[l];
                l++;
            }
            for (int i = 0; i < 10; i++)
            {
                for (int q = 1; q < 10; q++)
                {
                    vert[q + (i * 10)] = new Vector3(vert[0].x + (width * i), 0, vert[0].z + (height * q));
                }
            }
        }
        else if (dirz == -1)
        {
            int l = 0;
            for (int i = 9; i < 100; i += 10)
            {
                vert[i] = side[l];
                l++;
            }
            for (int i = 0; i < 10; i++)
            {
                for (int q = 0; q < 9; q++)
                {
                    vert[q + (i * 10)] = new Vector3(vert[9].x + (width * i), 0, vert[9].z - (height * (10 - q)));
                }
            }
        }
    }
}
class Ground
{
    public int id;
    public Vector3[] vert = new Vector3[100];
    public int[] tri = new int[162];
    public Vector3[] normals = new Vector3[100];
    public Vector2[] uv = new Vector2[100];
    public int[] connections = new int[4];

    public Vector3[] getSideVert(int dirx, int dirz)
    {
        Vector3[] side = new Vector3[10];
        if (dirx == 1)
        {
            int q = 0;
            for(int i = 90; i < 100; i += 1)
            {
                side[q] = vert[i];
                q++;
            }
            return side;
        }
        else if (dirx == -1)
        {
            int q = 0;
            for (int i = 0; i < 100; i++)
            {
                side[q] = vert[i];
                q++;
            }
            return side;
        }
        else if (dirz == 1)
        {
            int q = 0;
            for (int i = 9; i < 100; i += 10)
            {
                side[q] = vert[i];
                q++;
            }
            return side;
        }
        else if (dirz == -1)
        {
            int q = 0;
            for (int i = 0; i < 100; i += 10)
            {
                side[q] = vert[i];
                q++;
            }
            return side;
        }
        return null;
    }*/
}
