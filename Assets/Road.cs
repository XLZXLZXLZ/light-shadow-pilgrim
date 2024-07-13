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
        // ��ȡ���ʲ�����ԭʼ��ɫ
        material = GetComponent<Renderer>().material;
        originalColor = material.color;


        // ��ȡ����������ϵͳ
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
    /// ��·��ʾ�����붯��
    /// </summary>
    /// <param name="material"></param>
    void FadeIn(Material material)
    {
  
        material.DOFade(1, 1f);
    }

    /// <summary>
    /// ֹͣ���Ӳ������ݻٸù�·
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
