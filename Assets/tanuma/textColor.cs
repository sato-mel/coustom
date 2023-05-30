using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class textColor : MonoBehaviour
{
    [Header("点滅時間の間隔")]
    [SerializeField]
    private float blinkIntervalTime = 1.5f;

    [Header("イージングの設定")]
    [SerializeField]
    private Ease easeType = Ease.Linear;

    void Start()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup
            .DOFade(0.0f, blinkIntervalTime)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo);
    }
}