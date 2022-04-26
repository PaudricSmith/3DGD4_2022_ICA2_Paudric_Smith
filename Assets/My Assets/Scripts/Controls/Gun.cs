using System.Collections;
using UnityEngine;


public class Gun : MonoBehaviour
{
    private bool isEquiped = false;

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject gunModel;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;

    [SerializeField] private AudioSource gunAudioSource;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip gunShot;
    [SerializeField] private AudioClip equipSFX;
    [SerializeField] private AudioClip unEquipSFX;
    [SerializeField] private AudioClip emptyGunSFX;

    [SerializeField] private float range = 60.0f;
    [SerializeField] private float damage = 10.0f;


    private void Awake()
    {
        gun.SetActive(false);
    }


    private void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }


    private void PlayGunshotSound()
    {
        StartCoroutine("PlayGunshot", 0.1f);
    }


    private IEnumerator PlayGunshot(float waitTime)
    {
        gunAudioSource.clip = gunShot;
        gunAudioSource.Play();

        yield return new WaitForSeconds(waitTime);
    }


    public void Equip()
    {
        if (gun != null)
        {
            if (gun.activeSelf == false)
            {
                // Equip
                gun.SetActive(true);
                isEquiped = true;

                // Play Equip SFX
                gunAudioSource.PlayOneShot(equipSFX);
            }
            else if (gun.activeSelf)
            {
                // Unequip
                gun.SetActive(false);
                isEquiped = false;

                // Play Unequip SFX
                playerAudioSource.PlayOneShot(unEquipSFX);
            }
        }
    }


    public void Shoot()
    {
        // If the Player's gun is equipped 
        if (isEquiped)
        {
            // Play gun shot SFX
            PlayGunshotSound();

            //Calls animation on the gun that has the relevant animation events that will fire
            gunAnimator.SetTrigger("Fire");

            RaycastHit hitInfo;
            // If the player hit something in range
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hitInfo, range))
            {                             
            }
            // If the Player didn't hit anything in range still use up one bullet 
            else
            {               
            }
        }
    }
}