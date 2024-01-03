using UnityEngine;

public class damage : MonoBehaviour
{
    public int damageAmount = 5; // Số lượng sát thương mỗi giây
    public float damageInterval = 1f; // Khoảng thời gian giữa các lần gây sát thương

    private float timer = 0f;

    void OnCollisionStay(Collision collision)
    {
        // Kiểm tra xem đối tượng va chạm có một thành phần cụ thể (ví dụ: "Player") không
        if (collision.gameObject.CompareTag("Player"))
        {
            // Tăng đồng hồ đếm thời gian
            timer += Time.deltaTime;

            // Kiểm tra xem đã đến lúc gây sát thương chưa
            if (timer >= damageInterval)
            {
                // Gọi hàm gây sát thương và đặt lại đồng hồ đếm
                InflictDamage(collision.gameObject);
                timer = 0f;
            }
        }
    }

    void InflictDamage(GameObject target)
    {
        // Thực hiện xử lý sát thương tại đây, ví dụ: giảm máu của đối tượng
        PlayerStats healthController = target.GetComponent<PlayerStats>();
        if (healthController != null)
        {
            healthController.TakeDamage(damageAmount);
        }
    }
}