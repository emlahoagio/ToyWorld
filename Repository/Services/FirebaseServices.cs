using Contracts;
using Entities.DataTransferObject;
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
        public async Task<FirebaseProfile> getEmailFromToken(string firebaseToken)
        {
            try
            {
                FirebaseToken decodeToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
                return new FirebaseProfile
                {
                    Email = decodeToken.Claims.GetValueOrDefault("email").ToString(),
                    Avatar = decodeToken.Claims.GetValueOrDefault("picture").ToString(),
                    Name = decodeToken.Claims.GetValueOrDefault("name").ToString()
                };
            }
            catch (Exception ex)
            {
                return new FirebaseProfile { Email = ex.Message};
            }
        }

        public void initFirebase()
        {
            if (FirebaseApp.DefaultInstance == null)
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
