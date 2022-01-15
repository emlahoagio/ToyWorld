using Contracts;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class FirebaseServices : IFirebaseSupport
    {
        private readonly IConfiguration configuration;

        public FirebaseServices(IConfiguration configuration)
        {
            this.configuration = configuration;
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
                string path = configuration["Firebase:CridentialPath"];
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path),
                    ServiceAccountId = configuration["FirebaseApp:ServiceAccountId"]
                });
            }
        }
    }
}
