using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    PlayerControl player;
    [SerializeField] Slider HPslider;
    [SerializeField] Slider APslider;
    [SerializeField] Slider STslider;

    private void Awake()
    {
        player = transform.GetComponent<PlayerControl>();
        SetBars();
    }

    public void SetBars()
    {
        HPslider.value = player.CurHP / player.MaxHP;
        APslider.value = player.CurAp / player.MaxAp;
        STslider.value = player.CurSt / player.MaxSt;

        Debug.Log(player.CurHP);
        Debug.Log(player.CurAp);
        Debug.Log(player.CurSt);
    }    

    public void CheckHPgage()
    {
        if(HPslider != null)
        {
            HPslider.value = player.CurHP/ player.MaxHP;
        }
    }

    public void CheckAPgage()
    {
        if (APslider != null)
        {
            APslider.value = player.CurAp / player.MaxAp;
        }
    }
    public void CheckSTgage()
    {
        if (STslider != null)
        {
            STslider.value = player.CurSt / player.MaxSt;
        }
    }
}
