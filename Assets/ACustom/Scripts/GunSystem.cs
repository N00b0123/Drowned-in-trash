using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit hit;
    public LayerMask whatIsEnemy;
    public ParticleSystem muzzleFlash;
    public GameObject bulletHoleGraphic;
    public TextMeshProUGUI text;

    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;
        muzzleFlash.Play();

        //spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            //Debug.Log(rayHit.collider.name);
            //   if (rayHit.collider.CompareTag("Enemy"))
            //       rayHit.collider.GetComponent<EnemyController>().takeDamaged(); //verificar isso depois se funciona

            Instantiate(bulletHoleGraphic, hit.point, Quaternion.Euler(0, 180, 0));
        }
        //Instantiate(bulletHoleGraphic, hit.point, Quaternion.LookRotation(hit.normal));
        //Graphics
        // Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShooting);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }


}
