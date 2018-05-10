using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGun : MonoBehaviour {

    public virtual void PickupGun(Transform player) { }
    public virtual float ReturnForce() { return 0; }
    public virtual float ReturnAttackSpeed() { return 0; }
    public virtual void Shoot() { }
    public virtual void DropGun() { }
    public virtual void Reload() { }
    public virtual void LoadBullets(GameObject bullet, int bulletDmg) { }
    public virtual IEnumerator Special() { yield return null; }

    }

public static class GunFunctions {

    //Calculate the angle to shoot towards the mouse from the player
    public static Quaternion CalcPlayerToMouseAngle(Vector2 currentpos) {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(currentpos);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
        }

    //Calculate rotation for a scatter or spreadshot
    public static Quaternion calcSpreadShot(Vector2 currentPos, float minRange, float maxRange) {

        Vector3 sp = Camera.main.WorldToScreenPoint(currentPos);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float spread = UnityEngine.Random.Range(minRange, maxRange);

        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + spread));

        return bulletRotation;
        }

    //Give the gun recoil
    public static Vector2 KnockBack(Rigidbody2D rBody, Vector2 dir, float force) {
        rBody.AddForce(dir * force);
        return rBody.transform.position;
        }
    }

