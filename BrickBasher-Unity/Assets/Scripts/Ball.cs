/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Bobby Ouyang 
 * Last Edited: April 28, 2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    public int score;
    public float speed;
    public Text ballText;
    public Text scoreText;
    public GameObject paddle;
    

    [Header("Ball Settings")]
    public int numberOfBalls;
    public Vector3 force = new Vector3(.5f, 1, 0);

    [HideInInspector]
    Rigidbody rb;
    AudioSource audioSource;
    bool isInPlay = false;

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }//end Awake()


        // Start is called before the first frame update
        void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        ballText.text = "Balls: " + numberOfBalls;
        scoreText.text = "Score: " + score;

        if (!isInPlay) 
        {
            Vector3 pos = paddle.transform.position;
            pos.y += paddle.transform.localScale.y;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.Space) && !isInPlay)
        {
            isInPlay = true;
            Move();
        }
    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay)
        {
            rb.velocity = speed * rb.velocity.normalized;
        }

    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()


    void Move() 
    {
        rb.AddForce(force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        GameObject gb = collision.gameObject;

        if (gb.CompareTag("Brick")) 
        {
            score += 100;
            Destroy(gb);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gb = other.gameObject;

        if (gb.CompareTag("OutBounds")) 
        {
            numberOfBalls--;
        }

        if (numberOfBalls > 0) 
        {
            Invoke("SetStartingPos", 2f);

        }
    }

}
