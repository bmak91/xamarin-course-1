using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageVM();
        }
    }

    public class MainPageVM : BaseVM
    {
        private int _count;

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        public ICommand ClickCommand { get; set; }

        public MainPageVM()
        {
            ClickCommand = new Command(OnClick, CheckCanClick);
        }

        private async void OnClick()
        {
            Count++;

            CanClick = false;

            // CallAPI()
            await Task.Run(() => Thread.Sleep(3000));

            CanClick = true;
        }

        private bool _canClick = true;
        private bool CanClick
        {
            get => _canClick;
            set
            {
                _canClick = value;
                ((Command)ClickCommand).ChangeCanExecute();
            }
        }

        private bool CheckCanClick()
        {
            return CanClick;
        }
    }

    public class BaseVM : INotifyPropertyChanged
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
