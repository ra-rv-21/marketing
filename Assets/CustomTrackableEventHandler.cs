using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
  public bool isVideoEnabled;
  [Space]
  public VideoPlayer videoPlayer;
  public RawImage rawImage;

    protected override void Start()
    {
        base.Start();
    StartCoroutine(PrepareVideo());
    }

  private IEnumerator PrepareVideo()
  {
    videoPlayer.Prepare();

    while (!videoPlayer.isPrepared)
    {
      yield return new WaitForSeconds(.5f);
    }

    rawImage.texture = videoPlayer.texture;

    isVideoEnabled = true;
  }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        if(isVideoEnabled) videoPlayer.Play();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if(isVideoEnabled) videoPlayer.Pause();
    }
}