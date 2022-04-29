using Entities.DataTransferObject;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFirebaseSupport
    {
        void initFirebase();
        Task<FirebaseProfile> GetEmailFromToken(string firebaseToken);
    }
}
