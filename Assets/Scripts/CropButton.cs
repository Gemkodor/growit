using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropButton : MonoBehaviour
{
    [SerializeField] int cropId;

    public int GetCropId()
    {
        return cropId;
    }
}
