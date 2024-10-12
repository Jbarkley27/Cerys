using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    public int maxSkillCooldown;
    public Skill skill;
    public List<Image> cooldownUIElements;
    public Color disabledColor;
    public Color enabledColor;
    public int currentCooldownedElements;
    public CanvasGroup cooldownCanvasGroup;

    // Use this for initialization
    void Start()
	{
        // Loop through all child transforms of the parent
        foreach (Transform child in gameObject.transform)
        {
            cooldownUIElements.Add(child.GetComponent<Image>());
        }

        cooldownUIElements.ForEach((image) =>
        {
            image.color = enabledColor;
        });

        maxSkillCooldown = cooldownUIElements.Count;
        currentCooldownedElements = maxSkillCooldown - 1;
    }

	// Update is called once per frame
	void Update()
	{
        cooldownCanvasGroup.alpha =
                Cooldowned() ? 1 : .3f;

    }

    public void StartCooldown()
    {
        currentCooldownedElements = -1;

        cooldownUIElements.ForEach((image) =>
        {
            image.color = disabledColor;
        });
    }

    public void CooldownNextElement()
    {
        if (currentCooldownedElements + 1 >= maxSkillCooldown) return;
        currentCooldownedElements++;
        cooldownUIElements[currentCooldownedElements].color = enabledColor;
    }

    public void AddToCooldown()
    {
        cooldownUIElements[currentCooldownedElements].color = disabledColor;
        currentCooldownedElements--;
    }

    public bool Cooldowned()
    {
        return (currentCooldownedElements + 1) == maxSkillCooldown;
    }
}

