using FluentValidation;
using ParkBee.Core.Application.Garages.Commands;
using System;

namespace ParkBee.Core.Application.Common.Validators
{
   public class UpdateStatusRequestValidator : AbstractValidator<RefreshGarageDoorStatusCommand>
    {
       public UpdateStatusRequestValidator()
        {
            RuleFor(x => x.DoorId).NotEqual(Guid.Empty).WithMessage("Invalid door Id").WithErrorCode("Err-01");

            RuleFor(x => x.GargeId).NotEqual(Guid.Empty).WithMessage("Invalid garage Id").WithErrorCode("Err-02");

            RuleFor(x => x.IPAddress).NotEmpty().WithMessage("Invalid door IP Address").WithErrorCode("Err-03");
        }
    }
}
