namespace pocker_backend_core.frontEnd
{
    public sealed class User
    {
        public static readonly User Invalid = new User();

        private User()
        {
        }

        public static User Create()
        {
            return new User();
        }
    }
}