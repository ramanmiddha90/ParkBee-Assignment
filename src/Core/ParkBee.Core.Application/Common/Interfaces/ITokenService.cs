using ParkBee.Core.Application.Common.Models;
using ParkBee.Core.Application.Token;
using System.Threading.Tasks;

namespace ParkBee.Core.Application.Common.Interfaces
{
    public interface ITokenService
    {
       TokenResult GetToken(TokenRequestQuery query);
    }
}
