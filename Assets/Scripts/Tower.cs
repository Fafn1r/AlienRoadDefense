using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private Projectile projectile;

    private Enemy targetEnemy = null;
    private float attackCounter;
    private bool isAttacking = false;

    private void Update()
    {
        attackCounter -= Time.deltaTime;
      //  if (targetEnemy == null)
     //   {
            Enemy nearestEnemy = GetNearestEnemyInRange();
            if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius)
                targetEnemy = nearestEnemy;
     //   }
     //   else
     //   {
        if (targetEnemy != null)
        {
            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = timeBetweenAttacks;
            }
            else
                isAttacking = false;
            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
                targetEnemy = null;
        }

      //  }
    }

    private void FixedUpdate()
    {
        if (isAttacking)
            Attack();
    }

    public void Attack()
    {
        isAttacking = false;
        Projectile newProjectile = Instantiate(projectile) as Projectile;
        newProjectile.transform.localPosition = transform.localPosition;
        if (targetEnemy == null)
            Destroy(newProjectile.gameObject);
        else
            StartCoroutine(MoveProjectile(newProjectile, targetEnemy));
    }

    IEnumerator MoveProjectile(Projectile projectile, Enemy projectileTarget)
    {
        while (GetTargetDistance(projectileTarget) > 0.20f && projectile != null && projectileTarget != null)
        {
            var dir = projectileTarget.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, projectileTarget.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }
        if (projectile != null && (projectileTarget == null || projectile.transform.localPosition == projectileTarget.transform.localPosition))
            Destroy(projectile.gameObject);
    }

    private float GetTargetDistance(Enemy thisEnemy)
    {
      //  if (thisEnemy == null)
      //  {
            thisEnemy = GetNearestEnemyInRange();
            if (thisEnemy == null)
                return 0f;
      //  }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }

    private Enemy GetNearestEnemyInRange()
    {
        if (GameManager.Instance.EnemyList.Count != 0)
        {
            Dictionary<float, Enemy> distDic = new Dictionary<float, Enemy>();

            foreach (Enemy enemy in GameManager.Instance.EnemyList)
            {
                float dist = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                distDic.Add(dist, enemy);
            }

            List<float> distances = distDic.Keys.ToList();
            distances.Sort();

            return distDic[distances[0]];
        }
        else
            return null;
    }
}
