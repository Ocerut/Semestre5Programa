using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Player : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    private bool ground = false;
    [SerializeField] private float lowRange;
    [SerializeField] private int sceneNo;
    private string nextScene;
    [SerializeField] private float score;
    [SerializeField] private float goal;
    [SerializeField] private TextMeshProUGUI scoreText;
    private Rigidbody2D rb2d;
    [SerializeField] private GameObject porta;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (sceneNo == 1)
        {
            scoreText.text = score.ToString();
            nextScene = "Level2";
        }
        else if (sceneNo == 2)
        {
            scoreText.text = score.ToString();
            nextScene = "Level3";
        }
        else if (sceneNo == 3)
        {
            scoreText.text = score.ToString();
            nextScene = "Fim";
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
        //transform.Translate(Vector2.up * verticalInput * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (ground)
            {
                Jump();
            }
        }

        if (transform.position.y < lowRange)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (score >= goal && porta.activeSelf == false)
        {
           porta.SetActive(true);
        }
    }

    void RevertPower()
    {
        rb2d.gravityScale = 5f;
        speed = 15f;
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        ground = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
        {
            ground = true;
        }
        if (collision.gameObject.tag == "Espinho")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moeda")
        {
            Destroy(collision.gameObject);
            score++;
            scoreText.text = score.ToString();
        }
        if (collision.gameObject.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            rb2d.gravityScale = 2.5f;
            speed = 22.5f;
            Invoke(nameof(RevertPower), 7.5f);
        }
        
        if (collision.gameObject.tag == "Fim")
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
