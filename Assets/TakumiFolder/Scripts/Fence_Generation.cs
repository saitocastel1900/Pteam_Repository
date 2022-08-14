using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence_Generation : MonoBehaviour
{
    [SerializeField] GameObject fence;
    [SerializeField] GameObject StageOb;
    private int fence_num1 = 0;//時間が無かったので雑　後で配列にしておきます
    private int fence_num2 = 0;
    private int fence_num3 = 0;
    // Start is called before the first frame update
    void Start()
    {
        var parent = StageOb.transform;
        while (fence_num1 < 31)
        {
            Instantiate(fence, new Vector3(86.23f + fence_num1 * 6.45f, 11.67f, 108.9f), Quaternion.identity,parent);
            fence_num1++;
        }
        while (fence_num2 < 60)
        {
            Instantiate(fence, new Vector3(86.23f + fence_num2 * 6.45f, 11.67f, 424.8f), Quaternion.identity, parent);
            fence_num2++;
        }
        while (fence_num3 < 48)
        {
            Instantiate(fence, new Vector3(84.1f , 11.67f, 118.7f + fence_num3* 6.45f), Quaternion.Euler(0,90,0), parent);
            fence_num3++;
        }
        Debug.Log("柵が作られる");
       
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
