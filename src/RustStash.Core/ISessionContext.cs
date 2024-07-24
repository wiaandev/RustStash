namespace RustStash.Core;

public interface ISessionContext
{
    public int PartyId { get; }

    public IDisposable DangerouslyAssumeSystemParty();
}
