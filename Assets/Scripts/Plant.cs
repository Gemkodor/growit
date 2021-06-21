using UnityEngine;

public class Plant : MonoBehaviour
{
    GameManager gm;
    GameManager.CROPS cropType;
    GameObject plantedCrop;
    Player player;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gm.isPlanting && plantedCrop == null && player.crops[gm.GetSelectedCropType().ToString()] > 0)
            {
                // Player clicks to plant a crop
                // Instantiate crop and place it on the screen
                Vector3 cropPos = GetComponent<Renderer>().bounds.center;
                GameObject crop = Instantiate(gm.GetSelectedCrop());
                crop.transform.position = cropPos;

                plantedCrop = crop;
                cropType = gm.GetSelectedCropType();
                player.RemoveCrop(cropType, 1);

                gm.SetPlanting(player.crops[gm.GetSelectedCropType().ToString()] > 0);
            }
            else if (!gm.isPlanting && plantedCrop != null)
            {
                // Player clicks to harvest crop
                Crop crop = plantedCrop.GetComponent<Crop>();
                if (crop.fullGrowth)
                {
                    player.AddCrop(cropType, crop.GetHarvestingQty());
                    Destroy(plantedCrop.gameObject);
                }
            }
        }
    }
}
