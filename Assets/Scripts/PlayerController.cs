using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDataPersistent
{
    public float movePower = 10f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

    public float scale = 0.5f;
    public Text dieText;

    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;
    private readonly float fallPosition = -23f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Restart();
        if (alive && !GameState.Instance.GetPlayerFronze())
        {
            Hurt();
            Die();
            Attack();
            Jump();
            Run();

        }
        else
        {
            anim.SetBool("isRun", false);
        }
        if (transform.position.y < fallPosition)
        {
            Recover();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetBool("isJump", false);
    }


    void Run()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction * scale, scale, scale);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction * scale, scale, scale);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
        if (!isJumping)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("attack");
        }
    }
    void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        }
    }
    void Die()
    {
        if (GameState.Instance.GetPlayerDie())
        {
            anim.SetTrigger("die");
            alive = false;
            StartCoroutine(Helper.Delay(Recover, 0.5f));
        }
    }
    void Recover()
    {
        GameState.Instance.AddDeadCount();
        dieText.text = GameState.Instance.GetDeadCount().ToString();
        anim.SetTrigger("idle");
        alive = true;
        GameState.Instance.SetPlayerDie(false);
        transform.position = Vector3.Lerp(transform.position, GameState.Instance.GetPlayerInitPosition(), 1);
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }

    public void LoadData(GameState data)
    {
        transform.position = GameState.Instance.GetPlayerPosition();
        dieText.text = GameState.Instance.GetDeadCount().ToString();
    }

    public void SaveData()
    {
        GameState.Instance.SetPlayerPosition(transform.position);
    }
}