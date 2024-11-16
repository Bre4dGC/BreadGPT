using BreadGPT.Commands;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    class WindowViewModel : BaseViewModel
    {
        public ICommand CreateChatCommand { get; set; }


        public WindowViewModel()
        {
            CreateChatCommand = new CreateChatCommand();
        }
    }
}
