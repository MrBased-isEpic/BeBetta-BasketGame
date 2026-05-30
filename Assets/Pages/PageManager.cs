using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public bool selfStart = true;
    public bool showFirstPage = true;
    public bool createSingleton = true;
    
    private Dictionary<int, Page> pages;
    private Page current;

    private bool initialized;

    public static PageManager Instance;

    public void Start()
    {
        if (selfStart)
            Initialize();
    }
    public void Initialize()
    {
        if (initialized) return;
        
        if(createSingleton && Instance == null && Instance != this)
            Instance = this;
        
        pages = new Dictionary<int, Page>(transform.childCount);

        Page temp;
        for (int i = 0; i < transform.childCount; i++)
        {
            temp = transform.GetChild(i).GetComponent<Page>();
            temp?.Setup();

            //DebugLogger.LogError(temp.GetType().Name);
            pages.Add(temp.GetType().Name.GetHashCode(), temp);
        }

        if(showFirstPage)
            ShowPage(pages.ElementAt(0).Value);

        initialized = true;
    }

    public void OnDestroy()
    {
        if(Instance != null)
            Instance = null;
    }
    
    public void GoToPage<PageType>(float delay = 0)
    {
        Page targetPage = GetPage<PageType>();

        if (targetPage.content.activeSelf) return;
        
        List<Page> closingScreens = new List<Page>();

        foreach (Page page in pages.Values)
        {
            if (page.content.activeSelf)
            {
                page.Hide();
                closingScreens.Add(page);
            }
        }
        
        StopAllCoroutines();
        StartCoroutine(WaitForHideAnimationAndShow(closingScreens, targetPage, delay));
    }

    public void OpenPage<PageType>(float delay = 0)
    {
        Page targetPage = GetPage<PageType>();

        if (targetPage.content.activeSelf) return;
        
        targetPage.Show();
    }
    
    public Page GetPage<PageType>()
    {
        int hash = typeof(PageType).Name.GetHashCode();
        if (!pages.ContainsKey(hash))
        {
            Debug.LogError($"Can't find requested screen {typeof(PageType).Name}");
            return (pages.ElementAt(0).Value);
        }

        return pages[hash];
    }
    
    #region PRIVATE_METHODS
    
    private IEnumerator WaitForHideAnimationAndShow(List<Page> hiding, Page target, float delay)
    {
        yield return new WaitForSeconds(delay);

        bool isAnimationFinished = false;
        if (hiding != null && hiding.Count > 0)
        {
            do
            {
                yield return null;
                isAnimationFinished = true;
                foreach (Page h in hiding)
                {
                    if (h.IsAnimationPlaying())
                    {
                        isAnimationFinished = false;
                        break;
                    }
                }
            }while (!isAnimationFinished);
            
        }

        ShowPage(target);
    }

    private void ShowPage(Page page)
    {
        page.Show();
    }

    #endregion
}
