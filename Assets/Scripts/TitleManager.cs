using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //スタートボタンを押した
    public void PushStartButton () {
        //ステージ選択シーンへ
        SceneManager.LoadScene ("StageSelectScene");
    }
}
