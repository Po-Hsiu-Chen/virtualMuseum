using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;  

    public void PlayAudioForCanvas(string canvasName)
    {
        // 在Assets/Resources/Audio資料夾找到對應Canvas名稱的音訊
        AudioClip clip = Resources.Load<AudioClip>($"Audio/{canvasName}");

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"在 Assets/Resources/Audio 未找到 {canvasName} 音檔");
        }
    }
}
