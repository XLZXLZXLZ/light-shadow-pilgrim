using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class LevelItem : MonoBehaviour
{
    [field: SerializeField] public int LevelIndex { get; private set; }

    private List<MeshRenderer> renderers;
    private Vector3 startPos;

    private void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>().ToList();
        startPos = transform.localPosition;
    }

    public void SetMat(Material material)
    {
        renderers.ForEach(meshRenderer => meshRenderer.material = material);
    }

    public void MoveUp()
    {
        transform.DOLocalMove(startPos + Vector3.up * 0.5f, 0.25f);
    }

    public void MoveDown()
    {
        transform.DOLocalMove(startPos, 0.25f);
    }
}

