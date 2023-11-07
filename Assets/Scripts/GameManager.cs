using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StageInfo
{
    public Vector2 ballStartPosition; // 3D位置で保存
}


public class GameManager : MonoBehaviour
{

    public int StageNo;             //ステージナンバー

    public bool isBallMoving;       //ボールが移動中か否か

    public GameObject ballPrefab;   //ボールプレハブ
    public GameObject ball;         //ボールオブジェクト

    public GameObject goButton;     //ボタン：ゲーム開始
    public GameObject retryButton;  //ボタン：リトライ
    public GameObject clearText;    //テキスト：クリア

    public AudioClip clearSE;       //効果音：クリア
    private AudioSource audioSource;//オーディオソース

    // Start is called before the first frame update
    void Start()
    {
        retryButton.SetActive (false);  //リトライボタンを非表示
        isBallMoving = false;           //ボールは移動中ではない

        //オーディオソースを取得
        audioSource = gameObject.GetComponent<AudioSource> ();

        //プレイヤーの進行度を一時的に削除するもの(使用後にコメントに戻す)
        //PlayerPrefs.DeleteAll();
    }

    //バックボタンを押した
    public void PushBackButton () {
        GobackStageSelect ();
    }

    //ステージクリア処理
    public void StageClear () {
         if (audioSource && clearSE) {
        audioSource.PlayOneShot(clearSE);
        }
        
        //audioSource.PlayOneShot (clearSE);//クリア音再生

        //セーブデータ更新
        if (PlayerPrefs.GetInt ("CLEAR", 0) < StageNo) {
            //セーブされているステージNoより今のステージNoが大きければ
            PlayerPrefs.SetInt ("CLEAR", StageNo); //ステージNoを記録
        }
        clearText.SetActive (true);      //クリア表示
        retryButton.SetActive (false);   //リトライボタン非表示

        //2秒後に自動的にステージセレクト画面へ
        Invoke ("GobackStageSelect", 2.0f);
    }

    //移動処理
    void GobackStageSelect () {
        SceneManager.LoadScene ("StageSelectScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //GOボタンを押した
    public void PushGoButton () {
        //ボールの重力を有効化
        Rigidbody2D rd = ball.GetComponent<Rigidbody2D>();
        rd.isKinematic = false;

        retryButton.SetActive (true);   //リトライボタンを表示
        goButton.SetActive (false);     //GOボタンを非表示
        isBallMoving = true;            //ボールは移動中
    }

    //リトライボタンを押した
    public void PushRetryButton () {
        Destroy (ball);                 //ボールオブジェクトを削除

        //プレハブより新しいボールオブジェクトを作成
        ball = (GameObject)Instantiate (ballPrefab);

        retryButton.SetActive (false);  //リトライボタンを表示
        goButton.SetActive (true);      //GOボタンを表示
        isBallMoving = false;           //ボールは「移動中ではない」
        
        if(StageNo >= 0 && StageNo < stageInfos.Count) {
            ball.transform.position = stageInfos[StageNo].ballStartPosition;
            } else {
            Debug.LogError("Invalid StageNo: " + StageNo);
        }

        ball.transform.position = stageInfos[StageNo].ballStartPosition;
        //ボールの初期位置をstageInfosから取得

    }

    public List<StageInfo> stageInfos;

}
