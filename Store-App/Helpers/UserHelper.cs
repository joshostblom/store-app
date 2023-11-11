using Store_App.Models.DBClasses;

namespace Store_App.Helpers
{
    public static class UserHelper
    {
        private static Person? _currentUser = null;

        public static void SetCurrentUser(Person? user)
        {
            _currentUser = user;
        }

        public static Person? GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
