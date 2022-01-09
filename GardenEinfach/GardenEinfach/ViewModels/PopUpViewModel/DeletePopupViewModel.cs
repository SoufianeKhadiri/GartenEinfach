using GardenEinfach.ViewModels;
using Prism.Commands;
using Prism.Events;
using Rg.Plugins.Popup.Services;

namespace GardenEinfach.ViewModels
{
    
    public class DeletePopupViewModel : BaseViewModel
    {
      
        public DeletePopupViewModel()
        {
           
        }
        private DelegateCommand _ConfirmDelete;
        public DelegateCommand ConfirmDelete =>
        _ConfirmDelete ?? (_ConfirmDelete = new DelegateCommand(ConfirmDeleteM));

        void ConfirmDeleteM()
        {
            MyPostsViewModel.ConfirmDeleteCommand.Execute();

            PopupNavigation.Instance.PopAsync(true);
        }

        private DelegateCommand _CancelDelete;
        public DelegateCommand CancelDelete =>
        _CancelDelete ?? (_CancelDelete = new DelegateCommand(CancelDeleteM));

        void CancelDeleteM()
        {
            
            PopupNavigation.Instance.PopAsync(true);
        }

    }
}
