﻿using GamesLibrary.Client.Core.ViewModel;
using GamesLibrary.Client.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GamesLibrary.Client.UI
{
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<Type, Page> pages;

        public MainPage()
        {
            pages = new Dictionary<Type, Page>();
            InitializeComponent();

            masterPage.ListView.ItemSelected += OnItemSelected;
            masterPage.SettingsListView.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as NavigationMenuItem;
            if (item != null)
            {
                Navigate(item.TargetViewModelType);
            }
        }

        public void Navigate(Type viewModelType)
        {
            if (!pages.ContainsKey(viewModelType))
            {
                //only cache specific pages
                if (viewModelType.Equals(typeof(GameArchiveViewModel)))
                {
                    pages.Add(viewModelType, new GameArchivePage());
                }
                else if (viewModelType.Equals(typeof(WatchlistViewModel)))
                {
                    pages.Add(viewModelType, new WatchlistPage());
                }
            }

            Detail = new NavigationPage(pages[viewModelType]);
            masterPage.ListView.SelectedItem = null;
            masterPage.SettingsListView.SelectedItem = null;

            //TODO: Einhetiliches Verfahrens für Auswertung ob aktuell als Overlay dargestellt wird
            if (Bounds.Width < 400)
            {
                IsPresented = false;
            }
        }
    }
}