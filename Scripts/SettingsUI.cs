using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private Button MouseControlButton;
    [SerializeField]
    private Button KeyboardMouseControlButton;
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());

    }
    private IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}   