namespace Cqrs.Domain
{
    public enum ApplicationGuaranteeState
    {
        Initial = 0,
        BlockWaiting = 1,
        Blocked = 2,
        UnblockWaiting = 3,
        Unblocked = 4
    }
}