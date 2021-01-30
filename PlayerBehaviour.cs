using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public int speed;
    public int health;

    public float moveSpeed = 5f;

    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;
    public GameObject SpawnPoint4;
    public GameObject SpawnPoint5;
    public GameObject SpawnPoint6;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public PrinterBehaviour _printerBehaviour;
    public EmployeeBehaviour _employeeBehaviour;

    Rigidbody2D rb;
    Animator anim;

    public bool facingRight = true;
    public bool isJump = false;
    public bool crouch;

    Vector2 pos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pos.y = transform.position.y;
    }
    void Update()
    {
        MovementDiff();
        if (health<=0)
        {
            Time.timeScale = 0;
        }
        if (health == 2)
        {
            life1.SetActive(false);
        }
        if (health == 1)
        {
            life2.SetActive(false);
        }
        if (health == 0)
        {
            life3.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
        transform.Translate(5 * moveSpeed *Time.deltaTime, 0, 0);
    }
    void MovementDiff()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("isJump");
            anim.SetBool("isCrouch", false);
            rb.velocity = new Vector2(0, 7);
            StartCoroutine(JumpTimer());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("isCrouch", true);
            rb.gravityScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("isCrouch", false);
            rb.gravityScale = 1;
        }
    }
    public void Run()
    {
        anim.SetBool("isCrouch", false);
        rb.gravityScale = 1;
    }
    public void Up()
    {
        anim.SetTrigger("isJump");
        anim.SetBool("isCrouch", false);
        rb.velocity = new Vector2(0, 7);
        StartCoroutine(JumpTimer());
    }
    public void Down()
    {
        anim.SetBool("isCrouch", true);
    }
    private void OnTriggerEnter2D(Collider2D cd)
    {
        if(cd.gameObject.name=="Door1")
        {
            transform.position = SpawnPoint1.transform.position;
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if (cd.gameObject.name == "Door2")
        {
            transform.position = SpawnPoint2.transform.position;
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (cd.gameObject.name == "Door3")
        {
            transform.position = SpawnPoint3.transform.position;
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if (cd.gameObject.name == "Door4")
        {
            SceneManager.LoadScene("Level-2");
        }
        if(cd.gameObject.name=="Door4Level2")
        {
            transform.position = SpawnPoint4.transform.position;
        }
        if (cd.gameObject.name == "Door5")
        {
            transform.position = SpawnPoint5.transform.position;
        }
        if (cd.gameObject.name == "Door6")
        {
            transform.position = SpawnPoint6.transform.position;
        }
        if (cd.gameObject.name == "Door7")
        {
            SceneManager.LoadScene("Level-1");
        }
        if (cd.gameObject.tag == "Sign"|| cd.gameObject.tag == "Loud"|| cd.gameObject.tag == "Paper")
        {
            health -= 1;
            Destroy(cd.gameObject);
        }
    }
    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(1.25f);
        anim.SetBool("isJump",false);
    }
}
