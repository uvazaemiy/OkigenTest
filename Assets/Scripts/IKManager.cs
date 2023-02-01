using System.Collections;
using DitzelGames.FastIK;
using Unity.VisualScripting;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    [SerializeField] private FastIKFabric[] MainIK;
    [SerializeField] private FastIKFabric[] OtherIK;


    
    

    public void ActiveIK()
    {
        foreach (FastIKFabric IK in MainIK)
            IK.enabled = true;
        foreach (FastIKFabric IK in OtherIK)
            IK.enabled = true;
    }

    public void DisableIK()
    {
        foreach (FastIKFabric IK in OtherIK)
            IK.enabled = false;
        foreach (FastIKFabric IK in MainIK)
            IK.enabled = false;
    }
}
