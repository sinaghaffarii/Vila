using Vila.WebApi.Context;
using Vila.WebApi.CustomerModels;
using Vila.WebApi.Models;
using Vila.WebApi.Utility;

namespace Vila.WebApi.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        public CustomerService(DataContext context)
        {
            _context = context;
        }
        public bool ExistMobile(string mobile) =>
            _context.Customers.Any(x => x.Mobile.Trim() == mobile.Trim());

        public Customers Login(string mobile, string pass)
        {
            throw new NotImplementedException();
        }

        public bool PasswordIsCorrect(string mobile, string pass)
        {
            throw new NotImplementedException();
        }

        public bool Register(RegisterModel model)
        {
            var hashPass = PasswordHelper.EncodeProSecurity(model.Pass.Trim());
            Models.Customers customer = new()
            {
                Mobile = model.Mobile,
                Pass = hashPass,
                Role = "user"
            };
           

            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

      
    }
}
