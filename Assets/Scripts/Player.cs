using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //インプットシステム
    PlayerInput playerInput;

    //左右用列挙体...あまりやる必要はないがインスペクターからの見やすさのために用意
    enum FlipBool {
        Left = -1,Right = 0
    }

    //設定
    [SerializeField, Header("初期設定が右向きならRightに設定")] private FlipBool FlipSprite_ = FlipBool.Right;
    [SerializeField, Header("アニメーター")] private Animator animator;
    [SerializeField, Header("シールド")] private GameObject shield_;
    [SerializeField, Header("残機")] private const int PlayerLife_ = 5;
    private int Life_ = 5;
    private const float IdleTime_ = 0.2f;
    private float Idle_cool = IdleTime_;

    //外部機能参照
    [SerializeField, Header("GameManager")] private GameManager GameManager_;
    

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        shield_.SetActive(false);
    }

    void Update()
    {
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        if(move.x != 0)
        {
            if (!shield_.activeSelf)
            {
                shield_.SetActive(true);
            }
            Idle_cool = IdleTime_;
            if (move.x > 0) {
                FlipSprite_ = FlipBool.Right;
            }
            else {
                FlipSprite_ = FlipBool.Left;
            }

            transform.rotation = Quaternion.Euler(0, 180 * (int)(FlipSprite_), 0);
            animator.SetBool("Guard", true);
        }
        else
        {
            if (Idle_cool > 0) {
                Idle_cool -= Time.deltaTime;
            }
            else {
                shield_.SetActive(false);
                animator.SetBool("Guard", false);
            }
        }

        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            animator.SetTrigger("Death");
            GameManager_.Death_player(Reset_Player);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("被弾");

            if (Life_ > 0) {
                Life_--;
                animator.SetTrigger("Damage");
            }
            else
            {
                animator.SetTrigger("Death");
                GameManager_.Death_player(Reset_Player);
            }
            
        }
    }

    private void Reset_Player()
    {
        Life_ = PlayerLife_;
        animator.SetTrigger("Reset");
    }
}
