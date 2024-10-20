using UnityEngine;
using UnityEngine.Video;

public class ExhibitInteraction : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] descriptionCanvas; 
    public GameObject[] videoCanvas; 
    public Camera mainCamera;
    public Camera infoCamera;
    private GameObject player;
    private GameObject discussionObject;
    private GameObject descriptionObject;
    private RaycastManager raycastManager;
    private ComponentDisabler componentDisabler;
    
    // 暫時public
    public VideoPlayer videoPlayer;

    void Start()
    {
        foreach (GameObject canvas in descriptionCanvas)
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
            }
            else
            {
                canvas.SetActive(false);
            }
        }

        discussionObject = GameObject.Find("討論");
        descriptionObject = GameObject.Find("說明");
        player = GameObject.Find("PlayerArmature 1(Clone)");

        infoCamera.gameObject.SetActive(true);
        raycastManager.isInfo = true;

        discussionObject.SetActive(false);
        descriptionObject.SetActive(true);
        player.SetActive(false);
        componentDisabler.DisableComponents();
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

    public void ShowVideoCanvas(string objectName)
    {
        player = GameObject.Find("PlayerArmature 1(Clone)");
        raycastManager.isInfo = true;
        player.SetActive(false);
        componentDisabler.DisableComponents();

        foreach (GameObject canvas in videoCanvas)
        {
            if (canvas.name == objectName)
            {
                canvas.SetActive(true);
                videoPlayer.SetDirectAudioMute(0, false);  

                return;
            }
        }
    }

    public void HideVideoCanvas()
    {
        foreach (GameObject canvas in videoCanvas)
        {
            canvas.SetActive(false);
            videoPlayer.SetDirectAudioMute(0, true); 
        }
        
        player.SetActive(true);
        raycastManager.isInfo = false;
        componentDisabler.EnableComponents();
    }
}
