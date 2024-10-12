using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance { get; private set; }

    public int currentTurn;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Battle Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        currentTurn = 0;
    }

    // Use this for initialization
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}

    public void NextTurn()
    {
        SkillManager.instance.CooldownAllSkillsByOne();
        SkillManager.instance.RefreshAllSkillRequirements();
    }
}

