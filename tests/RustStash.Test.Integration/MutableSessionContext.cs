namespace RustStash.Test.Integration;

using RustStash.Core;

public class MutableSessionContext : ISessionContext
{
    public int PartyId { get; set; }

    public IDisposable DangerouslyAssumeSystemParty()
    {
        var previousPartyId = this.PartyId;
        this.PartyId = Constants.SystemEntityId;
        return new CleanUp(previousPartyId, this);
    }

    private sealed class CleanUp : IDisposable
    {
        private readonly int previousPartyId;
        private readonly MutableSessionContext sessionContext;

        public CleanUp(int previousPartyId, MutableSessionContext sessionContext)
        {
            this.previousPartyId = previousPartyId;
            this.sessionContext = sessionContext;
        }

        public void Dispose()
        {
            this.sessionContext.PartyId = this.previousPartyId;
        }
    }
}
