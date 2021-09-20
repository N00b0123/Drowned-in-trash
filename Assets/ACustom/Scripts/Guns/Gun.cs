using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    // TO DO later: make variables private and use [SerializeField] to show in inspector

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 200f;
    public int maxAmmo = 90;
    public int clipSize = 30;
    public float reloadTime = 1f;
    public int bulletsPerTap = 1;
    public float timeBetweenShooting, spread;
    public bool isShotgun, isPistol, isRifle, isSMG;
    public static bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;
    public TextMeshProUGUI text;

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
        if (isShotgun && (currentAmmo > 0 && currentAmmo > 0))
            FindObjectOfType<AudioManager>().Play("Shotgun Shot");

        if (isRifle && currentAmmo > 0)
            FindObjectOfType<AudioManager>().Play("Rifle Shot");

        if (isSMG && currentAmmo > 0)
            FindObjectOfType<AudioManager>().Play("SMG Shot");

        if (isPistol && currentAmmo > 0)
            FindObjectOfType<AudioManager>().Play("Pistol Shot");

        if (currentAmmo <=0)
            FindObjectOfType<AudioManager>().Play("Empty Shot");
    }
    void PlaySoundReloading()
    {
        if (isShotgun)
            FindObjectOfType<AudioManager>().Play("Shotgun Reload");

        if (isRifle)
            FindObjectOfType<AudioManager>().Play("Rifle Reload");

        if (isSMG)
            FindObjectOfType<AudioManager>().Play("SMG Reload");

        if (isPistol)
            FindObjectOfType<AudioManager>().Play("Pistol Reload");
    }
}
