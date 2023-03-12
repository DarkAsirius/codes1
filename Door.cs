using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public Animator m_Animator;
    public bool IsOpen;

    void Start()
    {
        if(IsOpen)
        {
            m_Animator.SetBool("IsOpen", true);
        }
    }

    public string GetDescription()
    {
        if(IsOpen) return "Press [E] to <color=red>close</color> the door";
        else return "Press [E] to <color=green>open</color> the door";
    }
    
    public void Interact()
    {
        IsOpen = !IsOpen;
        if(IsOpen)
        {
            m_Animator.SetBool("IsOpen", true);
            this.GetComponent<BoxCollider>().isTrigger = false; 
        }
        else
        {
            m_Animator.SetBool("IsOpen", false);
            this.GetComponent<BoxCollider>().isTrigger = true; 
        }
    }
}
