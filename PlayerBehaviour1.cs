using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 2;
    public float rayCastValue;

    public int jump;

    public bool canJump = false;
    public bool canMove = false;
    public bool inWater = false;
    public bool isMagnetic = false;
    public bool pushing = false;
    public bool magneticRight = false;
    public bool magneticUp = false;
    public bool magneticLeft = false;
    public bool magneticDown = false;

    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (canMove && !isMagnetic)
        {
            Movement();
        }
        if (magneticRight)
        {
            MagneticRight();
        }
        if (magneticUp)
        {
            MagneticUp();
        }
        if (magneticDown)
        {
            MagneticDown();
        }
        if (magneticLeft)
        {
            MagneticLeft();
        }
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            if (!pushing)
            {
                anim.SetBool("isWalk", true);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 180, 0);
            if (!pushing)
            {
                anim.SetBool("isWalk", true);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!canJump && !canMove)
            {
                rb.gravityScale += 2;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (canMove && canJump)
            {
                rb.AddForce(Vector3.up * jump);
                StartCoroutine(CanMove());
            }
        }
        if (canJump && canMove)
        {
            rb.gravityScale = 2.1f;
        }
        if ((Input.GetKeyUp(KeyCode.D)) || (Input.GetKeyUp(KeyCode.A)))
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isWalk", false);
        }
    }
    void MagneticRight()
    {
        Debug.Log("Right");
        transform.localEulerAngles = new Vector3(0, 0, 90);
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
        }
        isMagnetic = true;
        speed = 1.5f;
        rb.gravityScale = 0;
    }
    void MagneticUp()
    {
        transform.localEulerAngles = new Vector3(0, 0, 180);
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
        }
        isMagnetic = true;
        speed = 1.5f;
        rb.gravityScale = 0;
    }
    void MagneticDown()
    {
        Debug.Log("Down");
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 180, 0);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.up * 100);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
        }
        isMagnetic = true;
        speed = 1.5f;
    }
    void MagneticLeft()
    {
        Debug.Log("Left");
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
        }
        isMagnetic = true;
        speed = 1.5f;
        rb.gravityScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D cd)
    {
        if (cd.gameObject.tag == "Platform" || cd.gameObject.tag == "MagneticPlatform")
        {
            canJump = true;
            canMove = true;
            StopCoroutine(CanMove());
            Debug.Log("Working");
        }
        if (cd.gameObject.tag == "MagneticPlatform")
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        if (cd.gameObject.tag == "Block")
        {
            canJump = true;
            canMove = true;
            StopCoroutine(CanMove());
            pushing = true;
            Debug.Log("Working");
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", true);
        }
        if (cd.gameObject.tag == "MagneticPlatformRight")
        {
            magneticRight = true;
            canJump = false;
            StopCoroutine(CanMove());
        }
        if (cd.gameObject.tag == "MagneticPlatformLeft")
        {
            magneticLeft = true;
            canJump = false;
            StopCoroutine(CanMove());
        }
        if (cd.gameObject.tag == "MagneticPlatformDown")
        {
            magneticDown = true;
            canJump = true;
            StopCoroutine(CanMove());
        }
        if (cd.gameObject.tag == "MagneticPlatformUp")
        {
            magneticUp = true;
            canJump = false;
            StopCoroutine(CanMove());
        }
        if ((cd.gameObject.tag == "MagneticPlatformUp") && (cd.gameObject.tag == "MagneticPlatformRight"))
        {
            magneticUp = true;
            magneticRight = false;
        }
    }
    private void OnCollisionStay2D(Collision2D cd)
    {
        if (cd.gameObject.tag == "Platform" || cd.gameObject.tag == "MagneticPlatform")
        {
            canJump = true;
            canMove = true;
            StopCoroutine(CanMove());
        }

        if (cd.gameObject.tag == "Block")
        {
            canJump = true;
            canMove = true;
            pushing = true;
            StopCoroutine(CanMove());
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", true);
        }
        if (cd.gameObject.tag == "MagneticPlatformRight")
        {
            magneticRight = true;
            canJump = false;
            StopCoroutine(CanMove());
        }
        if (cd.gameObject.tag == "MagneticPlatformLeft")
        {
            magneticLeft = true;
            canJump = false;
            StopCoroutine(CanMove());
        }
        if (cd.gameObject.tag == "MagneticPlatformDown")
        {
            magneticDown = true;
            canJump = true;
            StopCoroutine(CanMove());
        }
        if (cd.gameObject.tag == "MagneticPlatformUp")
        {
            magneticUp = true;
            canJump = false;
            StopCoroutine(CanMove());
        }
    }
    private void OnCollisionExit2D(Collision2D cd)
    {
        if (cd.gameObject.tag == "Platform" && !inWater)
        {
            canJump = false;
            StartCoroutine(CanMove());
        }
        if (cd.gameObject.tag == "MagneticPlatformDown" || cd.gameObject.tag == "MagneticPlatformUp" || cd.gameObject.tag == "MagneticPlatformRight" || cd.gameObject.tag == "MagneticPlatformLeft")
        {
            canJump = false;
            StartCoroutine(CanMove());
        }
        if (cd.gameObject.tag == "MagneticPlatform")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (cd.gameObject.tag == "Block")
        {
            StartCoroutine(CanMove());
            anim.SetBool("isPush", false);
            pushing = false;
        }
        if (cd.gameObject.tag == "MagneticPlatformRight")
        {
            isMagnetic = false;
            speed = 2;
            rb.gravityScale = 3;
            magneticRight = false;
            magneticUp = false;
            canJump = true;
        }
        if (cd.gameObject.tag == "MagneticPlatformLeft")
        {
            isMagnetic = false;
            speed = 2;
            rb.gravityScale = 3;
            magneticRight = false;
            magneticUp = false;
            canJump = true;
        }
        if (cd.gameObject.tag == "MagneticPlatformDown")
        {
            isMagnetic = false;
            speed = 2;
            rb.gravityScale = 3;
            magneticRight = false;
            magneticUp = false;
            canJump = false;
        }
        if (cd.gameObject.tag == "MagneticPlatformUp")
        {
            isMagnetic = false;
            speed = 2;
            rb.gravityScale = 3;
            magneticRight = false;
            magneticUp = false;
            canJump = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D cd)
    {
        if (cd.gameObject.tag == "Finish")
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (cd.gameObject.tag == "Finish1")
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (cd.gameObject.tag == "Finish2")
        {
            Time.timeScale = 0;
            victoryPanel.SetActive(true);
        }
        if (cd.gameObject.tag == "Death")
        {
            anim.SetTrigger("isShock");
            anim.SetBool("isPush", false);
            anim.SetBool("isWalk", false);
            canJump = false;
            canMove = false;
            rb.gravityScale = 0;
            transform.position = transform.position;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (cd.gameObject.tag == "Fan")
        {
            rb.velocity = new Vector2(transform.position.x, Mathf.Lerp(0, 10, 1));
            rb.gravityScale = 0;
        }
        if (cd.gameObject.tag == "Water")
        {
            rb.gravityScale = 5;
            speed = 0.5f;
            inWater = true;
            StopCoroutine(CanMove());
        }
        if(cd.gameObject.tag=="MoveRight")
        {
            Debug.Log("right");
            transform.position = new Vector2(7.97f, 0.19f);
            transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        if (cd.gameObject.tag == "MoveUp")
        {
            Debug.Log("up");
            transform.position = new Vector2(6.71f, 1.47f);
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
    }
    private void OnTriggerStay2D(Collider2D cd)
    {
        if (cd.gameObject.tag == "Fan")
        {
            rb.velocity = new Vector2(transform.position.x, Mathf.Lerp(0, 15, 0.5f));
            rb.gravityScale = 0;
        }
        if (cd.gameObject.tag == "Water")
        {
            rb.gravityScale = 5;
            speed = 0.5f;
            inWater = true;
            StopCoroutine(CanMove());
        }
    }
    private void OnTriggerExit2D(Collider2D cd)
    {
        if (cd.gameObject.tag == "Fan")
        {
            rb.gravityScale = 3;
        }
        if (cd.gameObject.tag == "Water")
        {
            rb.gravityScale = 3;
            speed = 2;
            inWater = false;
        }
    }
    public void NextLevel2()
    {
        SceneManager.LoadScene("Level2");
        Time.timeScale = 1;
    }
    public void NextLevel3()
    {
        SceneManager.LoadScene("Level3");
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    IEnumerator CanMove()
    {
        yield return new WaitForSeconds(4f);
        canMove = false;
    }
}


