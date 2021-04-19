using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
  public bool isVideoEnabled;
  public string url;

  [Space]
  public GameObject videoGO;
  private VideoPlayer _videoPlayer;
  private RawImage _rawImage;

  private Button _interactionBtn;



  private void Awake(){
    _interactionBtn = videoGO.GetComponent<Button>();
    _videoPlayer = videoGO.GetComponent<VideoPlayer>();
    _rawImage = videoGO.GetComponent<RawImage>();
  }

    protected override void Start()
    {
        base.Start();

        _interactionBtn.onClick.AddListener(OpenUrl);

    StartCoroutine(PrepareVideo());
    }

  private IEnumerator PrepareVideo()
  {
    _videoPlayer.Prepare();

    while (!_videoPlayer.isPrepared)
    {
      yield return new WaitForSeconds(.5f);
    }

    _rawImage.texture = _videoPlayer.texture;

    isVideoEnabled = true;
  }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        if(isVideoEnabled) _videoPlayer.Play();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if(isVideoEnabled) _videoPlayer.Pause();
    }

    private void OpenUrl(){
      Application.OpenURL(url);
    }
}