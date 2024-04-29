using Firebase.Auth.Providers;
using Firebase.Auth;

namespace CropCare.Services
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Service that handles user authorization
    static public class AuthService
    {
        // Configure...
        private static FirebaseAuthConfig config = new FirebaseAuthConfig
        {
            ApiKey = App.Settings.FireBaseApiKey,
            AuthDomain = App.Settings.FirebaseAuthorizedDomain,
            Providers = new FirebaseAuthProvider[]
            {
                // Add and configure individual providers
                new EmailProvider()
            },
        };
        // ...and create your FirebaseAuthClient

        /// <summary>
        /// Client for accessing firebase authorization services
        /// </summary>
        public static FirebaseAuthClient Client { get; } = new FirebaseAuthClient(config);

        /// <summary>
        /// Currently signed in users's credentials
        /// </summary>
        public static UserCredential UserCreds { get; set; }
    }
}
