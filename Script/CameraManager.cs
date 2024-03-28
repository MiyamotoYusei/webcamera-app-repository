using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class CameraManager : MonoBehaviour
{
    [Header("Camera References")]
    [SerializeField] private int INPUT_SIZE = 256;
    [SerializeField] private int FPS = 30;
    [SerializeField] private RawImage webCamDisplay;
    private WebCamTexture webCamTexture;
    public int filterIndex = 0;

    void Awake()
    {
        WebCamInit();
    }

    private void WebCamInit()
    {
        // Webカメラのテクスチャを作成して設定
        webCamTexture = new WebCamTexture(INPUT_SIZE, INPUT_SIZE, FPS);
        // RawImageにテクスチャを設定
        webCamDisplay.texture = webCamTexture;
        // カメラ撮影開始
        webCamTexture.Play();
    }

    public void Capture()
    {
        //撮影した画像をTexture2Dに変換
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        // フィルターを適用する
        ApplyFilter(photo);

        // Webカメラを停止
        webCamTexture.Stop();

        // 画像をファイルに保存
        SaveImage(photo);
    }

    // フィルターを適用する
    private void ApplyFilter(Texture2D texture)
    {
        switch (filterIndex)
        {
            case 0:
                // フィルターなし
                break;
            case 1:
                // グレースケールに変換
                ConvertGrayscale(texture);
                break;
            case 2:
                // セピア調に変換
                ConvertSepia(texture);
                break;
            case 3:
                // ネガポジ反転
                ConvertNegative(texture);
                break;
        }
    }

    /// <summary>
    /// テクスチャをグレースケールに変換するメソッド
    /// </summary>
    /// <param name="texture">変換するテクスチャ</param>
    void ConvertGrayscale(Texture2D texture)
    {
        // テクスチャの全ピクセルを取得
        Color[] pixels = texture.GetPixels();
        // 全ピクセルをループしてグレースケールに変換
        for (int index = 0; index < pixels.Length; index++)
        {
            float grayValue = (pixels[index].r + pixels[index].g + pixels[index].b) / 3f;
            pixels[index] = new Color(grayValue, grayValue, grayValue);
        }
        // テクスチャに変換されたピクセルを設定して適用
        texture.SetPixels(pixels);
        texture.Apply();
        // テクスチャを表示する
        webCamDisplay.texture = texture;
    }

    void ConvertNegative(Texture2D texture)
    {
        // テクスチャの全ピクセルを取得
        Color[] pixels = texture.GetPixels();
        // 全ピクセルをループして画像の色を反転
        for (int index = 0; index < pixels.Length; index++)
        {
            float r = 1.0f - pixels[index].r;
            float g = 1.0f - pixels[index].g;
            float b = 1.0f - pixels[index].b;
            pixels[index] = new Color(r, g, b);
        }
        // 反転したピクセルをテクスチャに設定して適用
        texture.SetPixels(pixels);
        texture.Apply();
        // テクスチャを表示する
        webCamDisplay.texture = texture;
    }

    void ConvertSepia(Texture2D texture)
    {
        // テクスチャの全ピクセルを取得
        Color[] pixels = texture.GetPixels();
        // 全ピクセルをループして画像をセピア調に変換
        for (int index = 0; index < pixels.Length; index++)
        {
            float r = pixels[index].r;
            float g = pixels[index].g;
            float b = pixels[index].b;
        
            float tr = 0.393f * r + 0.769f * g + 0.189f * b;
            float tg = 0.349f * r + 0.686f * g + 0.168f * b;
            float tb = 0.272f * r + 0.534f * g + 0.131f * b;

            tr = Mathf.Clamp01(tr);
            tg = Mathf.Clamp01(tg);
            tb = Mathf.Clamp01(tb);

            pixels[index] = new Color(tr, tg, tb);
        }
        // セピア調に変換したピクセルをテクスチャに設定して適用
        texture.SetPixels(pixels);
        texture.Apply();
        // テクスチャを表示する
        webCamDisplay.texture = texture;
    }

    /// <summary>
    /// 画像をファイルに保存
    /// </summary>
    /// <param name="texture">保存するテクスチャ</param>
    private void SaveImage(Texture2D texture)
    {
        // ファイル名
        string fileName = "captured_image.png";
        
        // 保存先のファイルパス
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // 画像をファイルに保存
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }

    /// <summary>
    /// カメラを再初期化するメソッド
    /// </summary>
    public void Reset()
    {
        WebCamInit();
    }

    /// <summary>
    /// Webカメラの表示を反転するメソッド
    /// </summary>
    public void FlipWebCamDisplay()
    {
        GameObject.Find("WebCamDisplay").GetComponent<RectTransform>().Rotate(0, 180.0f, 0);
    }
}
