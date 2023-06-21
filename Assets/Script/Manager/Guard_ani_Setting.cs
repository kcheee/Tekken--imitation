using System.Collections;
using UnityEngine;

public class Guard_ani_Setting : MonoBehaviour
{
    public enum ani_state
    {
        idle,
        forwardstep,
        backstep,
        sit,
        jump,
        guard,
        H_attack,
        K_attack,
        Hit_hand,
        Hit_kick,
        Upper,
        Floating
    }

    public enum special_state
    {
        idle,
        upper
    }

    public lookat La;

    Animator ani;

    // �ִϸ����� ��Ȳ
    static public ani_state G_A_T;
    static public special_state G_S_T;

    // ����
    public AudioClip[] Audioclip;
    AudioSource soundSource;

    // guard�� gameobject�� �ƴϰ� collider
    public BoxCollider Hand_R;
    public BoxCollider Hand_L;
    public BoxCollider kick_R;
    public BoxCollider kick_L;

    // dash,backstep,sit,jump �÷���
    bool D_flag;
    bool B_flag;
    bool S_flag;
    bool J_flag;

    // ������ �Լ�
    IEnumerator delay(string S)
    {
        yield return new WaitForSeconds(0.2f);
        ani.SetBool(S, false);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        D_flag = false;
        B_flag = false;
        J_flag = false;
        S_flag = false;
    }

    // guard sound �ִϸ��̼� �̺�Ʈ�� ȣ��
    void Guard_Sound()
    {
        soundSource.clip = Audioclip[0];
        soundSource.PlayOneShot(Audioclip[0]);
    }
    void jab_sound()
    {
        //soundSource.clip = Audioclip[0];
        //soundSource.PlayOneShot(Audioclip[0]);
    }

    // hit �ִϸ��̼�, �ִϸ��̼� �̺�Ʈ�� ȣ��
    // �Ӹ� �´� �ִϸ��̼ǿ� ����
    void Head_hit()
    {
        if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.H_attack)
        {
            soundSource.clip = Audioclip[1];
            soundSource.PlayOneShot(Audioclip[1]);
        }
        if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.K_attack)
        {
            soundSource.clip = Audioclip[4];
            soundSource.PlayOneShot(Audioclip[4]);

        }
    }

    // �ߴ� �´� �ִϸ��̼�
    void Middle_hit()
    {
        if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.H_attack)
        {
            soundSource.clip = Audioclip[2];
            soundSource.PlayOneShot(Audioclip[2]);
        }
        if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.K_attack)
        {
            soundSource.clip = Audioclip[5];
            soundSource.PlayOneShot(Audioclip[5]);
        }
    }

    // �ϴ� �´� �ִϸ��̼�
    void Low_hit()
    {

    }

    void Start()
    {
        G_A_T = ani_state.idle;
        
        ani = gameObject.GetComponent<Animator>();
        soundSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log(G_A_T);
        // idle �϶��� lookat ������Ʈ ����
        if (GameManager.Gs == GameManager.Gamesetting.GameStart)
        {
            if (G_A_T == ani_state.idle ||
                G_A_T == ani_state.sit ||
                G_A_T == ani_state.forwardstep ||
                G_A_T == ani_state.backstep)
                La.enabled = true;
            else
                La.enabled = false;

            // ��밡 ���߿� �� �� lookat ���

            if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.Floating)
                La.enabled = false;

            // idle ��Ȳ�϶� ani_state => idle
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                G_A_T = ani_state.idle;
            }
            // ani.state => sit
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("sit"))
            {
                G_A_T = ani_state.sit;
            }
            // ani.state => fwd
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("fwd"))
            {
                G_A_T = ani_state.forwardstep;
            }
            // ani.state => fwd
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("bwd"))
            {
                G_A_T = ani_state.backstep;
            }

            //Debug.Log(G_A_T);
            // ���� ����϶� �ݶ��̴��� ����ִ� ���� ������Ʈ�� ����.
            if (G_A_T == ani_state.H_attack)
            {              
                Hand_R.enabled = true;
                Hand_L.enabled = true;
            }
            else if (G_A_T == ani_state.K_attack)
            {
                kick_L.enabled = true;
                kick_R.enabled = true;
            }
            else
            {
                Hand_R.enabled = false;
                Hand_L.enabled = false;
                kick_L.enabled = false;
                kick_R.enabled = false;

            }


            // ��� ���
            if (G_A_T == ani_state.backstep)
                ani.SetBool("Hit_possible", false);

            else
                ani.SetBool("Hit_possible", true);


            WalkFwd();
            WalkBwd();
            sit();
            jump();
            R_jab(); L_jab(); R_kick(); L_kick();
        }
        else
            G_A_T = ani_state.idle;
    }

    // �ִϸ��̼� events
    void False_dash()
    {
        ani.SetBool("Dash", false);
    }
    void False_backstep()
    {
        ani.SetBool("BackStep", false);
    }
    void WalkFwd()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (D_flag)
                ani.SetBool("Dash", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            G_A_T = ani_state.forwardstep;
            ani.SetBool("walkfwd", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            D_flag = true;

            StartCoroutine(Delay());
            ani.SetBool("walkfwd", false);
        }
    }
    void WalkBwd()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (B_flag)
                ani.SetBool("BackStep", true);
        }
        if (Input.GetKey(KeyCode.A))
        {

            ani.SetBool("walkbwd", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            B_flag = true;

            StartCoroutine(Delay());
            ani.SetBool("walkbwd", false);
        }
    }
    void sit()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (S_flag)
                ani.SetBool("siderwd", true);
        }
        if (Input.GetKey(KeyCode.S))
        {

            ani.SetBool("sit", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            S_flag = true;

            StartCoroutine(Delay());
            ani.SetBool("sit", false);
        }
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ani.SetBool("jump", true);
            if (J_flag)
                ani.SetBool("sidelwd", true);
            J_flag = true;
            StartCoroutine(Delay());
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ani.SetBool("jump", false);
        }


    }
    void R_jab()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(G_A_T!=ani_state.forwardstep)
            G_A_T = ani_state.H_attack;
            ani.SetBool("Jab_R", true);
            StartCoroutine(delay("Jab_R"));
        }
        if(Input.GetKey(KeyCode.I))
        {
            G_A_T = ani_state.H_attack;
        }
    }
    void L_jab()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            G_A_T = ani_state.H_attack;
            ani.SetBool("Jab_L", true);
            StartCoroutine(delay("Jab_L"));
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            G_A_T = ani_state.H_attack;
        }
    }
    void R_kick()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            G_A_T = ani_state.K_attack;
            ani.SetBool("Kick_R", true);
            StartCoroutine(delay("Kick_R"));
        }
        if (Input.GetKey(KeyCode.J))
        {
            G_A_T = ani_state.K_attack;
        }
    }
    void L_kick()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            G_A_T = ani_state.K_attack;
            ani.SetBool("Kick_L", true);
            StartCoroutine(delay("Kick_L"));
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            G_A_T = ani_state.K_attack;
        }
    }

}