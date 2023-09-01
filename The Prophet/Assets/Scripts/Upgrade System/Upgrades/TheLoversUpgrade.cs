using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class TheLoversUpgrade : UpgradeAbility
{
    [SerializeField] private float _radius = 4f;
    [SerializeField] private float _smoothTimeIdle = 0.25f;
    [SerializeField] private float _smoothTimeAttack = 0.05f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private int _projectileMaxCount = 4;

    private List<GameObject> projectiles;

    public override void Activate()
    {
        base.Activate();

        projectiles = new List<GameObject>();

        UpgradeSystemManager.instance.StartCoroutine(ProjectileSpawn());
    }  

    private IEnumerator ProjectileSpawn()
    {
        float elapsedTime = 0f;

        while (true)
        {
            elapsedTime += Time.deltaTime;

            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i] == null)
                {
                    projectiles.RemoveAt(i);
                    elapsedTime = 0f;
                }
            }

            if (projectiles.Count < _projectileMaxCount && elapsedTime > 4f)
            {
                elapsedTime = 0;

                GameObject projectileClone = Instantiate(_projectile);
                Debug.Log("Spawn Projectile");
                TheLoversProjectileController theLoversProjectileController = projectileClone.GetComponent<TheLoversProjectileController>();

                projectileClone.transform.position = CharacterController2D.instance.transform.position;

                theLoversProjectileController.radius = _radius;
                theLoversProjectileController.smoothTimeAttack = _smoothTimeAttack;
                theLoversProjectileController.smoothTimeIdle = _smoothTimeIdle;
                theLoversProjectileController.damage = _damage;
                theLoversProjectileController.enemyLayer = _enemyLayer;

                projectiles.Add(projectileClone);
            }

            yield return null;
        }
    }
}
