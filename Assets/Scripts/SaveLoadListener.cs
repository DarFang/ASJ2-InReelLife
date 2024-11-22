using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadListener : MonoBehaviour
{
    public void CallRemoveSaveState()
    {
        SaveLoadManager.Instance.DeleteAllApplicationFiles();
    }
}
