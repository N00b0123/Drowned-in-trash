using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    // TO DO later: make variables private and use [SerializeField] to show in inspector

    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] float impactForce = 200f;
    [SerializeField] int maxAmmo = 90;
    [SerializeField] int clipSize = 30;
    [SerializeField] float reloadTime = 1f;
    [SerializeField] int bulletsPerTap = 1;
    [SerializeField] float timeBetweenShooting, spread;
    [SerializeField] bool isShotgun, isPistol, isRifle, isSMG;
    public static bool isReloading = false;

    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem bloodShot;
    [SerializeField] GameObject impactEffect;
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI text;

    private bool readyToShoot;
    private float nextTimeToFire = 0f;
    private int currentAmmo, leftAmmo, ammoToReload;
    private int bulletsShot;

    public void Awake()
    {
        currentAmmo = clipSize;
        readyToShoot = true;
    }

     void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if (maxAmmo > 0 && Input.GetKeyDown(KeyCode.R) && (currentAmmo != clipSize) || (maxAmmo > 0 && currentAmmo <= 0))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && readyToShoot && currentAmmo > 0 && !GameManager.isPaused && !GameManager.isGameOver)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            bulletsShot = bulletsPerTap;
            Shoot();
        }
        //empty shot sound
        if (!isReloading && Input.GetButtonDown("Fire1") && currentAmmo <= 0)
            PlaySoundShoot();

        text.SetText(currentAmmo/ bulletsPerTap + " / " + maxAmmo/ bulletsPerTap);

    }

    void Shoot()
    {
        readyToShoot = false;
        muzzleFlash.Play();
        PlaySoundShoot();
        
        //spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(fpsCam.transform.position, direction, out RaycastHit hit, range))
        {
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            IDamage target = hit.transform.GetComponent<IDamage>();
            if(target != null)
            {
                target.TakeDamage(damage);
                if (enemy != null)
                    Instantiate(bloodShot, hit.point, Quaternion.LookRotation(hit.normal));

            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if(hit.rigidbody == null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if (hit.collider.CompareTag("Player"))
            {
                return;
            }
        }

        currentAmmo--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && currentAmmo > 0)
            Invoke("Shoot", timeBetweenShooting);
    }
    void ResetShot()
    {
        readyToShoot = true;
    }
    IEnumerator Reload()
    {
        isReloading = true;
        PlaySoundReloading();

        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        if (currentAmmo > 0)
        {
            leftAmmo = currentAmmo;
            if(clipSize <= maxAmmo)
            {
                ammoToReload = clipSize - leftAmmo;
                currentAmmo = currentAmmo + ammoToReload;
                maxAmmo = maxAmmo - ammoToReload;
            }
            else
            {
                ammoToReload = clipSize - leftAmmo;
                if (ammoToReload > maxAmmo)
                {
                    ammoToReload = maxAmmo;
                    currentAmmo = currentAmmo + ammoToReload;
                    maxAmmo = 0;
                }
                else
                {
                    currentAmmo = currentAmmo + ammoToReload;
                    maxAmmo = maxAmmo - ammoToReload;
                }
            }
        }
        else
        {
            if (clipSize < maxAmmo)
                currentAmmo = clipSize;
            else
                currentAmmo = maxAmmo;
            maxAmmo = maxAmmo - currentAmmo;
        }
        isReloading = false;
    }

    void PlaySoundShoot()
    {
        AudioManager audio = FindObjectOfType<AudioManager>();
        if (isShotgun && (currentAmmo > 0 && currentAmmo > 0))
            audio.Play("Shotgun Shot");

        if (isRifle && currentAmmo > 0)
            audio.Play("Rifle Shot");

        if (isSMG && currentAmmo > 0)
            audio.Play("SMG Shot");

        if (isPistol && currentAmmo > 0)
            audio.Play("Pistol Shot");

        if (currentAmmo <=0)
            audio.Play("Empty Shot");
    }
    void PlaySoundReloading()
    {
        AudioManager audio = FindObjectOfType<AudioManager>();
        if (isShotgun)
            audio.Play("Shotgun Reload");

        if (isRifle)
            audio.Play("Rifle Reload");

        if (isSMG)
            audio.Play("SMG Reload");

        if (isPistol)
            audio.Play("Pistol Reload");
    }
}
