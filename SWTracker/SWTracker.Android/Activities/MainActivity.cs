﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;
using Java.Text;
using Java.Util;
using SWTracker.Utilities;
using SWTracker.Classes;
using SWTracker.Droid.Activities;
using System.Collections.Generic;
using System.Linq;

namespace SWTracker.Droid
{
    [Activity(Label = "SWTracker.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        DBConnection dbConnection = new DBConnection();
        TextView numberText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            Button generateMyClass = FindViewById<Button>(Resource.Id.generateMyClass);
            Button makeDB = FindViewById<Button>(Resource.Id.makeDB);
            Button displayMyClass = FindViewById<Button>(Resource.Id.displayMyClass);
            Button deleteMyClass = FindViewById<Button>(Resource.Id.deleteMyClass);
            Button summonSessionButton = FindViewById<Button>(Resource.Id.SummonSessionButton);


            TextView idText = FindViewById<TextView>(Resource.Id.idText);
            numberText = FindViewById<TextView>(Resource.Id.numberText);

            button.Click += delegate {
                button.Text = string.Format("{0} clicks!", count++);
            };

            makeDB.Click += delegate {
                dbConnection.createDatabase(getFileDir());
            };

            summonSessionButton.Click += delegate {
                startNewSummonSession();
            };

            //generateMyClass.Click += delegate {
            //    addMyClass();
            //};

            //generateMyClass.Click += delegate
            //{
            //    Summon test = new Summon();
            //    var bird = test.SummonSession;
            //    var notbird = 1;
            //};

            //displayMyClass.Click += delegate {
            //    getClassNumber();
            //};

            deleteMyClass.Click += delegate
            {
                deleteClass();
            };

        }
        public string getFileDir()
        {
            return this.GetDatabasePath("Summons.db").AbsolutePath;
        }

        public async void startNewSummonSession()
        {
            SummonSession summonSession = new SummonSession();
            await dbConnection.insertUpdateData<SummonSession>(summonSession, getFileDir());

            //NOT NEEDED, GETS DATA FROM INSERT, WHOA
            //List<SummonSession> summonsessionList = await dbConnection.getSummonSessionList(getFileDir());
            //summonSession = summonsessionList.First(f => f.Date == summonsessionList.Max(g => g.Date)) ;
            Summon test = new Summon();
            test.SummonSessionID = summonSession.ID;
            test.Name = "Hello";
            //StartActivity(typeof(SummonSessionActivity));
        }

        //public async void addMyClass()
        //{
        //    MyClass myClass = new MyClass();
        //    myClass.testNum = 4;
        //    await db.myClassInsertUpdateData(myClass, getFileDir());
        //}
        //public async void getClassNumber()
        //{
        //    int number = await db.getNumberFromMyClass(getFileDir());
        //    numberText.Text = string.Format("The number is {0}!", number); ;
        //}
        //public async void removeMyClass()
        //{
        //    MyClass myClass = new MyClass();
        //    myClass.Id = 1;
        //    int number = await db.removeMyClass(myClass, getFileDir());
        //    numberText.Text = string.Format("Deleted!"); ;
        //}
        public async void deleteClass()
        {
            await dbConnection.myClassDeleteTable(getFileDir());
        }
    }
}


