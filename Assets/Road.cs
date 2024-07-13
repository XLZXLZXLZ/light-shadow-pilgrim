using UnityEngine;
using DG.Tweening;

public class Road : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    private Material material;
    private Color originalColor;
    private ParticleSystem[] particleSystems;
    private Material[] particleMaterials;

    void Start()
    {
        // 获取材质并保存原始颜色
        material = GetComponent<Renderer>().material;
        originalColor = material.color;


        // 获取所有子粒子系统
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        particleMaterials = new Material[particleSystems.Length];


        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].startColor = originalColor;
        }


        Color initialColor = originalColor;
        initialColor.a = 0;
        material.color = initialColor;


        FadeIn(material);
    }

    /// <summary>
    /// 光路显示，淡入动画
    /// </summary>
    /// <param name="material"></param>
    void FadeIn(Material material)
    {
  
        material.DOFade(1, 1f);
    }

    /// <summary>
    /// 停止粒子产生并摧毁该光路
    /// </summary>
    public void FadeOutAndDestroy()
    {

        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].loop = false;
        }
        material.DOFade(0, 1f).OnComplete(() => Destroy(gameObject)); 
    }
}
