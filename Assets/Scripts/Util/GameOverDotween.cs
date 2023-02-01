using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverDotween : MonoBehaviour
{
    public Ease ease;

    private void OnEnable()
    {
        this.transform.localPosition = new Vector3(0f, 700f, 0f);
        transform.DOLocalMoveY(313, 1f).SetEase(ease).SetDelay(0.2f);
    }
}
