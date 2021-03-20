using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAP : MonoBehaviour
{
    public GameObject AdBtn;
    private void Start()
    {
        if (PlayerPrefs.GetInt("NoAds")==1){
            AdBtn.SetActive(false);
        }
    }
    public void RemoveAds()
    {
        Debug.Log("AdRemoved");
        PlayerPrefs.SetInt("NoAds", 1);
        AdBtn.SetActive(false);
    }
    public void UnlockZombies()
    {
        Debug.Log("UnlockZombies");
        PlayerPrefs.SetInt("AllZombies", 1);
        GetComponentInParent<GameManager>().TheP.Anim.GetComponent<Model>().Refresh();
    }
    public void UnlockCharacters()
    {
        Debug.Log("UnlockCharacters");
        PlayerPrefs.SetInt("AllCharacters", 1);
        GetComponentInParent<GameManager>().TheP.Anim.GetComponent<Model>().Refresh();
    }
}
