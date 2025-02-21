using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Dr_InvisibleControl : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public MovePointControl movePointControl;

    [SerializeField]
    private GameObject TargetObject; /// 目標位置
    [SerializeField]
    public static float EscapeSpeed = 8.0f;//逃げる速さ

    //Playerを宣言
    private GameObject Player1;
    private GameObject Player2;
    private GameObject Player3;
    private GameObject Player4;

    [SerializeField] GameObject tmpPlayer2;
    [SerializeField] GameObject tmpPlayer3;
    [SerializeField] GameObject tmpPlayer4;

    //Playerの位置を入れる変数
    [SerializeField]
    public Vector3 Player1Pos;
    [SerializeField]
    public Vector3 Player2Pos;
    [SerializeField]
    public Vector3 Player3Pos;
    [SerializeField]
    public Vector3 Player4Pos;

    //プレイヤーとの距離
    [SerializeField]
    public Vector3 toPlayer1Distance;
    [SerializeField]
    public Vector3 toPlayer2Distance;
    [SerializeField]
    public Vector3 toPlayer3Distance;
    [SerializeField]
    public Vector3 toPlayer4Distance;

    NavMeshAgent m_navMeshAgent; /// NavMeshAgent

    void Start()
    {
        //プレイヤーを見つける
        findPlayer();

        //プレイヤーとの距離を初期化
        initializeToPlayerDistance();

        // NavMeshAgentコンポーネントを取得
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        this.m_navMeshAgent.speed = EscapeSpeed;
        Debug.Log(PhotonNetwork.CountOfPlayersInRooms + 1 + "/" + PhotonNetwork.CountOfPlayers);//ルーム内の人数把握を行い、後のif文で取得するオブジェクトの数を決める

        if (PhotonNetwork.IsMasterClient)
        {
            this.transform.position = new Vector3(Random.Range(90.0f, 470.0f), 10.0f, Random.Range(130.0f, 420.0f));
            TargetObject.transform.position = this.transform.position;
        }
    }

    void Update()
    {
        //Playerの位置を入れる
        setPlayerPos();

        //プレイヤーとの距離を入れる
        setToPlayerDistance();

        // NavMeshが準備できているなら
        if (m_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid) { m_navMeshAgent.SetDestination(TargetObject.transform.position); }// NavMeshAgentに目的地をセット

        if (Catch_Doctor.BoolCatch_Doctor == true)
        {
            this.m_navMeshAgent.speed = 0f;
            Debug.Log("検知しました:"+EscapeSpeed);
        }
    }

    //プレイヤーを見つけるメソッド    
    public void findPlayer()
    {
        if (GameObject.FindWithTag("Player1-1"))
            Player1 = GameObject.FindWithTag("Player1-1");

        if (GameObject.FindWithTag("Player2"))
            Player2 = GameObject.FindWithTag("Player2");
        else
            Player2 = tmpPlayer2;

        if (GameObject.FindWithTag("Player3"))
            Player3 = GameObject.FindWithTag("Player3");
        else
            Player3 = tmpPlayer3;

        if (GameObject.FindWithTag("Player4"))
            Player4 = GameObject.FindWithTag("Player4");
        else
            Player4 = tmpPlayer4;

    }

    //プレイヤーとの距離を初期化するメソッド
    public void initializeToPlayerDistance()
    {
        toPlayer1Distance = new Vector3(0, 0, 0);
        toPlayer2Distance = new Vector3(0, 0, 0);
        toPlayer3Distance = new Vector3(0, 0, 0);
        toPlayer4Distance = new Vector3(0, 0, 0);
    }

    //Playerの位置を入れるメソッド
    public void setPlayerPos()
    {
        Player1Pos = Player1.transform.position;
        //Debug.Log("player1Pos:" + Player1Pos);
        Player2Pos = Player2.transform.position;
        Player3Pos = Player3.transform.position;
        Player4Pos = Player4.transform.position;
    }

    //プレイヤーとの距離を入れるメソッド
    public void setToPlayerDistance()
    {
        if (movePointControl.Player1In == true) { toPlayer1Distance = Player1Pos - transform.position; }
        if (movePointControl.Player2In == true) { toPlayer2Distance = Player2Pos - transform.position; }
        if (movePointControl.Player3In == true) { toPlayer3Distance = Player3Pos - transform.position; }
        if (movePointControl.Player4In == true) { toPlayer4Distance = Player4Pos - transform.position; }
    }
}
