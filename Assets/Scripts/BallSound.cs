using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BallSound : MonoBehaviour
{
    private AudioSource audioSource;

    // Start関数でAudioSourceコンポーネントを取得
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ボールが他の2Dオブジェクトと衝突した際に呼ばれる関数
    void OnCollisionEnter2D(Collision2D collision)
    {
        // もし衝突したオブジェクトが"Box"タグを持っているなら
        if (collision.gameObject.CompareTag("Box"))
        {
            // 音を再生
            audioSource.Play();
        }
    }
}
