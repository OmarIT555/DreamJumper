using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    Rigidbody2D myBod;
    Animator myAnim;
    bool isGrounded = false;
    bool secondJump = true;
    float timer;
    float time = 0;
    float score = 0;
    public float spawnTime;
    public GameObject heart;
    GameObject[] hearts;

    AudioSource myAudio;
    public AudioClip ScoreUp;
    public AudioClip Jump1;
    public AudioClip Jump2;
    public AudioClip playerHit;
    bool ducking = false;
    public BoxCollider2D playerCollider;
    public BoxCollider2D playerCollider2;

    [SerializeField] Text FinalTime;
    [SerializeField] Text ScoreText;

    float heartNum = 3;
    float a = 0;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();

        timer = spawnTime;
        myBod = GetComponent<Rigidbody2D>();
        myAnim = GetComponentInChildren<Animator>();

        GameObject h1 = Instantiate(heart);
        h1.transform.position = new Vector3(-5, 4.47f, 0);

        GameObject h2 = Instantiate(heart);
        h2.transform.position = new Vector3(-6, 4.47f, 0);

        GameObject h3 = Instantiate(heart);
        h3.transform.position = new Vector3(-7, 4.47f, 0);

        
    }

    // Update is called once per frame
    void Update()
    {
        time += 1 * Time.deltaTime;
        //print("Time: " + time.ToString("0"));

        timer -= Time.deltaTime;
        if (timer <= 0 && time > 2)
        {
            Spawn();
            timer = spawnTime;
        }

        float x = myBod.velocity.x;
        float y = myBod.velocity.y;
    

        if (ducking == false)
        {
            a = Input.acceleration.x;
            a = a * .3f;

            if (a > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                print("player flipped");
            }
            else if (a < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                print("player flipped 2");
            }
        }

        if (!(Time.timeScale == 0))
        {
            gameObject.transform.Translate(a, 0, 0);
        }

        if (Input.touches.Length == 0)
        {
            myAnim.SetBool("ducking", false);
            playerCollider.size = new Vector2(.6f, 1.1f);
            playerCollider.offset = new Vector2(0.1f, .7f);
            print("Player Collider size :" + playerCollider.size);
            print("Player Collider offset :" + playerCollider.offset);

            playerCollider2.size = new Vector2(0.3f, 1);
            playerCollider2.offset = new Vector2(-5.8f, 6.6f);
            print("Player Collider2 size :" + playerCollider2.size);
            print("Player Collider2 offset :" + playerCollider2.offset);

            ducking = false;
        }

        if (Input.touches.Length == 2)
        {
            myAnim.SetBool("ducking", true);
            playerCollider.size = new Vector2(.6f, .5f);
            playerCollider.offset = new Vector2(.1f, .5f);

            playerCollider2.size = new Vector2(.3f, .5f);
            playerCollider2.offset = new Vector2(-5.8f, 6.3f);
            ducking = true;
        }

        if (Input.touches.Length == 4)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            y = 10;
            myAudio.PlayOneShot(Jump1);
        }

        if (Input.GetButtonDown("Fire1") && !isGrounded && secondJump)
        {
            y = 8;
            myAudio.PlayOneShot(Jump2);
            secondJump = false;
            
        }

        hearts = GameObject.FindGameObjectsWithTag("heart");
        if (!(Mathf.Approximately(hearts.Length, heartNum)))
        {
            heartNum = hearts.Length;
            myAudio.PlayOneShot(playerHit);
            myAnim.SetTrigger("hit");
        }

        if(hearts.Length == 0)
        {
            FinalTime.text =
            "Game Over " + "\n" +
            "Time: " + time.ToString("0") + " seconds" + "\n" +
            "Score: " + score + "\n" +
            "Tap to start again";
             Time.timeScale = 0;
        }

        if (Input.GetButtonDown("Fire1") && Time.timeScale == 0)
        {
            FinalTime.text = "";
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1;
        }

        ScoreText.text = "Score: " + score; 

        myBod.velocity = new Vector2(x, y);

        

    }

    void Spawn()
    {
        GameObject g = Instantiate(bullet);
        g.transform.position = transform.position + new Vector3(16, Random.Range(-1, 4), 0);
        GameObject g2 = Instantiate(bullet);
        g2.transform.position = transform.position + new Vector3(16, Random.Range(6, 8), 0);
        GameObject g3 = Instantiate(bullet);
        g3.transform.position = transform.position + new Vector3(16, Random.Range(-3, -4), 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
            isGrounded = true;
            myAnim.SetBool("Jumping", !isGrounded);
            secondJump = true;

        if (collision.gameObject.tag == "bullet" && collision.gameObject.tag != "PlayerHitBox")
        {
            score += 10;
            myAudio.PlayOneShot(ScoreUp);
        }

        if (collision.gameObject.tag == "fall")
        {
            FinalTime.text =
            "You fell! Game Over " + "\n" +
            "Time: " + time.ToString("0") + " seconds" + "\n" +
            "Score: " + score + "\n" + 
            "Tap to start again"; ;
            Time.timeScale = 0;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        myAnim.SetBool("Jumping", !isGrounded);

    }

}
