using System;
using System.Collections.Generic;
using System.Text;

namespace ByPetravetsKittens.ViewModels
{
    class ViewModelLocator
    {
        private UsersViewModel _usersViewModel;
        public UsersViewModel UsersViewModel
        {
            get
            {
                if (_usersViewModel == null)
                    _usersViewModel = new UsersViewModel();

                return _usersViewModel;
            }
        }
    }
}
