using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clouds : MonoBehaviour
{
    public GameObject player;
    public GameObject moon;
    public float speed = 50f;
    public float cloudSpeed = 0.1f;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        moon = GameObject.Find("Moon");
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.Pause)
        {
            if (transform.position.x - player.transform.position.x > 600)
            {
                transform.position = new Vector3(transform.position.x - 600 * 2, transform.position.y, transform.position.z);
            }
            if (transform.position.x - player.transform.position.x < -600)
            {
                transform.position = new Vector3(transform.position.x + 600 * 2, transform.position.y, transform.position.z);
            }
            if (transform.position.z - player.transform.position.z > 1000)
            {
                if (transform.eulerAngles.x < 50)
                {
                    transform.Rotate(1, 0, 0);
                }
                transform.position += Vector3.down * cloudSpeed;
                if (transform.position.y < -30)
                {
                    transform.position = new Vector3(transform.position.x, -20f, player.transform.position.z - 1300);
                    transform.rotation = Quaternion.Euler(-50, 0, 0);
                }
            }
            if (transform.position.z - player.transform.position.z < -1000)
            {
                if (transform.eulerAngles.x > 15)
                {
                    transform.Rotate(1, 0, 0);
                }
                if (transform.position.y < 36)
                {
                    transform.position += Vector3.up * cloudSpeed * 2;
                }
            }
            transform.position += Vector3.forward * cloudSpeed;
            if (moon.transform.position.y > 0 && rend.material.color.b > 0.25f && rend.material.color.g > 0.02f && rend.material.color.r > 0.05f)
            {
                rend.material.color -= new Color(0.95f * speed / 100, 0.98f * speed / 100, 0.75f * speed / 100, 0f);
            }
            if (moon.transform.position.y < 50 && rend.material.color.b < 1f && rend.material.color.g < 1f && rend.material.color.r < 1f)
            {
                rend.material.color += new Color(0.95f * speed / 100, 0.98f * speed / 100, 0.75f * speed / 100, 0);
            }
        }
        
    }
    
}
