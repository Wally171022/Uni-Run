using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   public int powertime = 0;



    private void Awake() {
       // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   }


   private void Update() {

        // 사망 시에는 더 이상 루틴을 처리하지 않는다.
        // **중요한 내용**
        // 필요없는 동작을 막을수 있다.
        if (isDead)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if(Input.GetMouseButtonDown(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
            //속도를 절반을 줄이며
        }
        animator.SetBool("Grounded", isGrounded);
        // 사용자 입력을 감지하고 점프하는 처리

   }

   private void Die() {
        // 사망 처리

        //애니메이터의 Die 트리거 파라미터를 셋
        playerAudio.clip = deathClip;

        playerAudio.Play();
        animator.SetTrigger("Die");
        playerRigidbody.velocity = Vector2.zero;

        isDead = true;
        
        //게임 매니저의 게임 오버 처리 실행
        GameManager.instance.OnPlayerDead();

   }


   private void OnTriggerEnter2D(Collider2D other) 
    {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
       if(other.tag == "Dead" && isDead == false)
        {
            //충돌한 상대방의 태그가 Dead이며 아직 사망하지 않았다면 Die()실행
            Die();
        }
       if(other.tag == "Coin" && isDead == false)
        {
            GameManager.instance.AddScore(1);
            other.gameObject.SetActive(false);
        }
       if(other.tag == "Power Up" && isDead == false)
        {
            GameManager.instance.AddPowerTime(5);
            GameManager.instance.OnPlayerPowerup(true);
            other.gameObject.SetActive(false);
        }
       if(other.tag == "Obstacle" && isDead == false && GameManager.instance.isPowerOn == false)
        {
            Die();

        }
   }


    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            Debug.Log(isGrounded);
            jumpCount = 0;
        }
   }

   private void OnCollisionExit2D(Collision2D collision) 
    {
        // 바닥에서 벗어났음을 감지하는 처리
        Debug.Log("##OnCollisionExit2D");
        isGrounded = false;
        
   }
}