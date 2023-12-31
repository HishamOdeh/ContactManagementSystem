﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public class ContactFormModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }

    }
    public class ContactValidator : AbstractValidator<ContactFormModel>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(25).WithMessage("First Name must be less than 25 characters")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters")
                .NotEmpty().WithMessage("First name must not be empty");

            RuleFor(x => x.LastName)
                .MaximumLength(25).WithMessage("Last Name must be less than 25 characters")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters")
                .NotEmpty().WithMessage("Last name must not be empty");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(25).WithMessage("Phone Number must be less than 25 characters")
                .MinimumLength(10).WithMessage("Phone Number must be at least 10 characters")
                .NotEmpty().WithMessage("Phone Number must not be empty");

            RuleFor(x => x.Email)
                .MinimumLength(10).WithMessage("Email must be at least 10 characters")
                .Must(ContainsAtSymbol).WithMessage("Email must contain @ symbol")
                .NotEmpty().WithMessage("Email must not be empty");
        }

        private bool ContainsAtSymbol(string email)
        {
            return email.Contains("@");
        }

    }
}
