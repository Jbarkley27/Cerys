using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager instance { get; private set; }
    public enum EnergyType { SOLAR, ARC, FROST, VOID, POISON, BLAST, STRENGTH, HEAL };
    public List<EnergyType> deck = new List<EnergyType>();
    public List<Energy> visualDeck = new List<Energy>();

    public GameObject energyParent;
    public GameObject discardParent;
    public GameObject handParent;
    public GameObject drawPileRoot;
    public GameObject discardRoot;


    public List<Energy> energyInHand;
    public List<Energy> discardPile;
    public int maxHand;
    


    [Header("Energy Prefabs")]
    public GameObject solarEnergyPrefab, frostEnergyPrefab,
                        arcEnergyPrefab, poisonEnergyPrefab, healEnergyPrefab,
                            strengthEnergyPrefab, voidEnergyPrefab, blastEnergyPrefab;


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
        CreateInitialDeck();
        drawPileRoot.transform.localScale = new Vector3(1, 1, 1);
        drawPileRoot.SetActive(false);


        discardRoot.transform.localScale = new Vector3(1, 1, 1);
        discardRoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Create Energy ------------------------------------
    public void CreateInitialDeck()
    {
        deck = ShuffleList(deck);
        deck.ForEach((energy) => CreateEnergyGO(energy));
    }

    public void CreateEnergyGO(EnergyType type)
    {
        switch (type)
        {
            case EnergyType.SOLAR:
                CreateEnergyHelper(solarEnergyPrefab);
                break;
            case EnergyType.ARC:
                CreateEnergyHelper(arcEnergyPrefab);
                break;
            case EnergyType.FROST:
                CreateEnergyHelper(frostEnergyPrefab);
                break;
            case EnergyType.POISON:
                CreateEnergyHelper(poisonEnergyPrefab);
                break;
            case EnergyType.BLAST:
                CreateEnergyHelper(blastEnergyPrefab);
                break;
            case EnergyType.STRENGTH:
                CreateEnergyHelper(strengthEnergyPrefab);
                break;
            case EnergyType.HEAL:
                CreateEnergyHelper(healEnergyPrefab);
                break;
            case EnergyType.VOID:
                CreateEnergyHelper(voidEnergyPrefab);
                break;
        }
    }

    public void CreateEnergyHelper(GameObject go)
    {
        GameObject newEnergyGO = Instantiate(go, energyParent.transform);
        Energy newEnergy = newEnergyGO.GetComponent<Energy>();
        visualDeck.Add(newEnergy);
        newEnergy.OnCreate();
    }

    public void ConsumeEnergy(List<EnergyType> typesToRemove)
    {
        foreach (var energyType in typesToRemove)
        {
            // Find the first matching energy in the energyList and remove it
            Energy energyToRemove = energyInHand.FirstOrDefault(e => e.energyType == energyType);

            if (energyToRemove != null)
            {
                DiscardEnergyFromHand(energyToRemove);
                //energyInHand.Remove(energyToRemove);
                Debug.Log($"Removed Energy of type {energyType}");
            }
            else
            {
                Debug.Log($"No Energy of type {energyType} found to remove");
            }
        }
        //SkillManager.instance.RefreshAllSkillRequirements();

    }


    public bool CanUseSkill(List<EnergyType> energyRequirement)
    {
        List<EnergyType> energyTypesInHand = EnergyTypesInHand();
        bool metReq = true;

        energyRequirement.ForEach((energy) =>
        {
            if (!energyTypesInHand.Contains(energy)) {
                metReq = false;
                return;
            }
            
        });

        return metReq;
    }


    public List<EnergyType> EnergyTypesInHand()
    {
        List<EnergyType> convertedList = new List<EnergyType>();

        energyInHand.ForEach((energy) =>
        {
            convertedList.Add(energy.energyType);
        });

        return convertedList;
    }

    
    // Drawing Energy -----------------------------------
    public void DrawEnergy(int amount)
    {
        if (IsHandFull()) return;

        visualDeck = ShuffleVisualDeck(visualDeck);
        for (int i = 0; i < amount; i++)
        {
            
            if (!CanDrawXAmountOfEnergy())
            {
                ShuffleDeckToDrawPile();
            }

            DrawEnergyHelper();
        }
    }

    public bool IsHandFull() { return energyInHand.Count == maxHand; }

    public void ShuffleDeckToDrawPile()
    {
        for(int i = 0; i < discardPile.Count; i++)
        {
            Energy energyToRemove = discardPile[i];

            discardPile.Remove(energyToRemove);
            i--;
            visualDeck.Add(energyToRemove);
            energyToRemove.transform.SetParent(energyParent.transform);
        }
    }

    public bool CanDrawXAmountOfEnergy(int amount = 1)
    {
        return visualDeck.Count - amount >= 0;
    }

    public bool IsDeckEmpty() { return visualDeck.Count == 0; }

    public void HandleVisualDrawPile(bool turnOn)
    {
        if (turnOn)
        {
            drawPileRoot.gameObject.transform.DOScale(
                new Vector3(0, 0, 0),
                .1f).SetEase(Ease.InBounce).OnComplete(() =>
                {
                    drawPileRoot.gameObject.transform.DOScale(
                        new Vector3(1, 1, 1),
                        .1f);
                    drawPileRoot.SetActive(true);
                });
        } else
        {
            drawPileRoot.gameObject.transform.DOScale(
                new Vector3(0, 0, 0),
                .1f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    drawPileRoot.SetActive(false);
                });
                
        }
    }


    public void HandleVisualDiscardPile(bool turnOn)
    {
        if (turnOn)
        {
            discardRoot.gameObject.transform.DOScale(
                new Vector3(0, 0, 0),
                .1f).SetEase(Ease.InBounce).OnComplete(() =>
                {
                    discardRoot.gameObject.transform.DOScale(
                        new Vector3(1, 1, 1),
                        .1f);
                    discardRoot.SetActive(true);
                });
        }
        else
        {
            discardRoot.gameObject.transform.DOScale(
                new Vector3(0, 0, 0),
                .1f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    discardRoot.SetActive(false);
                });

        }
    }


    public void DrawEnergyHelper()
    {
        Energy randomEnergy = visualDeck[Random.Range(0, visualDeck.Count)];

        if (randomEnergy)
        {
            visualDeck.Remove(randomEnergy);
            energyInHand.Add(randomEnergy);
            randomEnergy.transform.SetParent(handParent.transform);
            randomEnergy.OnCreate();
        }

        SkillManager.instance.RefreshAllSkillRequirements();
    }
    
    public void DiscardEnergyFromHand(Energy energy)
    {
        energy.OnDiscard(discardParent.transform);
        energyInHand.Remove(energy);
        discardPile.Add(energy);
        SkillManager.instance.RefreshAllSkillRequirements();
    }


    // Helpers ----------------------------------------------

    public List<EnergyType> ShuffleList(List<EnergyType> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            EnergyType temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    public List<Energy> ShuffleVisualDeck(List<Energy> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Energy temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }
}

