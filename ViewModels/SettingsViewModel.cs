using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TeamTasker.Core;
using static Azure.Core.HttpHeader;

namespace TeamTasker.ViewModels
{
    public class SettingsViewModel:ObservableObject
    {
        private ObservableCollection<ComboBoxItem> languageItems;
        public ObservableCollection<ComboBoxItem> LanguageItems 
        {
            get
            {
                return languageItems;
            }
            set
            {
                languageItems = value;
                OnPropertyChanged();
            }
        }
        private ComboBoxItem _currentLanguage;
        public ComboBoxItem CurrentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
                App.Language = (CultureInfo) _currentLanguage.Tag;
                OnPropertyChanged();
            }
        }
        public SettingsViewModel() 
        {
            LanguageItems=new ObservableCollection<ComboBoxItem>();
            foreach (var lang in App.Languages)
            {
                ComboBoxItem comboItem = new ComboBoxItem();
                comboItem.Content = lang.DisplayName;
                comboItem.Tag = lang;
                if (lang.Equals(App.Language))
                {
                    comboItem.IsSelected = true;
                }
                LanguageItems.Add(comboItem);
            }
        }
    }
}
