using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InitializeEmergentObject3D : MonoBehaviour
{
    Animator animator;
    BoxCollider boxCollider;
    public List<MeshRenderer> MeshRenderers = new List<MeshRenderer>();
    MeshFilter MeshFilter;
    SpriteRenderer spriteRendererShadow;
    public float timeTimer = 8;
    private float remainingTime = 0; 

    private void Reset()
    {
        if (transform.Find("WaterShadow") == null)
        {
            GameObject child = new GameObject("WaterShadow");
            spriteRendererShadow = child.AddComponent<SpriteRenderer>();
            animator = child.AddComponent<Animator>();
            child.AddComponent<SpriteBillboard>();
            child.AddComponent<Emerge>();
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
        }
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        MeshRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
        foreach (MeshRenderer renderer in MeshRenderers)
        {
            renderer.enabled = false;
        }
        MeshFilter = GetComponent<MeshFilter>();
        animator.runtimeAnimatorController =  Resources.Load<RuntimeAnimatorController>("InitializeEmergentObjectAnimator");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("InitializeEmergentObjectAnimation");
        boxCollider = GetComponent<BoxCollider>();
        MeshRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
        foreach (MeshRenderer renderer in MeshRenderers)
        {
            renderer.enabled = false;
        }
        remainingTime = timeTimer;
    }

    private void Update()
    {
        if (remainingTime < 0)
        {
            Destroy(gameObject);
        }
        remainingTime -= Time.deltaTime;
    }

    public void ActivateObject()
    {
        boxCollider.enabled = true;
        foreach (MeshRenderer renderer in MeshRenderers)
        {
            renderer.enabled = true;
        }
    }
}
