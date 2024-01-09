using FluentValidation;

namespace Application.User.UpdateUser
{
    public class UpdateUserValidations : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidations()
        {
            RuleFor(task => task.UserDetailsId).NotEmpty().WithMessage("Name is required");
            RuleFor(task => task.Name).NotEmpty().WithMessage("Name is required");
                RuleFor(task => task.RollNum).NotEmpty().WithMessage("RollNum is required");
                RuleFor(task => task.Department).NotEmpty().WithMessage("Department is required");
                RuleFor(task => task.Email).NotEmpty().WithMessage("Email is required");
                RuleFor(task => task.Mobile).NotEmpty().WithMessage("RollNum is required");
                RuleFor(task => task.RollNum).NotEmpty().WithMessage("RollNum is required");
                RuleFor(task => task.Address).SetValidator(new AddressValidations());



        }
        public class AddressValidations : AbstractValidator<UpdateAddressCommand>
        {
            public AddressValidations()
            {
                RuleFor(task => task.AddressId).NotEmpty().WithMessage("Name is required");
                RuleFor(task => task.HouseNo).NotEmpty().WithMessage("HouseNo is required");
                RuleFor(task => task.Street).NotEmpty().WithMessage("Street is required");
                RuleFor(task => task.Area).NotEmpty().WithMessage("Area is required");
                RuleFor(task => task.City).NotEmpty().WithMessage("City is required");
                RuleFor(task => task.State).NotEmpty().WithMessage("State is required");
                RuleFor(task => task.Country).NotEmpty().WithMessage("Country is required");
                RuleFor(task => task.Pincode).NotEmpty().WithMessage("Pincode is required");

            }
        }
    }
}
