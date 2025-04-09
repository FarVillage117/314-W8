using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XrAudioManager : MonoBehaviour
{
    [SerializeField] XRGrabInteractable[] grabInteractables;
    [SerializeField] AudioSource grabSound;
    [SerializeField] AudioClip grabClip;
    [SerializeField] AudioClip keyClip;
    [SerializeField] AudioSource activatedSound;
    [SerializeField] AudioClip grabActivatedClip;
    [SerializeField] AudioClip wandActivatedClip;
    [SerializeField] TheWall wall;
    [SerializeField] AudioSource wallSource;
    [SerializeField] AudioClip destroyWallClip;

    private void OnEnable()
    {
        grabInteractables = FindObjectsByType<XRGrabInteractable>(FindObjectsSortMode.None);
        for (int i = 0; i < grabInteractables.Length; i++)
        {
            grabInteractables[i].selectEntered.AddListener(OnSelectEnterGrabbable);
            grabInteractables[i].selectExited.AddListener(OnSelectExitGrabbable);
            grabInteractables[i].activated.AddListener(OnActivatedGrabbable);
        }
        if (fallBackClip == null)
        {
            fallBackClip = AudioClip.Create(FallBackClip_Name, 1, 1, 1000, true);
        }
    }

    private void OnActivatedGrabbable(ActivateEventArgs arg0)
    {
        GameObject tempGameObject = arg0.interactableObject.transform.gameObject;
        if (tempGameObject.GetComponent<WandControl>() != null)
        {
            activatedSound.clip = wandActivatedClip;
        }
        else
        {
            activatedSound.clip = grabActivatedClip;
        }
        activatedSound.Play();
    }

    private void OnSelectExitGrabbable(SelectExitEventArgs arg0)
    {
        grabSound.clip = grabClip;
        grabSound.Play();
    }

    private void OnSelectEnterGrabbable(SelectEnterEventArgs arg0)
    {
        if (arg0.interactableObject.transform.CompareTag("Key"))
        {
            grabSound.clip = keyClip;
        }
        else
        {
            grabSound.clip = grabClip;
        }
        grabSound.Play();
    }

    private void OnDestroyWall()
    {
        if (wallSource != null)
