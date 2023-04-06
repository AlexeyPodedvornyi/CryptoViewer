using CryptoViewer.Core;
using CryptoViewer.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoViewer.MVVM.ViewModel
{
    public class InfoViewModel : ObservableObject
    {
        private DetailedCryptoCurrency _requiredItem;
        private string _searchParam="";
        private List<DetailedCryptoCurrency> _navList;
        private int _position;
        private RelayCommand _navForward;
        private RelayCommand _navBackward;
        public RelayCommand NavForward
        {
            get
            {
                if(_navForward == null)
                {
                    _navForward = new RelayCommand(o =>
                    {
                        if (NavList != null && NavList.Count > 1 && _position < NavList.Count - 1)
                        {
                            _position++;
                            RequiredItem = NavList[_position];
                        }
                    });
                }

                return _navForward;
            }
        }
        public RelayCommand NavBackward
        {
            get
            {
                if (_navBackward == null)
                {
                    _navBackward = new RelayCommand(o =>
                    {
                        if(_position!=0)
                        {
                            _position--;
                            RequiredItem = NavList[_position];
                        }                      
                    });
                }

                return _navBackward;
            }

        }
        public List<DetailedCryptoCurrency> NavList
        {
            get => _navList;
            set
            {
                _navList = value;
                if (_navList.Count>1) 
                {
                    _position = 0;
                }
                OnPropertyChanged();
            }
        }
        public DetailedCryptoCurrency RequiredItem { 
            get=>_requiredItem;
            set 
            {
                _requiredItem = value;
                OnPropertyChanged();
            }
        }
        
        public string SearchParam
        {
            get => _searchParam;
            set
            {
                _searchParam = value;
                NavList = Task.Run(async () => await DetailedCryptoCurrency.GetByNameOrId(value)).Result;
                if (NavList.Count > 0)
                {
                    RequiredItem = NavList[_position];
                }
                if(NavList.Count == 0) 
                {
                    RequiredItem = new DetailedCryptoCurrency("there are no matching items", 0, "not found", 0.00, 0.00, 0.00);
                }
                OnPropertyChanged(nameof(SearchParam));
            }
        }

        public InfoViewModel()
        {
            _navList = new List<DetailedCryptoCurrency>();
        }

        private MainViewModel _parentViewModel;
        public InfoViewModel(MainViewModel parentViewModel) 
        {
            _parentViewModel = parentViewModel;
            _parentViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_parentViewModel.Data))
                {
                    SearchParam = _parentViewModel.Data;
                }
                if(args.PropertyName == nameof(_parentViewModel.SelectedCurr))
                {
                    RequiredItem = _parentViewModel.SelectedCurr;
                }
            };
        }

    }
}
