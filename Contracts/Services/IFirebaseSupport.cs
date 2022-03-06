using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFirebaseSupport
    {
        void initFirebase();
        Task<FirebaseProfile> getEmailFromToken(string firebaseToken);
    }
}
