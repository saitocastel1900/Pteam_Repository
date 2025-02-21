using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallDetection : MonoBehaviour
{
    [SerializeField] GameObject player,FadePanel;
    [SerializeField] Vector3 MoveRestXmin,MoveRestXmax,MoveRestZmin,MoveRestZmax;//移動制限の上限下限を記憶
    [SerializeField] bool findpanel=false;
    [SerializeField] Color Panelalpha;
    [SerializeField] Image FadePanelImage;
    [SerializeField] float alpha,TimerFadeOut,fadeout;
    // Start is called before the first frame update
    void Start()
    {
        MoveRestXmin.x = 84;
        MoveRestXmax.x = 460;
        MoveRestZmax.z = 424;
        MoveRestZmin.z = 108;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x <= MoveRestXmin.x|| player.transform.position.x>=MoveRestXmax.x
            || player.transform.position.z>=MoveRestZmax.z|| player.transform.position.z<=MoveRestZmin.z)
        {
            ResetPosPlayer();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("FallDetection"))
        {
            ResetPosPlayer();
        }

    }

    void Fade_Panel()
    {
        if (findpanel == false)
        {
            FadePanel = GameObject.FindWithTag("FadePanel");
            //FadePanel.GetComponent<Image>();
            FadePanelImage = FadePanel.GetComponent<Image>();
            alpha = FadePanelImage.color.a;
            findpanel = true;
        }

        FadePanelFadeIn();
        if (alpha == 255f)
        {

            TimerFadeOut += Time.deltaTime;
            ResetPosPlayer();

            if (TimerFadeOut>=0.5f)//0.5秒間画面が暗い状態
            {
                FadePanelFadeOut();
            }

        }

    }

    void FadePanelFadeIn()
    {
        if (alpha < 255f)
            alpha += Time.deltaTime * 255;
        else
            alpha = 255f;
    }
    void FadePanelFadeOut()
    {
        if (alpha > 0)
            alpha -= Time.deltaTime * 255;
        else
        {
            alpha = 0;
            TimerFadeOut = 0;
        }
    }

    void ResetPosPlayer()
    {
        player.transform.position = new Vector3(270f, 20f, 206f);
    }
}
