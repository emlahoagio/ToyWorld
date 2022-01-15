using Contracts;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FirebaseServices : IFirebaseSupport
    {
        private readonly IConfiguration _configuration;

        public FirebaseServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> getEmailFromToken(string firebaseToken)
        {
            try
            {
                FirebaseToken decodeToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
                return decodeToken.Claims.GetValueOrDefault("email").ToString();
            }
            catch (Exception ex)
            {
                return "Get email from token error: " + ex.Message;
            }
        }

        public void initFirebase()
        {
            if(FirebaseApp.DefaultInstance == null)
            {
                string path = _configuration["Firebase:CridentialPath"];
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path),
                    ServiceAccountId = _configuration["FirebaseApp:ServiceAccountId"]
                });
            }
        }
    }
}
