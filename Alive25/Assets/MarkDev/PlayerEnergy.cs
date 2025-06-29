using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public GameObject[] playerEnergyObjList;
    // Start is called before the first frame update
    
    void Start()
    {
        int childCount = transform.childCount;
        playerEnergyObjList = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            playerEnergyObjList[i] = transform.GetChild(i).gameObject;
        }
    }

    // 设置前 num 个能量图片为启用状态，其他禁用
    public void SetPlayerEnergyImg(int num)
    {
        for (int i = 0; i < playerEnergyObjList.Length; i++)
        {
            playerEnergyObjList[i].SetActive(i < num);
        }
    }
}
