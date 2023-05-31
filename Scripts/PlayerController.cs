using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private CapsuleCollider col;
    private Vector3 dir;
    private Animator anim;
    private Score score;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumping = 0f;
    [SerializeField] private int gravity = 0;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject LozePanel;
    [SerializeField] private int coins;
    [SerializeField] private Text coinsText;
    [SerializeField] private Score scoreRecord;
    
    private int linetoMove = 1;
    public float linedistance = 4;
    private float maxSpeed = 110;
    private bool Roll;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1;
        col = GetComponent<CapsuleCollider>();
        score = scoreText.GetComponent<Score>();
        coins = PlayerPrefs.GetInt("Coins");
        coinsText.text = coins.ToString();
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        if (Swipe.swipeRight)
        {
            if (linetoMove < 2)
                linetoMove++;
        }
        if (Swipe.swipeLeft)
        {
            if (linetoMove > 0)
                linetoMove--;
        }
        if (Swipe.swipeUp)
        {
            if (controller.isGrounded)
                StartCoroutine(Jump());
        }
        if (Swipe.swipeDown)
        {
            StartCoroutine(Slide());
        }
        Vector3 target = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (linetoMove == 0)
            target += Vector3.left * linedistance;
        else if (linetoMove == 2)
            target += Vector3.right * linedistance;
        if (transform.position == target)
            return;
        Vector3 diff = target - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
        speed += 0.5f * Time.deltaTime;
        if (controller.isGrounded && !Roll)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);
    }
    private IEnumerator Jump()
    {
        dir.y = jumping;
        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(1);
        anim.SetBool("Run", true);
    }
    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "lol")
        {
            LozePanel.SetActive(true);
            int ScoreMax = int.Parse(scoreRecord.ScoreText.text.ToString());
            PlayerPrefs.SetInt("ScoreMax", ScoreMax);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("Coins", coins);
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "x2")
        {
            StartCoroutine(X2());
            Destroy(other.gameObject);
        }
    }
    private IEnumerator SpeedIncrease()
    { 
        yield return new WaitForSeconds(4);
        if (speed < maxSpeed)
        {
            speed += 1;
            StartCoroutine(SpeedIncrease());
        }
    }
    private IEnumerator Slide()
    {
        
        controller.center = new Vector3(0, 0.5f, 0);
        controller.height = 1;
        col.center = new Vector3(0, 0.5f, 0);
        col.height = 1;
        gravity = -80;
        Roll = true;
        anim.SetTrigger("Roll");

        yield return new WaitForSeconds(0.8f);
        Roll = false;
        anim.SetBool("Run", true);
        col.center = new Vector3(0, 1, 0);
        col.height = 1.781034f;
        controller.center = new Vector3(0, 1, 0);
        controller.height = 2;
        gravity = -20;
    }
    private IEnumerator X2()
    {
        score.Multiply = score.Multiply * 4;
        yield return new WaitForSeconds(5);
        score.Multiply = score.Multiply / 2;
    }
}



