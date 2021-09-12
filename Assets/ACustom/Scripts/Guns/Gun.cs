using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 200f;
    public int maxAmmo = 90;
    public int clipSize = 30;
    public float reloadTime = 1f;
    public int bulletsPerTap = 1;
    public float timeBetweenShooting, spread;
    public bool isShotgun, isPistol, isRifle, isSMG, isFullAuto;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;
    public TextMeshProUGUI text;

    private bool isReloading = false;
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

        if (maxAmmo > 0 && Input.GetKeyDown(KeyCode.R) || (maxAmmo > 0 && currentAmmo <= 0))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && readyToShoot && currentAmmo > 0)
        {
            isFullAuto = true;
            nextTimeToFire = Time.time + 1f / fireRate;
            bulletsShot = bulletsPerTap;
            Shoot();

        }

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

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            IDamage target = hit.transform.GetComponent<IDamage>();
            if(target != null)
            {
                target.TakeDamage(damage);
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
        if (isShotgun)
            FindObjectOfType<AudioManager>().Play("Shotgun Shoot");
    }
    void PlaySoundReloading()
    {
        if (isShotgun)
            FindObjectOfType<AudioManager>().Play("Shotgun Reload");

        if (isRifle)
            FindObjectOfType<AudioManager>().Play("Rifle Reload");
    }
}
