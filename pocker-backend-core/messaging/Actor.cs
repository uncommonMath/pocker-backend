namespace pocker_backend_core.messaging
{
    public abstract class Actor
    {
        protected Actor()
        {
            Directory.RegisterActor(this);
        }
    }
}