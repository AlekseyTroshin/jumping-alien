using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed = 5;
    public float speedSand = 1f;
    public float speedIcy = 1f;
    public float speedAverage = 5f;
    public float jumpHeight;
    public Transform groundCheck;
    public bool isGrounded;
    Animator animator;
    int currentHP;
    int maxHP = 3;
    bool isHit = false;
    private Coroutine hitCoroutine;
    public Main main;
    public bool key = false;
    public bool canTP = true;
    public bool inWater = false;
    public bool isLadder = false;
    public int coints = 0;
    public bool canHit = true;
    public GameObject invulnerabilityGem;
    public GameObject speedGem;
    public int gemCount = 0;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 5;
        jumpHeight = 10;
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    private void Update()
    { 
        
        if (inWater && !isLadder)
        {
            animator.SetInteger("State", 4);
            isGrounded = true;
            if (Input.GetAxis("Horizontal") != 0)
                Flip();
        }
        else
        {
            GroundCheck();

            if (Input.GetAxis("Horizontal") == 0 && isGrounded && !isLadder)
            {
                animator.SetInteger("State", 1);
            }
            else
            {
                Flip();
                if (isGrounded && !isLadder)
                    animator.SetInteger("State", 2);
            }
        } 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && !isLadder)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
    
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    private void GroundCheck()
    {
        Collider2D[] collaiders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = collaiders.Length > 1;

        if (!isGrounded && !isLadder)
            animator.SetInteger("State", 3);
    }

    public int Coints
    {
        get { return coints; }
    }

    public int Hearts
    {
        get {return currentHP; }
    }

    public void RecountHP(int deltaHP)
    {
        if (deltaHP < 0 && !canHit) return;

        currentHP = currentHP + deltaHP;

        if (currentHP > maxHP) return;

        isHit = true;
        if (hitCoroutine != null) hitCoroutine = null;
        hitCoroutine = StartCoroutine(OnHit(deltaHP));

        if (currentHP <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1.5f);
        }
    } 

    private IEnumerator OnHit(int hit)
    {
        
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (hit < 0 && canHit)
        {
            while (isHit)
            {
                sr.color = new Color(1f, sr.color.g - 0.1f, sr.color.b - 0.1f);
                if (sr.color.b <= 0) isHit = false;
                yield return new WaitForSeconds(0.01f);
            }

            while (!isHit && sr.color.b <= 1f)
            {
                sr.color = new Color(1f, sr.color.g + 0.1f, sr.color.b + 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            canHit = true;
        }
        else if (hit > 0)
        {
            while (isHit)
            {
                sr.color = new Color(sr.color.g - 0.1f, 1f, sr.color.b - 0.1f);
                if (sr.color.b <= 0) isHit = false;
                yield return new WaitForSeconds(0.01f);
            }

            while (!isHit && sr.color.b <= 1f)
            {
                sr.color = new Color(sr.color.g + 0.1f, 1f, sr.color.b + 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
        }
            
        hitCoroutine = null;
    }

    public void Lose()
    {
        main.GetComponent<Main>().LoseGame();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            key = true;
        }

        if(collision.gameObject.tag == "Door")
        {

            if (collision.gameObject.GetComponent<Door>().isOpen && canTP)
            {
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);
                canTP = false;
                StartCoroutine(TPWait());
            }
            else if (key)
            {
                collision.gameObject.GetComponent<Door>().Unlock();
            }
        }

        if (collision.gameObject.tag == "Coint")
        {
            Destroy(collision.gameObject);
            coints++;
        }

        if (collision.gameObject.tag == "Heart" && currentHP < maxHP)
        {
            Destroy(collision.gameObject);
            RecountHP(1);
        }

        if (collision.gameObject.tag == "Mushroom")
        {
            Destroy(collision.gameObject);
            RecountHP(-1);
        }

        if (collision.gameObject.tag == "BlueGem" && canHit && !invulnerabilityGem.activeSelf)
        {
            Destroy(collision.gameObject);
            StartCoroutine(NoHit());
        }

        if (collision.gameObject.tag == "GreenGem" && !speedGem.activeSelf)
        {
            Destroy(collision.gameObject);
            StartCoroutine(SpeedBonus());
        }
    }

    private IEnumerator TPWait()
    {
        yield return new WaitForSeconds(2f);
        canTP = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isLadder = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (Input.GetAxis("Vertical") == 0)
                animator.SetInteger("State", 5);
            else
            {
                animator.SetInteger("State", 6);
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * 0.5f * Time.deltaTime);     
            }
        }

        if (collision.gameObject.tag == "Icy")
        {
            rb.gravityScale = 10f;
            speed = speedIcy;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isLadder = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (collision.gameObject.tag == "Icy")
        {
            rb.gravityScale = 1f;
            speed = speedAverage;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trampoline")
            StartCoroutine(TrampolineAnim(collision.gameObject.GetComponentInParent<Animator>()));

        if (collision.gameObject.tag == "QuickSand")
        {
            speed = speedSand;
            rb.mass = 300f;
        }    

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "QuickSand")
        {
            speed = speedAverage;
            rb.mass = 1f;
        }  
    }

    private IEnumerator TrampolineAnim(Animator anim)
    {
        anim.SetBool("isJump", true);

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("isJump", false);
    }

    private IEnumerator NoHit()
    {
        canHit = false;
        invulnerabilityGem.SetActive(true);
        gemCount++;
        PositionGemHint(invulnerabilityGem);
        invulnerabilityGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(Invis(invulnerabilityGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        
        canHit = true;
        invulnerabilityGem.SetActive(false);
        gemCount--;
        PositionGemHint(speedGem);
    }

    private IEnumerator SpeedBonus()
    {
        speed = speed * 2;
        speedGem.SetActive(true);
        gemCount++;
        PositionGemHint(speedGem);

        speedGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(Invis(speedGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);

        speed = speed / 2;
        speedGem.SetActive(false);
        gemCount--;
        PositionGemHint(invulnerabilityGem);
    }

    private void PositionGemHint(GameObject obj)
    {
        if (gemCount == 1)
        {
            obj.transform.localPosition = new Vector3(0, 1, obj.transform.localPosition.z);
        }
        else if (gemCount == 2)
        {
            invulnerabilityGem.transform.localPosition =new Vector3(-0.3f, 1f, invulnerabilityGem.transform.localPosition.z);
            speedGem.transform.localPosition =new Vector3(0.3f, 1f, speedGem.transform.localPosition.z);
        }
    }

    private IEnumerator Invis(SpriteRenderer spr, float time)
    {
        spr.color = new Color(1f, 1f, 1f, spr.color.a - time * 2);
        yield return new WaitForSeconds(time);
        if (spr.color.a > 0)
            StartCoroutine(Invis(spr, time));
    }

}
