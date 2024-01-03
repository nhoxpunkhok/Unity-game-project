using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public List<GameObject> itemPrefabs = new List<GameObject>(); // Danh sách các vật phẩm có thể rơi
    public List<float> dropRates = new List<float>(); // Danh sách tỉ lệ rơi tương ứng

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullets")
        {
            // Địch bị tiêu diệt bởi đạn của người chơi
            Destroy(gameObject);

            // Quyết định liệu có rơi vật phẩm hay không
            if (Random.value <= CalculateTotalDropRate())
            {
                // Rơi vật phẩm
                DropItem();
            }
    
        }
    }

    float CalculateTotalDropRate()
    {
        // Tính tổng tỉ lệ rơi
        float totalDropRate = 0f;
        foreach (float rate in dropRates)
        {
            totalDropRate += rate;
        }
        return totalDropRate;
    }

    void DropItem()
    {
        // Chọn ngẫu nhiên một vật phẩm từ danh sách
        float randomValue = Random.value * CalculateTotalDropRate();
        float cumulativeRate = 0f;

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            cumulativeRate += dropRates[i];
        if(itemPrefabs != null)
        {
            if (randomValue <= cumulativeRate)
            {
                
                // Tạo một vật phẩm mới
                GameObject newItem = Instantiate(itemPrefabs[i], transform.position, Quaternion.identity);

                // Kích hoạt Rigidbody để vật phẩm rơi xuống
                Rigidbody itemRigidbody = newItem.GetComponent<Rigidbody>();
                if (itemRigidbody != null)
                {
                    itemRigidbody.useGravity = true;
                    // Có thể thêm lực hoặc cấu hình vật lý cho vật phẩm ở đây
                }
            }
               
                }
            else 
            {
            Debug.LogError("itemPrefab is null. Make sure to assign a valid GameObject prefab.");
            return;
           
            }
        }
    }
}

    
