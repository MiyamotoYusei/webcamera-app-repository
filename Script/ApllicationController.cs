using System.Collections;
using UnityEngine;

public class ApllicationController : MonoBehaviour
{
    // サウンドマネージャーの参照
    [SerializeField] private SoundManager soundManager;
    // UIマネージャーの参照
    [SerializeField] private UIManager uIManager;
    // カメラマネージャーの参照
    [SerializeField] private CameraManager cameraManager;
    // カウントダウンの間隔（秒）
    private const float countDownDuration = 1.0f;
    // カウントダウンの開始数
    private const int countDownStartNum = 3;

    /// <summary>
    /// キャプチャーボタンが押されたときの処理
    /// </summary>
    public void ClickButtonCapture()
    {
        // 決定音再生
        soundManager.Play("決定音");
        // キャプチャーボタン非表示
        uIManager.SetCaptureButtonEnabled(false);
        // ポーズボタン表示
        uIManager.SetPauseButtonEnabled(true);
        // カウントダウンを開始
        StartCoroutine("StartCountDown");
    }

    /// <summary>
    /// カウントダウンを開始するコルーチン
    /// </summary>
    private IEnumerator StartCountDown()
    {   
        for (int i = countDownStartNum; i > 0; i--)
        {
            // カウントダウンのテキスト設定
            uIManager.CountDownSetText(i.ToString());
            // カウントダウン音再生
            soundManager.Play("カウントダウン");
            //カウントダウンの表示更新
            uIManager.countDown = true;
            //countDownDuration秒遅延
            yield return new WaitForSeconds(countDownDuration);
        }
        // カウントダウンのテキストを空に設定
        uIManager.CountDownSetText("");
        // 写真撮影
        Capture();
    }

    /// <summary>
    /// 写真を撮影する処理
    /// </summary>
    private void Capture()
    {
        // シャッター音再生
        soundManager.Play("シャッター音");
        // ポーズボタン非表示
        uIManager.SetPauseButtonEnabled(false);
        // リセットボタン表示
        uIManager.SetResetButtonEnabled(true);
        // フラッシュ表示
        uIManager.Flash();
        // カメラで写真撮影
        cameraManager.Capture();
        // ダイアログを開く
        uIManager.OpenDialog();
    }

    /// <summary>
    /// クローズボタンが押されたときの処理
    /// </summary>
    public void ClickButtonClose()
    {
        // ダイアログを閉じる
        uIManager.CloseDialog();
        // 決定音再生
        soundManager.Play("決定音");
    }

    /// <summary>
    /// フリップボタンが押されたときの処理
    /// </summary>
    public void ClickButtonFlip()
    {
        // 決定音再生
        soundManager.Play("決定音");
        // webカメラのディスプレイを左右反転
        cameraManager.FlipWebCamDisplay();
        // フリップボタンの画像を変更
        uIManager.ChageFlipButtonImage();
    }

    /// <summary>
    /// リセットボタンが押されたときの処理
    /// </summary>
    public void ClickButtonReset()
    {
        // カメラをリセット
        cameraManager.Reset();
        // 決定音再生
        soundManager.Play("決定音");
        // キャプチャーボタン表示
        uIManager.SetCaptureButtonEnabled(true);
        // リセットボタン非表示
        uIManager.SetResetButtonEnabled(false);
    }

    /// <summary>
    /// フィルターボタンが押されたときの処理
    /// </summary>
    private void ChangeFilter(int index)
    {
        // すべてのフィルターボタンの枠を非表示
        uIManager.HideAllFilterButtonFrame();
        // クリックされたフィルターボタンの枠を表示
        uIManager.ShowFilterButtonFrame(index);
        // すべてのフィルターボタンをクリック可能状態に変更
        uIManager.EnableAllFiliterButton();
        // クリックされたフィルターボタンをクリック不可状態に変更
        uIManager.DisabledFiliterButton(index);
        // 決定音再生
        soundManager.Play("決定音");
        // 撮影するフィルターの番号を設定
        cameraManager.filterIndex = index;
    }

    /// <summary>
    /// フィルターオリジナルボタンが押されたときの処理
    /// </summary>
    public void ClickButtonFilterOriginal()
    {
        ChangeFilter(0);
    }

    /// <summary>
    /// フィルターグレースケールボタンが押されたときの処理
    /// </summary>
    public void ClickButtonFilterGrayscale()
    {
        ChangeFilter(1);
    }

    /// <summary>
    /// フィルターセピアボタンが押されたときの処理
    /// </summary>
    public void ClickButtonFilterSepia()
    {
        ChangeFilter(2);
    }

    /// <summary>
    /// フィルターネガポジ反転ボタンが押されたときの処理
    /// </summary>
    public void ClickButtonFilterNegaposi()
    {
        ChangeFilter(3);
    }
}