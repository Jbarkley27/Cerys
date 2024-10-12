using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof (CanvasGroup))]
public class Energy : MonoBehaviour, IPointerClickHandler
{
    public EnergyManager.EnergyType energyType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreate()
    {
        gameObject.transform.DOScale(
                    new Vector3(0, 0, 0),
                    .1f).SetEase(Ease.InBounce).OnComplete(() => 
                        gameObject.transform.DOScale(
                                    new Vector3(1, 1, 1),
                                    .2f).SetEase(Ease.OutBounce)
            );
    }

    public void OnDiscard(Transform parent)
    {
        gameObject.transform.DOScale(
                    new Vector3(0, 0, 0),
                    .2f).SetEase(Ease.InBounce).OnComplete(() =>
                    {
                        gameObject.transform.DOScale(
                                   new Vector3(1, 1, 1),
                                   .1f).SetEase(Ease.OutBounce);

                        gameObject.transform.SetParent(parent);

                    }
            ) ;
    }


    // To be removed TODO
    public void OnPointerClick(PointerEventData eventData)
    {
        EnergyManager.instance.DiscardEnergyFromHand(this);
    }

    
}
