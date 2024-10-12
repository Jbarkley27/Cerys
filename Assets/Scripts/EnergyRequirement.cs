using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class EnergyRequirement : MonoBehaviour
{
	public Color solarColor;
	public Color frostColor;
	public Color arcColor;
	public Color blastColor;
	public Color strenghtColor;
	public Color healColor;
	public Color voidColor;
	public Color poisonColor;
    public Color anyEnergyColor;
    public Skill assignedSkill;


    public GameObject energyReqPrefab;
    public CanvasGroup energyReqCG;
    public Skill skill;
    public bool CanUse;

	// Use this for initialization
	void Start()
	{
        CountEnergyTypes();

    }

	// Update is called once per frame
	void Update()
	{
        CanUse = EnergyManager.instance.CanUseSkill(skill.energyRequirement);
        energyReqCG.alpha = CanUse
                                ? 1
                                : .3f;
        
	}

	public Color GetEnergyColor(EnergyManager.EnergyType type)
	{
		switch(type)
		{
			case EnergyManager.EnergyType.ARC:
				return arcColor;
            case EnergyManager.EnergyType.BLAST:
                return blastColor;
            case EnergyManager.EnergyType.FROST:
                return frostColor;
            case EnergyManager.EnergyType.HEAL:
                return healColor;
            case EnergyManager.EnergyType.POISON:
                return poisonColor;
            case EnergyManager.EnergyType.SOLAR:
                return solarColor;
            case EnergyManager.EnergyType.STRENGTH:
                return strenghtColor;
            case EnergyManager.EnergyType.VOID:
                return voidColor;
            default:
                return anyEnergyColor;
        }

	}

    private void CountEnergyTypes()
    {
        // Use LINQ to group and count
        var energyTypeCounts = assignedSkill.energyRequirement
            .GroupBy(type => type)
            .ToDictionary(
                group => group.Key,
                group => group.Count()
            );

        // Print or use the counts
        foreach (var kvp in energyTypeCounts)
        {
            Debug.Log($"{kvp.Key} - {kvp.Value}");
            GameObject newEnergy = Instantiate(energyReqPrefab, gameObject.transform);
            newEnergy.GetComponent<Image>().color = GetEnergyColor(kvp.Key);
            newEnergy.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{kvp.Value}";
        }
    }

}

