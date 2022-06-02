using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;
    public float MaxSpeed = 5;
    public Rigidbody2D HeroBody;
    [HideInInspector]
    public bool bFaceRight = true;
    public bool bJump = false;
    public float JumpForce = 100;
    private Transform mGrondCheck;
    public AudioClip[] JumpClips;
    public AudioSource audioSource;
    public Animator anim;

    float mVolume = 0;
    public AudioMixer audioMixer;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();
        mGrondCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //控制速度
        if(Mathf.Abs(HeroBody.velocity.x)<MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }
        if (Mathf.Abs(HeroBody.velocity.x) >5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);
        }

        anim.SetFloat("speed", Mathf.Abs(h));

        //角色旋转
        if (h > 0&&!bFaceRight)
        {
            flip();
        }
        else if(h<0&&bFaceRight)
        {
            flip();
        }



        //角色跳跃
        if (Physics2D.Linecast(transform.position, mGrondCheck.position, 1<<LayerMask.NameToLayer("Ground")))
        {
            if(Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }

    }
    
    private void flip()
        {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
        }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            mVolume++;
            audioMixer.SetFloat("MasterVolume", mVolume);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mVolume--;
            audioMixer.SetFloat("MasterVolume", mVolume);
        }
        if (bJump)
        {
            int i = Random.Range(0, JumpClips.Length);
            // AudioSource.PlayClipAtPoint(JumpClips[i], transform.position);
            audioSource.clip = JumpClips[i];
            audioSource.Play();
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
            anim.SetTrigger("jump");

        }


    }
}
