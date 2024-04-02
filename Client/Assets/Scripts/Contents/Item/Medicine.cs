using UnityEngine;

public class Medicine : BaseItem
{
    public override bool CheckAndUseItem(Crew crew)
    {
        if (crew.CrewStat.Hp == crew.CrewStat.MaxHp)
        {
            return false;
        }
        Use(crew);
        return true;
        
    }

    protected override void Rpc_Use()
    {
    }

    public void Use(Crew crew)
    {
        crew.CrewStat.Hp += 50;
        Debug.Log("Hp Recover");
        crew.Inventory.CurrentItemIdx = -1;
    }

}