using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager instance { get; private set; }

    private int healthStat;
    private int currentHealth;
    private int blastBarrierStat;
    private int elementalBarrierStat;
    private int blastDamageStat;
    private int elementalDamageStat;
    private int speedStat;

    private int healthDebuff, blastBarrierDebuff, elementalBarrierDebuff, blastDamageDebuff, elementalDamageDebuff, speedDebuff = 0;
    private int healthBuff, blastBarrierBuff, elementalBarrierBuff, blastDamageBuff, elementalDamageBuff, speedBuff = 0;

    public GameObject healthUIRoot;
    public GameObject blastBarrierUIRoot;
    public GameObject elementalBarrierUIRoot;
    public GameObject blastDamageUIRoot;
    public GameObject elementalDamageUIRoot;
    public GameObject speedUIRoot;

    public TMP_Text healthText;
    public TMP_Text blastBarrierText;
    public TMP_Text elementalBarrierText;
    public TMP_Text blastDamageText;
    public TMP_Text elementalDamageText;
    public TMP_Text speedText;

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
        hoverCanvasGroup.alpha = 0;
        EventSystem.current.SetSelectedGameObject(healthText.gameObject);
        healthStat = Random.Range(0, 20);
        currentHealth = healthStat;
    }

    public void Update()
    {
        healthText.text = "" + GetHealthStat() + "/" + GetMaxHealthStat();
        blastBarrierText.text = "" + GetBlastBarrierStat();
        elementalBarrierText.text = "" + GetElementalBarrierStat();
        blastDamageText.text = "" + GetBlastStat();
        elementalDamageText.text = "" + GetElementalDamageStat();
        speedText.text = "" + GetSpeedStat();
    }


    public void HoverPanel(int i)
    {
        switch(i)
        {
            case 0:
                hoverTitle.text = "Health";
                hoverDescription.text = "Current health points, 0 = Game Over";
                break;
            case 1:
                hoverTitle.text = "Blast Barrier";
                hoverDescription.text = $"Reduces all incoming blast damage by {GetBlastBarrierStat()}";
                break;
            case 2:
                hoverTitle.text = "Elemental Barrier";
                hoverDescription.text = $"Reduces all incoming elemental damage by {GetElementalBarrierStat()}";
                break;
            case 3:
                hoverTitle.text = "Blast Damage";
                hoverDescription.text = $"Increases all incoming blast damage by {GetBlastStat()}";
                break;
            case 4:
                hoverTitle.text = "Elemental Damage";
                hoverDescription.text = $"Increases all incoming elemental damage by {GetElementalDamageStat()}";
                break;
            case 5:
                hoverTitle.text = "Speed";
                hoverDescription.text = $"Determine who moves first, the higher stat wins";
                break;
        }

        hoverCanvasGroup.alpha = 0;
        hoverCanvasGroup.DOFade(1, .2f).SetEase(Ease.Linear);
    }

    public void UnhoverPanel()
    {
        hoverCanvasGroup.DOFade(0, .2f).SetEase(Ease.Linear);
    }

    public int GetHealthStat() => Mathf.Clamp((currentHealth + healthDebuff + healthBuff), 0, 100);
    public int GetMaxHealthStat() => Mathf.Clamp((healthStat), 0, 100);
    public int GetBlastBarrierStat() => Mathf.Clamp((blastBarrierStat + blastBarrierDebuff + blastBarrierBuff), -100, 100);
    public int GetElementalBarrierStat() => Mathf.Clamp((elementalBarrierStat + elementalBarrierDebuff + elementalBarrierBuff), -100, 100);
    public int GetBlastStat() => Mathf.Clamp((blastDamageStat + blastDamageDebuff + blastDamageBuff), -100, 100);
    public int GetElementalDamageStat() => Mathf.Clamp((elementalDamageStat + elementalDamageDebuff + elementalDamageBuff), -100, 100);
    public int GetSpeedStat() => Mathf.Clamp((speedStat + speedDebuff + speedBuff), -100, 100);
}
