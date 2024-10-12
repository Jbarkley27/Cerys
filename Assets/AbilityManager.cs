using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance { get; private set; }

    public TMP_Text titleText;
    public TMP_Text descriptionText;

    public GameObject hoverPanel;
    public TMP_Text hoverTitle;
    public TMP_Text hoverDescription;
    public CanvasGroup hoverCanvasGroup;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Player Stat Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
       
    }

    public void Update()
    {
        
    }


    public void HoverPanel(int i)
    {
        
    }

    public void UnhoverPanel()
    {
        hoverCanvasGroup.DOFade(0, .2f).SetEase(Ease.Linear);
    }

   
}
