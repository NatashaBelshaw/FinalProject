using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_controller : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BoundMovement();

        if (scoreText.GetComponent<score_controller>().score < 0)
        {
            FindObjectOfType<game_manager>().GameOver();
        }
    }

    void Move()
    {
        //checking to see if user is pressing keyboard
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float moveX = x * speed;
        float moveY = y * speed;

        rb.velocity = new Vector2(moveX, moveY);
    }
    
    void BoundMovement()
    {
        float dist = (this.transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        Vector3 playerSize = GetComponent<Renderer>().bounds.size;

        this.transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x, leftBorder + playerSize.x / 2, rightBorder - playerSize.x / 2),
            Mathf.Clamp(this.transform.position.y, topBorder + playerSize.y / 2, bottomBorder - playerSize.y / 2),
            this.transform.position.z
            );
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);

        if (other.gameObject.tag == "Worm")
        {
            scoreText.GetComponent<score_controller>().score += 5;
            scoreText.GetComponent<score_controller>().UpdateScore();
        } 
        else if (other.gameObject.tag == "Fish")
        {
            scoreText.GetComponent<score_controller>().score += 10;
            scoreText.GetComponent<score_controller>().UpdateScore();
        } 
        else if (other.gameObject.tag == "Trash")
        {
            scoreText.GetComponent<score_controller>().score -= 10;
            scoreText.GetComponent<score_controller>().UpdateScore();
        }
        else if (other.gameObject.tag == "Shark")
        {
            FindObjectOfType<game_manager>().GameOver();
        }
    }
}
