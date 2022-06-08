using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class admob : MonoBehaviour
{
    private RewardedAd rwa;
    // Start is called before the first frame update
    public admob()
    {
        string adUnitId;
#if UNITY_ANDROID
                    adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
                    adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitId = "unexpected_platform";
#endif

        rwa = new RewardedAd(adUnitId);
    }

    public bool ads()
    {
        loadReward();
        return showReward();
    }

    private bool showReward()
    {
        if (rwa.IsLoaded())
        {
            rwa.Show();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void loadReward()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rwa.LoadAd(request);
    }
}
