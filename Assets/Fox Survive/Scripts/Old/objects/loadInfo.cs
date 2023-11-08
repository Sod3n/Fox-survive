using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadInfo : MonoBehaviour
{
    public static bool loading = true;
    public static float loadingProgress = 0;
    public generateWorld generateWorld = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(loading == true)
        {
            pause.Pause = true;
            if (generateWorld == null)
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Ground"))
                {
                    if (obj.GetComponent<generateWorld>() != null)
                    {
                        generateWorld = obj.GetComponent<generateWorld>();
                        break;
                    }
                }

            }
            else if (loadingProgress == ((generateWorld.maxGenerationRange + 1) * 2 * generateWorld.maxGenerationRange * 2 + 1))
            {
                loading = false;
                pause.Pause = false;
                loadingProgress++;
            }
        }
        
       // Debug.Log((generateWorld.maxGenerationRange + 1) * 2 * generateWorld.maxGenerationRange * 2);
    }
}
