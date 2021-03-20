using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
public class GAds : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private BannerView bannerView;
    public float RewardedFloat;
    public int RewardAmount;
    public bool IsTest;
    public GameObject OpenRewardedAd;
    public string InterString;
    public string RewardedString;
    public string BannerString;
    public string InterStringIOS;
    public string RewardedStringIOS;
    public string BannerStringIOS;
    string gameId = "3780999";
    string myPlacementId = "rewardedVideo";
    public bool IsEditor;
    public GameObject RewardedBtn;
    public GameObject NoAdShow;
    // Start is called before the first frame update
    void Awake()
    {
        IsEditor = Application.isEditor;
       
        if (IsEditor)
        {
            IsTest = true;
        }
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        RequestRewarded();
        if (PlayerPrefs.GetInt("NoAds") == 0)
        {
            //RequestBanner();
            if (PlayerPrefs.GetInt("OfflineAd") == 1)
            {
                ShowInterstitial();
            }
        }
       

        //StopBanner();

    }
    private void Update()
    {
        NoAdShow.SetActive(!this.rewardedAd.IsLoaded());
        if (GetComponent<GameManager>().TheP.IsDead)
        {
            RewardedBtn.GetComponent<Button>().interactable = this.rewardedAd.IsLoaded();

        }
    }
    public void ShowInterDelayed(float TimeM)
    {
        StartCoroutine(ShowTheAd(TimeM));
    }
   IEnumerator ShowTheAd(float Time)
    {
        yield return new WaitForSeconds(Time);
        ShowInterstitial();
    }
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId;
        if (IsTest)
        {
            adUnitId = "ca-app-pub-3940256099942544/6300978111";

        }
        else
        {
            //adUnitId = "ca-app-pub-9771432649146144/4129547936";
            adUnitId = BannerString;
        }
#elif UNITY_IPHONE
        string adUnitId;
        if (IsTest)
        {
            adUnitId = "ca-app-pub-3940256099942544/6300978111";

        }
        else
        {
            //adUnitId = "ca-app-pub-9771432649146144/4129547936";
            adUnitId = BannerStringIOS;
        }
#else
            string adUnitId = "unexpected_platform";
#endif
        AdSize adSize = new AdSize(300, 50);
       // BannerView bannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, adSize, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
    public void StopBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
    private void RequestRewarded()
    {

#if UNITY_ANDROID
        string adUnitId;
        if (IsTest)
        {
            adUnitId = "ca-app-pub-3940256099942544/5224354917";

        }
        else
        {
            //adUnitId = "ca-app-pub-9771432649146144/4786669814";
            adUnitId = RewardedString;
        }
#elif UNITY_IPHONE
        string adUnitId;
        if (IsTest)
        {
            adUnitId = "ca-app-pub-3940256099942544/5224354917";

        }
        else
        {
            //adUnitId = "ca-app-pub-9771432649146144/4786669814";
            adUnitId = RewardedStringIOS;
        }
#else
        string adUnitId = "unexpected_platform";
#endif
        this.rewardedAd = new RewardedAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);



        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId;
        if (IsTest)
        {
            adUnitId = "ca-app-pub-3940256099942544/1033173712";

        }
        else
        {
           // adUnitId = "ca-app-pub-9771432649146144/6947282961";
            adUnitId = InterString;
        }
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712";
       // string adUnitId = "ca-app-pub-9771432649146144/3691810107";
#elif UNITY_IPHONE
        string adUnitId;
        if (IsTest)
        {
            adUnitId = "ca-app-pub-3940256099942544/1033173712";

        }
        else
        {
            // adUnitId = "ca-app-pub-9771432649146144/6947282961";
            adUnitId =InterStringIOS;
        }
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        if (PlayerPrefs.GetInt("NoAds") == 1)
        {
          //  GetComponent<IAP>().NoAdsBtn.SetActive(false);
        }
        

    }
    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("NoAds") == 0)
        {
            RequestBanner();
        }
    }
  
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            RequestInterstitial();
        }
        if (!IsEditor)
        {
            if (PlayerPrefs.GetInt("NoAds") == 0)
            {
                //if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
               // {
                   // Appodeal.showTestScreen();
                  //  Appodeal.show(Appodeal.INTERSTITIAL);
                //}
               // else
                
              
            }
        }
        else
        {
           
        }

    }
    public void ShowRewarded()
    {
      
       // RewardAmount = GetComponent<GameManager>().CoinsCollected;
        if (!IsEditor)
        {
           
            if (this.rewardedAd.IsLoaded())
            {
                this.rewardedAd.Show();
            }
            else
            {
              
                ShowRewardedVideo();
            }
            RequestRewarded();

        }
        else
        {
            ShowRewardedVideo();
        }
       

    }
    public void onRewardedVideoFinished()
    {
      
        Debug.Log("AppodealRewardedSuccess");
    }
    public void ShowRewardedVideo()
    {
        Debug.Log("ShowRewarded");
    }
   
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
       // MonoBehaviour.print(
          //  "HandleRewardedAdFailedToLoad event received with message: "
                  //           + args.Message);
       // GetComponent<PopUp>().Header[5] = "Watch Ads";
       // GetComponent<PopUp>().Description[5] = "Ad Failed";
      //  GetComponent<PopUp>().PopupEnabled(5);

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
       

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardedAdClosed event received");
       //GetComponent<PopUp>().Header[5] = "Watch Ads";
        //GetComponent<PopUp>().Description[5] = "Ad Skipped";
        //GetComponent<PopUp>().PopupEnabled(5);
       // Debug.Log("UnityAdRewardSkip");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        RewardedFloat = Convert.ToSingle(amount);
        if (GetComponent<GameManager>().TheP.IsDead)
        {
            GetComponent<GameManager>().Revive();
        }
        else
        {
            GetComponent<GameManager>().AdContinueWatched();
        }
      

    }

    public event EventHandler<EventArgs> OnAdLoaded;

    public event EventHandler<AdErrorEventArgs> OnAdFailedToLoad;

    public event EventHandler<AdErrorEventArgs> OnAdFailedToShow;

    public event EventHandler<EventArgs> OnAdOpening;

    public event EventHandler<EventArgs> OnAdClosed;

    public event EventHandler<Reward> OnUserEarnedReward;

    #region Rewarded Video callback handlers
    public void onRewardedVideoLoaded(bool isPrecache) { print("Video loaded"); } //Called when rewarded video was loaded (precache flag shows if the loaded ad is precache).
    public void onRewardedVideoFailedToLoad() { print("Video failed"); } // Called when rewarded video failed to load
    public void onRewardedVideoShowFailed() { print("Video show failed"); } // Called when rewarded video was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative)
    public void onRewardedVideoShown() { onRewardedVideoFinished(); print("Video shown"); } // Called when rewarded video is shown
    public void onRewardedVideoClicked() { print("Video clicked"); } // Called when reward video is clicked
    public void onRewardedVideoClosed(bool finished) { print("Video closed"); } // Called when rewarded video is closed
    public void onRewardedVideoFinished(double amount, string name) { print("Reward: " + amount + " " + name); } // Called when rewarded video is viewed until the end
    public void onRewardedVideoExpired() { print("Video expired"); } //Called when rewarded video is expired and can not be shown


    #endregion
}

