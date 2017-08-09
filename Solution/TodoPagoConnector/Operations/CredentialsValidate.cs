using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoPagoConnector.Exceptions;
using TodoPagoConnector.Model;

namespace TodoPagoConnector.Operations
{
    internal class CredentialsValidate
    {
        public Boolean ValidateCredentials(User user)
        {
            Boolean result = true;

            if (user != null)
            {
                if (user.getUser() == null || user.getUser().Equals(""))
                    throw new EmptyFieldUserException("User/Mail is empty");

                if (user.getPassword() == null || user.getPassword().Equals(""))
                    throw new EmptyFieldPassException("Password is empty");
            }
            else
                throw new EmptyFieldPassException("User is null");

            return result;
        }
    }
}
