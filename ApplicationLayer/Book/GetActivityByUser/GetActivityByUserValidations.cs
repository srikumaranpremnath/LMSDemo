using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Book.GetActivityByUser
{
    public class GetActivityByUserValidations : AbstractValidator<GetActivityByUserQuery>
    {
        public GetActivityByUserValidations()
        {
            RuleFor(task => task.RollNum).NotEmpty().WithMessage("User Details Id Is required");
        }
    }
}
