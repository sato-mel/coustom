using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class textColor : MonoBehaviour
{
    [Header("�_�Ŏ��Ԃ̊Ԋu")]
    [SerializeField]
    private float blinkIntervalTime = 1.5f;

    [Header("�C�[�W���O�̐ݒ�")]
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