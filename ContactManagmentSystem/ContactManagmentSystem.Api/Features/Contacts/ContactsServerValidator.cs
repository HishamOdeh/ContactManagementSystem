using ContactManagementSystem.Domain;
using ContactManagementSystem.Shared.Features.Contacts;
using FluentValidation;
using static ContactManagementSystem.Shared.Features.Contacts.ContactFormModel;

namespace ContactManagementSystem.Api.Features.Contacts
{
    public class ContactsServerValidator : AbstractValidator<ContactFormModel>
    {
        private readonly AppDbContext _context;

        public ContactsServerValidator(AppDbContext context)
        {
            _context = context;

            Include(new ContactValidator());

            RuleFor(x => x).Must(x => NoDuplicatePhoneNumbers(x)).WithMessage("NO duplicate phone numbers");
            RuleFor(x => x).Must(x => NoDuplicateEmails(x)).WithMessage("NO duplicate emails");

        }

        private bool NoDuplicatePhoneNumbers(ContactFormModel contact)
        {
            return !_context.Contacts.Any(x => x.PhoneNumber == contact.PhoneNumber && x.Id != contact.Id);
        }

        private bool NoDuplicateEmails(ContactFormModel contact)
        {
            return !_context.Contacts.Any(x => x.Email == contact.Email && x.Id != contact.Id);
        }


    }
}
