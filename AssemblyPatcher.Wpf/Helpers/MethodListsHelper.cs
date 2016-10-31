using System.Collections.Generic;
using System.Linq;
using AssemblyPatcher.Wpf.ViewModels;
using Caliburn.Micro;


namespace AssemblyPatcher.Wpf.Helpers
{
    public static class MethodListsHelper
    {
        public static IList<MethodListViewModel> GetMethodLists(IList<MethodViewModel> methods = null)
        {
            var methodLists = new List<MethodListViewModel>();

            var publicList = new MethodListViewModel("Public");
            var privateList = new MethodListViewModel("Private");

            if (methods != null)
            {
                publicList.Methods = new BindableCollection<MethodViewModel>(methods.Where(m => m.MethodInfo.IsPublic));
                privateList.Methods = new BindableCollection<MethodViewModel>(methods.Where(m => !m.MethodInfo.IsPublic));
            }

            methodLists.Add(publicList);
            methodLists.Add(privateList);

            return methodLists;
        }
    }
}