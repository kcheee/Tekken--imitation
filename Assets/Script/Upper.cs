using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upper : MonoBehaviour
{
    IEnumerator ExecuteForDuration(float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            // ���� �ð� ���� ����� �ڵ�

           // �̸��� ���尡 ���ԵǸ� ������ ���¸� ���������� �ٲ۴�.
            if (gameObject.name.Contains("Guard"))
            {
                Guard_ani_Setting.G_S_T = Guard_ani_Setting.special_state.upper;
                
            }
            if (gameObject.name.Contains("Maskman"))
            {
                Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.H_attack;
                Debug.Log("maskman");
            }

            // �ð� ������Ʈ
            timer += Time.deltaTime;
            
            yield return null;           
        }
        
        // ������ ���Ŀ� hand attack �� �� �����ƵǴ� �� ����.
        yield return new WaitForSeconds(0.1f);
        Guard_ani_Setting.G_S_T = Guard_ani_Setting.special_state.idle;
        Maskman_ani_Setting.M_S_T = Maskman_ani_Setting.special_state.idle;
    }
    void Upper_check()
    {
        StartCoroutine(ExecuteForDuration(1f));
    }
}