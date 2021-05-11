using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TempScript : MonoBehaviour
{
    private void Awake()
    {
        // Sequence sequence = DOTween.Sequence();
        // sequence.Append(transform.DOMoveX(transform.position.x + 10, 2f));
        // sequence.Append(transform.DOMoveX(transform.position.x + -10, 2f));

        transform.DOMoveX(transform.position.x + 10, 2f).SetLoops(-1,LoopType.Yoyo);
    }
}