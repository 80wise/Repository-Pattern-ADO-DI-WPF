using Caliburn.Micro;
using Pattern_Repository.CSVRepositories;
using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ViewModels
{
    public class ShellViewModel
    {
        private IChatRepository chatRepository;
        public BindableCollection<Chat> chats { get; set; }

        public ShellViewModel(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }
        public ShellViewModel()
        {

        }

        public async Task fill(IChatRepository chatRepository)
        {
            
        }

    }
}
