using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;


public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject skillInfoHoverPanel;
    public CanvasGroup skillInfoCG;
    public string SkillName;
    public string SkillDescription;
    public List<EnergyManager.EnergyType> energyRequirement;
    public bool CanUse = false;
    public CanvasGroup skillCG;
    public TMP_Text skillNameText;
    public TMP_Text descriptionText;
    public SkillCooldown skillCooldownSystem;



    // Start is called before the first frame update
    void Start()
    {
        skillInfoCG.alpha = 1;
        skillInfoCG.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        skillNameText.text = SkillName;
        descriptionText.text = SkillDescription;
        skillInfoHoverPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        HandleUIState();
    }


    public void CreateCooldownUI()
    {

    }


    public void Activate()
    {
        if (!CanUse) return;
        Debug.Log($"{SkillName} Activated");
        skillCooldownSystem.StartCooldown();
        EnergyManager.instance.ConsumeEnergy(energyRequirement);
        SkillManager.instance.RefreshAllSkillRequirements();
    }




    public void RefreshRequirementState()
    {
        CanUse = EnergyManager.instance.CanUseSkill(energyRequirement)
            && skillCooldownSystem.Cooldowned();
    }


    public void HandleUIState()
    {
        if (CanUse)
        {
            skillCG.alpha = 1;
            skillInfoCG.alpha = 1;
        }
        else
        {
            skillCG.alpha = .3f;
            skillInfoCG.alpha = .4f;
        }
    }


    // UI ---------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        skillInfoHoverPanel.SetActive(true);
        if (!CanUse) return;
        gameObject.transform.DOScale(
                .9f,
                .1f)
            .SetEase(Ease.InBounce);


        
        //skillInfoHoverPanel.transform
        //    .DOScale(1.1f, .1f)
        //    .SetEase(Ease.Linear);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        skillInfoHoverPanel.SetActive(false);
        if (!CanUse) return;
        gameObject.transform.DOScale(
                1f,
                .1f)
            .SetEase(Ease.OutBounce);

        

        //skillInfoHoverPanel.transform.DOScale(0f, .1f)
        //    .SetEase(Ease.Linear)
        //    .OnComplete(() =>
        //    {
        //        skillInfoHoverPanel.SetActive(false);
        //    });
    }
}
