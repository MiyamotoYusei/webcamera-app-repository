using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// SEを再生
    /// </summary>
    /// <param name="seName">再生したいSEのファイル名（Resources/SE/のファイル名）</param>
    public void Play(string seName)
    {
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("SE/" + seName), transform.position);
    }
}