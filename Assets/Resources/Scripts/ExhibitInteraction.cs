using UnityEngine;
using UnityEngine.Video;

public class ExhibitInteraction : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] descriptionCanvas; 
    public GameObject[] videoCanvas; 
    public VideoPlayer[] videoPlayers; // 手動拖入的所有 VideoPlayer
    public Camera mainCamera;
    public Camera infoCamera;
    private GameObject player;
    private GameObject discussionObject;
    private GameObject descriptionObject;
    private RaycastManager raycastManager;
    private ComponentDisabler componentDisabler;

    void Start()
    {
        foreach (GameObject canvas in descriptionCanvas)
        {
            canvas.SetActive(false);
        }

        foreach (GameObject canvas in videoCanvas)
        {
            canvas.SetActive(false);
        }

        infoCamera.gameObject.SetActive(false);
        raycastManager = manager.GetComponent<RaycastManager>();
        componentDisabler = manager.GetComponent<ComponentDisabler>();
    }

    public void ShowInfoCanvas(string objectName)
    {
        foreach (GameObject canvas in descriptionCanvas)
        {
            if (canvas.name == objectName)
            {
                canvas.SetActive(true);

                discussionObject = GameObject.Find("討論");
                descriptionObject = GameObject.Find("說明");
                player = GameObject.Find("PlayerArmature 1(Clone)");

                infoCamera.gameObject.SetActive(true);
                raycastManager.isInfo = true;

                discussionObject.SetActive(false);
                descriptionObject.SetActive(true);
                player.SetActive(false);
                componentDisabler.DisableComponents();

                return;
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }

    public void HideInfoCanvas()
    {
        foreach (GameObject canvas in descriptionCanvas)
        {
            canvas.SetActive(false);
        }

        player.SetActive(true);
        infoCamera.gameObject.SetActive(false);
        descriptionObject.SetActive(true);
        discussionObject.SetActive(true);
        raycastManager.isInfo = false;
        componentDisabler.EnableComponents();
    }

    private void MuteAllVideoPlayers()
    {
        foreach (VideoPlayer player in videoPlayers)
        {
            player.SetDirectAudioMute(0, true); // 靜音所有音頻
            //player.Stop(); // 停止播放
        }
    }

    public void ShowVideoCanvas(string objectName)
    {
        player = GameObject.Find("PlayerArmature 1(Clone)");
        raycastManager.isInfo = true;
        player.SetActive(false);
        componentDisabler.DisableComponents();

        for (int i = 0; i < videoCanvas.Length; i++)
        {
            if (videoCanvas[i].name == objectName)
            {
                videoCanvas[i].SetActive(true);

                if (i < videoPlayers.Length && videoPlayers[i] != null)
                {
                    // 靜音其他影片
                    MuteAllVideoPlayers();

                    // 啟用音頻並播放影片
                    Debug.Log($"Audio Track Count: {videoPlayers[i].audioTrackCount}");
                    videoPlayers[i].SetDirectAudioMute(0, false);
                    videoPlayers[i].Play();
                }
                else
                {
                    Debug.LogWarning($"No VideoPlayer found for canvas {objectName}");
                }

                return;
            }
            else
            {
                videoCanvas[i].SetActive(false);
            }
        }
    }

    public void HideVideoCanvas()
    {
        for (int i = 0; i < videoCanvas.Length; i++)
        {
            videoCanvas[i].SetActive(false);

            if (i < videoPlayers.Length && videoPlayers[i] != null)
            {
                //videoPlayers[i].Stop(); // 停止播放
                videoPlayers[i].SetDirectAudioMute(0, true); // 靜音
            }
        }

        player.SetActive(true);
        raycastManager.isInfo = false;
        componentDisabler.EnableComponents();
    }
}
