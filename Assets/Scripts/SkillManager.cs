using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
	public List<Skill> equippedSkills = new List<Skill>();
    public static SkillManager instance { get; private set; }
    public GameObject skillUIParent;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Energy Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
	{
        equippedSkills.Clear();

        // Loop through all child transforms of the parent
        foreach (Transform child in skillUIParent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                equippedSkills.Add(child.GetComponent<Skill>());
            }
        }
    }

	// Update is called once per frame
	void Update()
	{
			
	}

	public void RefreshAllSkillRequirements()
	{
		equippedSkills.ForEach((skill) =>
		{
			skill.RefreshRequirementState();
		});
	}

	public void CooldownAllSkillsByOne()
	{
        equippedSkills.ForEach((skill) =>
        {
            skill.skillCooldownSystem.CooldownNextElement();
        });
    }
}

