using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour
{
    Rigidbody2D background;
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float a = Input.acceleration.x;
        float b = Input.acceleration.y;
        
        print(Input.acceleration);

        a = a * .05f;
        b = b * .05f;

        if (!(Time.timeScale == 0))
        {
            if (background.transform.position.y <= -.992)
            {
                b = 0;
            }
            if (background.transform.position.y >= -2.46)
            {
                b = 0;
            }
            


            background.transform.Translate(a, b, 0);

            
        }

    }
}
