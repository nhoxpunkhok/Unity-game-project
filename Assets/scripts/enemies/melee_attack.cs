using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack02 : MonoBehaviour
{
    public bool attack = false;
    public float attackDelay = 2.0f; 
    public float raycastRange = 5.0f;   // do dai tia raycast
    public Transform target;             // doi tuong tan cong
    public int damage = 10;              // sat thuong tan cong

    private bool playerAttack = false;

    void Start()
    {
        MAttack();
    }

    void Update()
    {
        // every frame, ktra co tan cong khong
        MAttack();
    }

    void MAttack()
    {
        RaycastHit hit;

        if (attack)
        {
            // Tinh huong forward cua doi tuong
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            // dung raycast ktra vtri va cham 
            if (Physics.Raycast(target.position, fwd, out hit, raycastRange))
            {
                // Ve duong line do (debug)
                Debug.DrawLine(transform.position, hit.point, Color.red);

                if (hit.collider.CompareTag("Player"))
                {
                    // chay anim slash
                    GetComponent<Animator>().Play("Attack");
                    Debug.Log("Attacked");
                    // dat co playerAttack = true de xu ly sau va cham 
                    playerAttack = true;

                    // tham chieu den script Enemy va goi ham loseHealth voi gtri dmg
                    Stats playerScripts = hit.collider.GetComponent<Stats>();
                    if (playerScripts != null)
                    {
                        playerScripts.LoseHealth(damage);
                    }

                    // dat co attack = false de ngan chan viec tan cong lien tuc
                    attack = false;
                }
                else
                {
                    // Neu khong va cham, dat co playerAttack = false va dung anim tan cong
                    playerAttack = false;
                    GetComponent<Animator>().StopPlayback();
                }
            }
        }
        else
        {
            // Neu khong trong trang thai tan cong, delay thoi gian kick hoat lai co attack
            StartCoroutine(AttackCooldown());
        }
    }

    // Ham IEnumerator de su dung WaitForSeconds
    System.Collections.IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackDelay);
        attack = true;
    }
}
