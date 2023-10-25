using DocVisionClient.Services;
using DocVisionClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DocVisionClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        private readonly ISendService _sendService;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(ISendService sendService, IServiceProvider serviceProvider)
        {    
            InitializeComponent();
            _sendService = sendService;
            Manager.MainFrame = MainFrame;
            _serviceProvider = serviceProvider;
            Manager.MainFrame.Navigate(new StartPage());     
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
            => Manager.MainFrame.GoBack();

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            BackButton.Visibility = Manager.MainFrame.CanGoBack ?
            Visibility.Visible : Visibility.Hidden;

            if(Manager.MainFrame.Content is SenderPage)
            {
                SendButton.Visibility = Visibility.Visible;
                DataContext = _serviceProvider.GetRequiredService<MessageViewModel>();
            }
            else
            {
                SendButton.Visibility = Visibility.Hidden;
            }
              
        }

    }
}
