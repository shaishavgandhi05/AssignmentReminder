﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Assignment_Reminder.ViewModels;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Scheduler;

namespace Assignment_Reminder
{
    public partial class AddAssign : PhoneApplicationPage
    {
        public AddAssign()
        {
            InitializeComponent();
        }

        private void Blah_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Blah.Text == "")
                Blah.Text = "Assignment Name";
        }

        private void Blah_GotFocus(object sender, RoutedEventArgs e)
        {
            Blah.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string assignname = Blah.Text;
            DateTime due = (DateTime)datePicker.Value;
            
            string dueDate = due.ToString();
            string completed = ((ListPickerItem)CompletedProperty.SelectedItem).Content.ToString();
            bool completedOrNot = Convert.ToBoolean(completed);
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(assignname) || assignname=="" || assignname=="Assignment Name")
            {
                MessageBox.Show("Either an assignment with this name already exists or you're screwing with us :)");
            }
            else {
                
                settings.Add(assignname,new ItemViewModel() { ID = "6", Title = assignname, Completed = completedOrNot, DueDate = dueDate });
                settings.Save();
                if(due.Date.AddDays(-1).AddHours(18) > DateTime.Now){
                Reminder reminder = new Reminder(assignname);
                reminder.Title = assignname;
                reminder.Content = "You have your " + assignname + " assignment due tomorrow";
                reminder.BeginTime = due.Date.AddDays(-1).AddHours(18);
                //reminder.ExpirationTime = new DateTime(1814, 08, 30, 11, 50, 30);
                //reminder.RecurrenceType = recurrence;
                reminder.NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative);
                ScheduledActionService.Add(reminder);
                    }
                NavigationService.GoBack();
            }
           
            //App.ViewModel.Items.Add(new ItemViewModel() { ID = "6", Title = assignname, Completed = completed, DueDate = dueDate });
           
        }
    }
}