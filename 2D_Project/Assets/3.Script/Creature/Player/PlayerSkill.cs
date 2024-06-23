using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    /*
    <필수 기능>
    1. 쿨타임
     */

    Animator Ani;
    Movement2D movement2D;
    SpriteRenderer Sprite;
    MouseControl mouse;
    PlayerControl player;
    PlayerBar playerBar;
    GameObject[] Shadows;
    TraceShadow[] shadowMethods;
    SpriteRenderer[] ShadowSprite;
    [SerializeField]Image skillPic;
    Color rightColor;
    Color setColor;

    private void Awake()
    {
        Ani = GetComponent<Animator>();
        movement2D = GetComponent<Movement2D>();
        Sprite = GetComponent<SpriteRenderer>();
        mouse = GetComponent<MouseControl>();
        player = GetComponent<PlayerControl>();
        Shadows = GameObject.FindGameObjectsWithTag("Shadow");
        shadowMethods = new TraceShadow[Shadows.Length];
        ShadowSprite = new SpriteRenderer[Shadows.Length];
        playerBar = GetComponent<PlayerBar>();
        rightColor = skillPic.color;
        setColor = rightColor;
        setColor.g = 0f;
        setColor.b = 0f;

        for (int i = 0; i < 4; i++)
        {
            if (Shadows[i] == null)
            {
                Debug.Log("그림자가 할당 X");
                break;
            }
            shadowMethods[i] = Shadows[i].GetComponent<TraceShadow>();
            ShadowSprite[i] = Shadows[i].GetComponent<SpriteRenderer>();
        }
    }
    public bool test = true;

    private void Update()
    {
        if (player.CurAp >= 50)
        {
            skillPic.color = rightColor;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(Getmousebutton());
            }
        }

        else
        {
            skillPic.color = setColor;
        }
    }
    public bool isDashSlice = false;
    private IEnumerator TryDashSlice()
    {
        isDashSlice = true;

        for (int i = 0; i < 4; i++)
        {
            shadowMethods[i].OnShadow();
        }

        WaitForSeconds wfs = new WaitForSeconds(0.2f);
        float skilldistance = 10f;
        test = false;



        Ani.SetTrigger("Attack");
        Sprite.flipX = true;
        movement2D.Move_Speed = skilldistance;
        movement2D.MoveTo(new Vector3(1, 0, 0));
        yield return wfs;
        movement2D.Move_Speed = player._NormalSpeed;

        Ani.SetTrigger("Attack");
        movement2D.Move_Speed = skilldistance;
        movement2D.MoveTo(new Vector3(-1, 1, 0));
        yield return wfs;
        movement2D.Move_Speed = player._NormalSpeed;

        Color[] NormalColors = new Color[4];
        for (int i = 0; i < Shadows.Length; i++)
        {
            NormalColors[i] = ShadowSprite[i].color;
        }

        for (int i = 0; i < 4; i++)
        {
            Color newColor = Color.HSVToRGB(240f * 0.0028f, 100f * 0.01f, 100f * 0.01f);
            newColor.a = 0.2f;
            ShadowSprite[i].color = newColor;

        }

        Ani.SetTrigger("Attack");
        Sprite.flipX = false;
        movement2D.Move_Speed = skilldistance;
        movement2D.MoveTo(new Vector3(-1, -1, 0));
        yield return wfs;
        movement2D.Move_Speed = player._NormalSpeed;

        for (int i = 0; i < 4; i++)
        {
            ShadowSprite[i].color = NormalColors[i];
        }

        Ani.SetTrigger("Attack");
        movement2D.Move_Speed = skilldistance;
        movement2D.MoveTo(new Vector3(1, -1, 0));
        yield return wfs;

        for (int i = 0; i < 4; i++)
        {
            Color newColor = Color.HSVToRGB(240f * 0.0028f, 100f * 0.01f, 100f * 0.01f);
            newColor.a = 0.2f;
            ShadowSprite[i].color = newColor;
        }

        Ani.SetTrigger("Attack");
        Sprite.flipX = true;
        movement2D.Move_Speed = skilldistance;
        movement2D.MoveTo(new Vector3(0, 1, 0));
        yield return wfs;

        for (int i = 0; i < 4; i++)
        {
            ShadowSprite[i].color = NormalColors[i];
            shadowMethods[i].OffShadow();
        }

        Ani.SetTrigger("Idle");
        movement2D.Move_Speed = player._NormalSpeed;
        isDashSlice = false;
    }

    private float SkillCost = 50f;

    private IEnumerator Getmousebutton()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.001f);

        while (!Input.GetMouseButtonDown(0))
        { 
            yield return wfs;
        }

        player.CurAp -= SkillCost;
        playerBar.CheckAPgage();
        player.isGageCharge[(int)GageName.AP] = true;

        StartCoroutine(TryDashSlice());
    }
}
