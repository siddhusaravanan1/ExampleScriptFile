using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 3;
    public float rayCastValue;

    public int jump = 500;
    public int magnetic = 0;

    public bool inWater = false;
    public bool pushing = false;
    public bool isGrounded = true;
    public bool isFat = false;

    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    public Animator door;


    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(magnetic == 0)
        {
            Movement();
        }

        if (magnetic == 1)
        {
            MagneticRight();
        }
        if (magnetic == 4)
        {
            MagneticUp();
        }
        if (magnetic == 3)
        {
            MagneticDown();
        }
        if (magnetic == 2)
        {
            MagneticLeft();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && !isFat)
        {
            transform.localScale = new Vector3(0.6f,0.6f,0);
            speed = 2;
            rb.mass = 2;
            isFat = true;
            jump = 800;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && isFat)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0);
            speed = 3;
            rb.mass = 1;
            isFat = false;
            jump = 650;
        }
    }
    void Movement()
    {
        if (isGrounded)
        {
            rb.gravityScale = 2.1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 180, 0);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!isGrounded)
            {
                rb.gravityScale += 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jump);
                isGrounded = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
        }
    }
    void MagneticRight()
    {
        Debug.Log("Right");
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 0, 90);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(180, 0, 90);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", false);
        }
        rb.gravityScale = 0;
        speed = 1.5f;
    }
    void MagneticUp()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 180, 180);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 0, 180);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", false);
        }
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jump);
                isGrounded = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", false);
        }
        speed = 5f;
    }
    void MagneticLeft()
    {
        Debug.Log("Left");
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(180, 0, -90);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, 0, -90);
            anim.SetBool("isWalk", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", false);
        }
        speed = 1.5f;
        rb.gravityScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D cd)
    {
        if (cd.gameObject.tag == "Platform")
        {
            isGrounded = true;
            Debug.Log("Working");
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (cd.gameObject.tag == "Block" && isFat)
        {
            isGrounded = true;
            pushing = true;
            Debug.Log("Working");
            anim.SetBool("isPush", true);
        }
        if (cd.gameObject.tag == "MagneticPlatformRight")
        {
            magnetic = 1;
            transform.localEulerAngles = new Vector3(0, 0, 90);
            isGrounded = false;
            anim.SetBool("isPush", false);
        }
        if (cd.gameObject.tag == "MagneticPlatformLeft")
        {
            magnetic = 2;
            transform.localEulerAngles = new Vector3(0, 0, -90);
            isGrounded = false;
            anim.SetBool("isPush", false);
        }
        if (cd.gameObject.tag == "MagneticPlatformDown")
        {
            magnetic = 3;
            isGrounded = true;
            anim.SetBool("isPush", false);
        }
        if (cd.gameObject.tag == "MagneticPlatformUp")
        {
            magnetic = 4;
            transform.localEulerAngles = new Vector3(0, 0, 180);
            isGrounded = false;
            anim.SetBool("isPush", false);
        }

    }
    private void OnCollisionStay2D(Collision2D cd)
    {
        if (cd.gameObject.tag == "Platform" || cd.gameObject.tag == "MagneticPlatform")
        {
            isGrounded = true;
        }

        if (cd.gameObject.tag == "Block" && isFat)
        {
            isGrounded = true;
            pushing = true;
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", true);
        }
        if (cd.gameObject.tag == "MagneticPlatformRight")
        {
            magnetic = 1;
            isGrounded = false;
            anim.SetBool("isPush", false);
        }
        if (cd.gameObject.tag == "MagneticPlatformLeft")
        {
            magnetic = 2;
            isGrounded = false;
            anim.SetBool("isPush", false);
        }
        if (cd.gameObject.tag == "MagneticPlatformDown")
        {
            magnetic = 3;
            isGrounded = true;
            anim.SetBool("isPush", false);
        }
        if (cd.gameObject.tag == "MagneticPlatformUp")
        {
            magnetic = 4;
            isGrounded = false;
            anim.SetBool("isPush", false);
        }
    }
    private void OnCollisionExit2D(Collision2D cd)
    {
        if (cd.gameObject.tag == "Platform" && !inWater)
        {
            isGrounded = false;
        }
        if (cd.gameObject.tag == "MagneticPlatformDown" || cd.gameObject.tag == "MagneticPlatformUp" || cd.gameObject.tag == "MagneticPlatformRight" || cd.gameObject.tag == "MagneticPlatformLeft")
        {
            isGrounded = false;
        }
        if (cd.gameObject.tag == "Block")
        {
            pushing = false;
        }
        if (cd.gameObject.tag == "MagneticPlatformRight")
        {
            speed = 2;
            rb.gravityScale = 3;
            magnetic = 0;
        }
        if (cd.gameObject.tag == "MagneticPlatformLeft")
        {
            speed = 2;
            rb.gravityScale = 3;
            magnetic = 0;
        }
        if (cd.gameObject.tag == "MagneticPlatformDown")
        {
            speed = 2;
            rb.gravityScale = 3;
            magnetic = 0;
        }
        if (cd.gameObject.tag == "MagneticPlatformUp")
        {
            speed = 2;
            rb.gravityScale = 3;
            magnetic = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D cd)
    {
        if(cd.gameObject.tag == "Block")
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", true);
        }
        if (cd.gameObject.tag == "Finish")
        {
            door.SetTrigger("isOpen");
            StartCoroutine(DoorOpen());
        }
        if (cd.gameObject.tag == "Finish1")
        {
            door.SetTrigger("isOpen");
            StartCoroutine(DoorOpen());
        }
        if (cd.gameObject.tag == "Finish2")
        {
            door.SetTrigger("isOpen");
            StartCoroutine(DoorOpen());
        }
        if (cd.gameObject.tag == "Finish3")
        {
            SceneManager.LoadScene("EndCutScene");
        }
        if (cd.gameObject.tag == "Death")
        {
            anim.SetTrigger("isShock");
            anim.SetBool("isPush", false);
            anim.SetBool("isWalk", false);
            isGrounded = false;
            rb.gravityScale = 0;
            transform.position = transform.position;
            StartCoroutine(Death());
        }
        if (cd.gameObject.tag == "Fan")
        {
            //rb.AddForce(new Vector2(0, 100));
            rb.gravityScale = -1;
        }
        if (cd.gameObject.tag == "FanDown")
        {
            rb.gravityScale = 3;
        }
        if (cd.gameObject.tag == "Water")
        {
            rb.gravityScale = 5;
            speed = 0.5f;
            inWater = true;
            isGrounded = true;
        }
    }
    private void OnTriggerStay2D(Collider2D cd)
    {
        if (cd.gameObject.tag == "Fan")
        {
            rb.gravityScale = -1;
        }
        if (cd.gameObject.tag == "FanDown")
        {
            rb.gravityScale = 3;
        }
        if (cd.gameObject.tag == "Water")
        {
            rb.gravityScale = 5;
            speed = 0.5f;
            inWater = true;
            isGrounded = true;
        }
        if (cd.gameObject.tag == "Block")
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isPush", true);
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
        if (cd.gameObject.tag == "Block")
        {
            anim.SetBool("isPush", false);
            Debug.Log("Normal");
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
        SceneManager.LoadScene("Level4");
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    IEnumerator DoorOpen()
    {
        yield return new WaitForSeconds(2f);
        victoryPanel.SetActive(true);
        Time.timeScale = 0;
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.25f);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

}


