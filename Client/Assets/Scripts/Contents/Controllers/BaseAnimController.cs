using Fusion;

public abstract class BaseAnimController : NetworkBehaviour
{
    public NetworkMecanimAnimator NetworkAnim { get; protected set; }

    public Creature Creature { get; protected set; }
    public Define.CreatureState CreatureState => Creature.CreatureState;
    public Define.CreaturePose CreaturePose => Creature.CreaturePose;

    [Networked] public float XParameter { get; set; }
    [Networked] public float ZParameter { get; set; }
    [Networked] public float SpeedParameter { get; set; }

    public override void Spawned()
    {
        Init();
    }

    protected virtual void Init()
    {
        NetworkAnim = gameObject.GetComponent<NetworkMecanimAnimator>();
        Creature = gameObject.GetComponent<Creature>();
    }

    #region Update

    public abstract void PlayIdle();

    public abstract void PlayMove();

    #endregion

    #region SetParameter

    protected void SetTrigger(string parameter)
    {
        NetworkAnim.SetTrigger(parameter);
    }

    protected void SetBool(string parameter, bool value)
    {
        NetworkAnim.Animator.SetBool(parameter, value);
    }

    protected void SetFloat(string parameter, float value)
    {
        NetworkAnim.Animator.SetFloat(parameter, value);
    }

    protected abstract void SetParameterFalse();

    #endregion
}