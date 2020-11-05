namespace pocker_backend_core.messages
{
    public abstract class Actor
    {
        protected Actor()
        {
            Directory.RegisterActor(this);
        }
    }
}