﻿using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.XamForms;
using RxUIDemoApp.Models;
using RxUIDemoApp.ViewModels;
using Xamarin.Forms;

namespace RxUIDemoApp.Views
{
    public partial class SearchDemoPage : ReactiveContentPage<SearchPageViewModel>
    {
        public SearchDemoPage()
        {
            InitializeComponent();

            Observable.FromEventPattern<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(
                    x => SearchBar.TextChanged += x,
                    x => SearchBar.TextChanged -= x)
                .Throttle(TimeSpan.FromSeconds(2), TaskPoolScheduler.Default)
                .Select(args => args.EventArgs.NewTextValue)
                .Where(txt => !string.IsNullOrWhiteSpace(txt))
                .Subscribe(text => { ViewModel.SearchResults.Add(new SearchResults { Description = text }); });

            Observable.FromEventPattern<EventHandler<SelectedItemChangedEventArgs>, SelectedItemChangedEventArgs>(
                    x => ResultsList.ItemSelected += x,
                    x => ResultsList.ItemSelected -= x)
                .Select(args => args.EventArgs.SelectedItem)
                .Subscribe(item =>
                {
                    ResultsList.SelectedItem = null;
                    Console.WriteLine(item);
                });

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.SearchResults, c => c.ResultsList.ItemsSource);
            });
        }
    }
}
