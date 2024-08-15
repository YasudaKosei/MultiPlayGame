using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject target;

    // 更新用の関数
    void Update()
    {

        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;
        pos.x = target.transform.position.x;    // x座標へ0.01加算
        pos.y = 0.8f;    // y座標へ0.01加算
        pos.z = target.transform.position.z;    // z座標へ0.01加算

        myTransform.position = pos;  // 座標を設定
    }

}
