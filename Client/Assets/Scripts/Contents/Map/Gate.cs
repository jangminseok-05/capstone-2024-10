using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Gate : NetworkBehaviour
{
    public void Open()
    {
        GetComponent<NetworkMecanimAnimator>().Animator.SetBool("IsOpen", true);
        Rpc_PlaySound();
    }

    public void Close()
    {
        GetComponent<NetworkMecanimAnimator>().Animator.SetBool("IsOpen", false);
        Rpc_PlaySound();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void Rpc_PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}