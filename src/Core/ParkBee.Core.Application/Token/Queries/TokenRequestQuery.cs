using MediatR;
using ParkBee.Core.Application.Common.Interfaces;
using ParkBee.Core.Application.Common.Models;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace ParkBee.Core.Application.Token
{
    public class TokenRequestQuery : IRequest<TokenResult>
    {
        public TokenRequestQuery() { }
        public string UserName { get;  set; }

        public string Password { get;  set; }

        public TokenRequestQuery(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
    public sealed class TokenRequestHandler : IRequestHandler<TokenRequestQuery, TokenResult>
    {

        private readonly ITokenService _tokenService;

        public TokenRequestHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<TokenResult> Handle(TokenRequestQuery request, CancellationToken cancellationToken)
        {
            return  _tokenService.GetToken(request);
        }
    }
}

