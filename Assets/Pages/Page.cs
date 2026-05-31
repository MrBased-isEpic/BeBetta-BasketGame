using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    public bool selfStart;
    
    private bool initialized;

    protected PageManager PageManager;
    protected Animator animator;
    
    [Tooltip("Leave as Empty, Sets Automatically")]
    public GameObject content;
    private GameObject fader;
    
    private AnimatorStateInfo clipInfo;

    private void Awake()
    {
        if (!selfStart) return;
        
        //Debug.Log("Self Start");
        Setup();
    }
   
    public void Setup()
    {
        PageManager = GetComponentInParent<PageManager>();
        content = transform.GetChild(0).gameObject;
        fader = transform.GetChild(1).gameObject;
        fader.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        animator.SetTrigger("Stop");
        content.SetActive(false);
        initialized = false;
    }

    #region INTERFACE

    public virtual void Show()
    {
        //Debug.Log($"page Check For {initialized} {transform.name}");
        if (!initialized)
        {
            Initialize();
            //Debug.Log($"Init page {transform.name}");
        }

        if (content.activeSelf)
        {
            //Debug.Log($"Content is Active {content.activeSelf} {transform.name}");
            return;
        }
        
        content.SetActive(true);

        if (animator.enabled)
        {
            //Debug.Log($"Animator is  {animator.enabled} and Called Entry {transform.name}");
            animator.SetTrigger("Entry");
        }
    }

    public virtual void Hide()
    {
        if (animator.enabled)
        {
            animator.SetTrigger("Exit");
        }
        StartCoroutine(WaitForHideAnimationAndTurnOff());
    }

    #endregion

    #region FOR_CHILDREN

    protected virtual void Initialize()
    {
        initialized = true;
        //Debug.Log($"BAse page init Called  {transform.name}");
    }

    protected void GoToPage<Page>()
    {
        if(PageManager != null)
            PageManager.GoToPage<Page>();
        else
            Debug.LogError("No manager controls this page");
    }

    protected void OpenPage<Page>()
    {
        if(PageManager != null)
            PageManager.OpenPage<Page>();
        else
            Debug.LogError("No manager controls this page");
    }

    #endregion


    private IEnumerator WaitForHideAnimationAndTurnOff()
    {
        yield return null;
        while (IsAnimationPlaying())
        {
            yield return null;
        }
        
        animator.SetTrigger("Stop");
            
        content.SetActive(false);
        
        fader.SetActive(false);
        fader.GetComponent<Image>().color = Color.clear;
    }

    public bool IsAnimationPlaying()
    {
        clipInfo = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log($"ISinAnimation {!(clipInfo.normalizedTime > 1)} {transform.name}");
        return !(clipInfo.normalizedTime >= 1);
    }
}
