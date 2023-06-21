using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_check : StateMachineBehaviour
{
    public bool guard;
    public bool maskman;
    public bool floatingState;
    public GameObject Maskman;
    float po_X;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        po_X = animator.transform.position.x; 
    }
    // floating ������ �� lookat ������Ʈ ��Ȱ��ȭ �ϱ� ���� ����
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 Po = animator.transform.position;
        Po.x = po_X;
        animator.transform.position = Po;
        if (guard)
        {
            floatingState = true;
            Guard_ani_Setting.G_A_T = Guard_ani_Setting.ani_state.Floating;
        }
        if (maskman)
        {
            floatingState = true;
            Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.Floating;
        }

    }

    // �ִϸ��̼� ���� �� ������
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (guard)
        {
            floatingState = false;
            Guard_ani_Setting.G_A_T = Guard_ani_Setting.ani_state.idle;
        }
        if (maskman) floatingState = false;
        {
            floatingState = false;
            Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.idle;
        }
    }
}