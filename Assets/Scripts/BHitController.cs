using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHitController : MonoBehaviour
{
    Animator playerAnim;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHitBox")
        {
            Destroy(gameObject);
            Debug.Log("Bullet destroyed by touching player");
            playerAnim.SetTrigger("hit");
        }

        if (collision.gameObject.tag == "InvsWall")
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
