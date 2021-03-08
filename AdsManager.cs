using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string GooglePlay_ID = "4037201";
    string rewardedVideo_ID = "rewardedVideo";
    bool testMode = false;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(GooglePlay_ID, testMode);
    }

    public void DisplayAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Ad not ready");
        }
    }

    public void DisplayRewardedAd()
    {
        if (Advertisement.IsReady(rewardedVideo_ID))
        {
            Advertisement.Show(rewardedVideo_ID);
        }
        else
        {
            Debug.Log("Ad not ready");
        }

    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("Ad Finished");
            GameManager.Instance.player.AddDiamonds(100);
            UIManager.Instance.OpenShop(GameManager.Instance.player.diamonds);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Ad Skipped");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == rewardedVideo_ID)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
