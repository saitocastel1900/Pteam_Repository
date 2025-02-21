
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 lastMousePosition;
    [SerializeField] Vector3 newAngle = new Vector3(0, 0, 0);
    [SerializeField] float y_rotate, x_rotate, x_reverce, y_reverce,
        cameraX, mouseX, mouseY, tmpX = 0, tmpY = 0, boolX, boolY;
    public float CameraSensitivityX=5, CameraSensitivityY=5,GetEscKey=0;
    public static  Transform transCamera;
    public static bool TimeDelay;//falseでdelay
    public static GameObject GamePlayer;
    [SerializeField] Vector3 GamePlayerTransform;
    private string Player;


    void Start()
    {
        TimeDelay = false;
                          //newAngle = this.transform.localEulerAngles;
                          // lastMousePosition = Input.mousePosition;

        //  Camera1.name = photonView.Owner.NickName+"Camera";
        // Player1 = ""+ photonView.OwnerActorNr;

    }

    private void OnEnable()//SetActiveがtrueからfalseになったときに起動
    {

        transCamera = this.gameObject.transform;
        DontDestroyOnLoad(this.gameObject);
        
        /*if (this.gameObject.name == "Camera1")*/
        //else if (this.gameObject.name == "Camera2") GamePlayer = GameObject.Find("Player2");
        //else if (this.gameObject.name == "Camera3") GamePlayer = GameObject.Find("Player3");
        //else if (this.gameObject.name == "Camera4") GamePlayer = GameObject.Find("Player4");
        //else { Debug.Log("エラー"); }
    }

    void FixedUpdate()
    {

       //if (TimeDelay)
       //{
       if(SceneManager.GetActiveScene().name == "TestPlayScene")
        {
            if (TimeOver.gameover == false)
            {
                /*newAngle.y += (Input.mousePosition.x - lastMousePosition.x) * y_rotate * x_reverce;
                newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * x_rotate * y_reverce;
                this.gameObject.transform.localEulerAngles = newAngle;
                lastMousePosition = Input.mousePosition;*/
                //mouseX = Input.mousePosition.x* CameraSensitivityX/100;

                // mouseY = Input.mousePosition.y;
                mouseY = Input.GetAxis("Mouse Y") * CameraSensitivityY;
                mouseX = Input.GetAxis("Mouse X") * CameraSensitivityX;
                x_rotate += mouseX * Time.deltaTime * 100.0f;
                /* if (mouseX - tmpX > 0)
                 {
                     boolX = 1;
                     tmpX = mouseX;
                 }
                 else if (mouseX - tmpX < 0)
                 {
                     boolX = -1;
                     tmpX = mouseX;
                 }
                 else
                     boolX = 0;*/

                cameraX = Mathf.Clamp(cameraX - mouseY * Time.deltaTime * 100.0f, -40, 40);
                transCamera.localEulerAngles = new Vector3(cameraX, x_rotate, 0);
            }
        }
            
       //}
        //修正
        if (RoomSceneManager2.SceneEnter==true)//ゲームシーンに入る前(カメラを取得する前からカメラをあること前提に動いているのでif追加)
        {
            if (SceneManager.GetActiveScene().name == "TestPlayScene")
            {
                GamePlayer = GameObject.Find("Player" + RoomSceneManager.Porder + "(Clone)");
                GamePlayerTransform = GamePlayer.transform.position;//エラー1
                transCamera.position = GamePlayerTransform + new Vector3(0, 2.25f, 0);
            }
        }
            
        
    }

}
