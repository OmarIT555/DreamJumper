using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D myBod;
    
    // Start is called before the first frame update
    void Start()
    {
        myBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = myBod.velocity.x;
        float y = myBod.velocity.y;

        x = -5;
        myBod.velocity = new Vector2(x, y);

        

        if (transform.position.x <= -9.38 || transform.position.y <= -6.6 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHitBox")
        {
            Destroy(GameObject.FindGameObjectWithTag("heart"));
            Debug.Log("Heart destroyed"); 

            Destroy(gameObject);
            Debug.Log("Bullet destroyed by touching player");
        }

        if (collision.gameObject.tag == "InvsWall" || collision.gameObject.tag == "fall")
        {
            Destroy(gameObject);
            Debug.Log("Bullet destroyed by touching wall");
        }

        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
            Debug.Log("Bullet destroyed by bullet");
        }

    }

}
