using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    private GunData gunData;

    [SerializeField] private GameObject projectile;

    [SerializeField] private GunData pistolData;
    [SerializeField] private GunData shotgunData;
    [SerializeField] private GunData tommygunData;

    [SerializeField] private GameObject muzzleFlash;

    private SpriteRenderer spriteRenderer;
    private int currGunIndex;
    private int gunInventorySize = 3;

    float timeSinceLastShot;
    // Start is called before the first frame update
    void Start()
    {
        gunData = pistolData;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gunData.gunSprite;

        timeSinceLastShot = gunData.fireRate;
        currGunIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= (1.0f / gunData.fireRate))
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
                timeSinceLastShot = 0.0f;
            }
        }

        // Weapon Switching: Numbers
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currGunIndex = 0;
            currGunIndex = currGunIndex % gunInventorySize;
            SwitchGun(currGunIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currGunIndex = 1;
            currGunIndex = currGunIndex % gunInventorySize;
            SwitchGun(currGunIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currGunIndex = 2;
            currGunIndex = currGunIndex % gunInventorySize;
            SwitchGun(currGunIndex);
        }

        // Weapon Switching Scroll
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            currGunIndex++;
            currGunIndex = currGunIndex % gunInventorySize;
            SwitchGun(currGunIndex);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            currGunIndex--;

            // Infinite scroll backwards
            if (currGunIndex < 0)
            {
                currGunIndex = gunInventorySize - 1;
            }

            currGunIndex = currGunIndex % gunInventorySize;
            SwitchGun(currGunIndex);
        }
    }

    void Shoot()
    {
        // Introduce gun specifics
        for(int i = 0; i < gunData.projectilesPerShot; i++)
        {
            Vector2 spawnPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 targetPosition = Input.mousePosition;
            Vector3 direction = (targetPosition - spawnPosition).normalized;

            direction = Quaternion.Euler(0.0f, 0.0f, Random.Range(-gunData.spread/2.0f, gunData.spread/2.0f)) * direction;

            GameObject currentProjectile = Instantiate(projectile, transform.position, transform.rotation);
            currentProjectile.GetComponent<Projectile>().SetDirection(direction);
        }

        muzzleFlash.SetActive(true);
        StartCoroutine(Muzzle());
    }

    IEnumerator Muzzle()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
    }

    void SwitchGun(int gunIndex)
    {
        switch (gunIndex)
        {
            default:
                gunData = pistolData;
                break;
            case 0:
                gunData = pistolData;
                break;
            case 1:
                gunData = shotgunData;
                break;
            case 2:
                gunData = tommygunData;
                break;
        }
        spriteRenderer.sprite = gunData.gunSprite;
    }
}
