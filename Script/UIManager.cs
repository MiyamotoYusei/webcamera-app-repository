using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image captureButtonImage;
    [SerializeField] private Image pauseButtonImage;
    [SerializeField] private Image resetButtonImage;
    [SerializeField] private Image flipButtonImage;
    [SerializeField] private Image countDownCircle;
    [SerializeField] private Animator dialogPanelAnimator;
    [SerializeField] private Animator flashDisplay;
    [SerializeField] private CanvasGroup dialogPanelCanvasGroup;
    [SerializeField] private List<Image> FilterButtonFrameList = new List<Image>();
    [SerializeField] private List<Button> filterButtonList = new List<Button>();
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI resultMessage;
    public bool countDown = false;

    /// <summary>
    /// カウントダウンに指定された文字列を設定
    /// </summary>
    /// <param name="text">設定したい文字列</param>
    public void CountDownSetText(string text)
    {
        countDownText.SetText(text);
    }

    /// <summary>
    /// キャプターボタンを【表示・有効/非表示・無効】を設定
    /// </summary>
    /// <param name="enabled">【表示・有効】の場合はtrue、【非表示・無効】の場合はfalse</param>
    public void SetCaptureButtonEnabled(bool enabled)
    {
        captureButtonImage.enabled = enabled;
    }

    /// <summary>
    /// ポーズボタンを【表示・有効/非表示・無効】を設定
    /// </summary>
    /// <param name="enabled">【表示・有効】の場合はtrue、【非表示・無効】の場合はfalse</param>
    public void SetPauseButtonEnabled(bool enabled)
    {
        pauseButtonImage.enabled = enabled;
    }

    /// <summary>
    /// リセットボタンを【表示・有効/非表示・無効】を設定
    /// </summary>
    /// <param name="enabled">【表示・有効】の場合はtrue、【非表示・無効】の場合はfalse</param>
    public void SetResetButtonEnabled(bool enabled)
    {
        resetButtonImage.enabled = enabled;
    }

    private void Update()
    {
        UpdateCountdown();
    }

    /// <summary>
    /// カウントダウンのサークルを1周させる処理
    /// </summary>
    private void UpdateCountdown()
    {
        if(countDown && countDownCircle.fillAmount < 0.98f) 
        {
            countDownCircle.fillAmount += Time.deltaTime;
        } else 
        {
            countDownCircle.fillAmount = 0f;
            countDown = false;
        }
    }

    /// <summary>
    /// flashDisplayの色を白に設定
    /// </summary>
    public void Flash()
    {
        flashDisplay.SetTrigger("Flash");
    }

    /// <summary>
    /// 全てのフィルターボタンの枠を非表示に設定
    /// </summary>
    public void HideAllFilterButtonFrame()
    {
        foreach (Image image in FilterButtonFrameList)
        {
            image.enabled = false;
        }
    }

    /// <summary>
    /// フィルターボタンのフレームを表示に設定
    /// </summary>
    /// <param name="index">表示するボタンの番号</param>
    public void ShowFilterButtonFrame(int index)
    {
        FilterButtonFrameList[index].enabled = true;
    }

    /// <summary>
    /// 全てのフィルターボタンを【クリック可能】に設定
    /// </summary>
    public void EnableAllFiliterButton()
    {
        foreach (Button button in filterButtonList)
        {
            button.interactable = true;
        }
    }

    /// <summary>
    /// フィルターボタンを【クリック不可】に設定
    /// </summary>
    /// /// <param name="index">クリック不可にするボタンの番号</param>
    public void DisabledFiliterButton(int index)
    {
        filterButtonList[index].interactable = false;
    }

    /// <summary>
    /// フリップボタンの画像の切り替え
    /// </summary>
    public void ChageFlipButtonImage()
    {
        if(flipButtonImage.sprite == Resources.Load<Sprite>("Image/FlipButton1"))
        {
            flipButtonImage.sprite = Resources.Load<Sprite>("Image/FlipButton2");
        } 
        else
        {
            flipButtonImage.sprite = Resources.Load<Sprite>("Image/FlipButton1");
        }
    }

    /// <summary>
    /// ダイアログを開く
    /// </summary>
    public void OpenDialog()
    {
        dialogPanelAnimator.SetTrigger("DialogOpen");
        dialogPanelCanvasGroup.interactable = true;
        dialogPanelCanvasGroup.blocksRaycasts = true;
        // ファイル名
        string fileName = "captured_image.png";
        // 保存先のファイルパス
        string filePath = Application.persistentDataPath;
        resultMessage.SetText("●保存場所\n" + filePath + "\n" + "●ファイル名\n" + fileName);
    }

    /// <summary>
    /// ダイアログを閉じる
    /// </summary>
    public void CloseDialog()
    {
        dialogPanelAnimator.SetTrigger("DialogClose");
        dialogPanelCanvasGroup.interactable = false;
        dialogPanelCanvasGroup.blocksRaycasts = false;
    }
}