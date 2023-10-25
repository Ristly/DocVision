using DocVisionClient.Services;
using DocVisionClient.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DocVisionClient.ViewModels;

public class MessageViewModel:INotifyPropertyChanged
{
    private Message _message;
    private ISendService _sendService;

    public MessageViewModel(ISendService sendService)
    {
        _sendService = sendService;
        _message = new Message();
    }

    private CustomCommand _sendCommand;
    public CustomCommand SendCommand
    {
        get
        {
            return _sendCommand ??
                (_sendCommand = new CustomCommand(async obj
                    => {
                        if (!PropertyChecker())
                        {
                            MessageBox.Show("Не все поля заполнены", "Ошибка");
                            return;
                        }
                           

                        var result = _sendService.SendMessage(_message);
                        Manager.MainFrame.IsEnabled = false;
                        while (!result.IsCompleted)
                            MessageBox.Show("Отправка сообщения...\nПодождите", "Обработка сообщения");
                        
                        if((await result))
                        {
                            MessageBox.Show("Сообщение обработано","Успешно");
                            Manager.MainFrame.IsEnabled = true;
                            Manager.MainFrame.GoBack();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка сервера\nПопробуйте снова", "Ошибка");
                            Manager.MainFrame.IsEnabled = true;
                        }
                       
                        }
                ));
        }
    }

    public string Title
    {
        get { return _message.Title; }
        set
        {
            _message.Title = value;
            OnPropertyChanged("Title");
        }
    }
    public DateTime SendDate
    {
        get { return _message.SendDate.Date; }
        set
        {
            _message.SendDate = value.Date;
            OnPropertyChanged("SendDate");
        }
    }
    public string Addressee {
        get { return _message.Addressee; }
        set
        {
            _message.Addressee = value;
            OnPropertyChanged("Addressee");
        }
    }
    public string Sender
    {
        get { return _message.Sender; }
        set
        {
            _message.Sender = value;
            OnPropertyChanged("Sender");
        }
    }
    public string Content
    {
        get { return _message.Content; }
        set
        {
            _message.Content = value;
            OnPropertyChanged("Content");
        }
    }


    public bool PropertyChecker()
    {
        if(string.IsNullOrWhiteSpace(_message.Title))
            return false;
        if(string.IsNullOrWhiteSpace(_message.Addressee))
            return false;
        if(string.IsNullOrWhiteSpace(_message.Sender))
            return false;
        if(string.IsNullOrWhiteSpace(_message.Content))
            return false;

        _message.SendDate.ToUniversalTime();
        return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
