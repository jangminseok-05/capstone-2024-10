﻿using Fusion;
using UnityEngine;

public class ItemKit : BaseWorkStation
{
    [Header("Item")]
    [SerializeField] private int _itemId;

    public override string InteractDescription => "OPEN ITEM KIT";

    public GameObject Cover { get; protected set; }

    protected override void Init()
    {
        base.Init();

        Cover = Util.FindChild(gameObject, "Cover", true);

        CanRememberWork = false;
        IsCompleted = false;

        TotalWorkAmount = 21f;
    }
    public override bool CheckInteractable(Creature creature)
    {
        creature.IngameUI.ErrorTextUI.Hide();

        if (creature is not Crew crew)
        {
            creature.IngameUI.InteractInfoUI.Hide();
            return false;
        }

        if (creature.CreatureState == Define.CreatureState.Interact)
        {
            creature.IngameUI.InteractInfoUI.Hide();
            return false;
        }

        if (IsCompleted)
        {
            creature.IngameUI.InteractInfoUI.Hide();
            return false;
        }

        creature.IngameUI.InteractInfoUI.Show(InteractDescription);
        return true;
    }

    protected override void WorkComplete()
    {
        // NetworkObject no = Managers.ObjectMng.SpawnItemObject(_itemId, transform.position);
        // no.transform.SetParent(gameObject.transform);

        Rpc_WorkComplete();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    protected override void Rpc_WorkComplete()
    {
        if (IsCompleted)
            return;
        IsCompleted = true;

        Cover.SetActive(false);
    }

    protected override void PlayInteractAnimation()
    {
        CrewWorker.CrewAnimController.PlayKeypadUse();
    }

    protected override void PlayEffectMusic()
    {
        Managers.SoundMng.Play("Music/Clicks/Typing_Keyboard", Define.SoundType.Effect, 0.5f, true);
    }
}