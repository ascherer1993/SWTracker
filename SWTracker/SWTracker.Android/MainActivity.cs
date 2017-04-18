using System;

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

namespace SWTracker.Droid
{
	[Activity (Label = "SWTracker.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
        DBConnection db = new DBConnection();
        TextView numberText;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
            Button generateMyClass = FindViewById<Button>(Resource.Id.generateMyClass);
            Button makeDB = FindViewById<Button>(Resource.Id.makeDB);
            Button displayMyClass = FindViewById<Button>(Resource.Id.displayMyClass);
            Button deleteMyClass = FindViewById<Button>(Resource.Id.deleteMyClass);



            TextView idText = FindViewById<TextView>(Resource.Id.idText);
            numberText = FindViewById<TextView>(Resource.Id.numberText);

            button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};

            makeDB.Click += delegate {
                db.createDatabase(getFileDir());
            };

            generateMyClass.Click += delegate {
                addMyClass();
            };

            displayMyClass.Click += delegate {
                getClassNumber();
            };

            deleteMyClass.Click += delegate {
                removeMyClass();
            };
        }
        public string getFileDir()
        {
            return this.GetDatabasePath("Summons.db").AbsolutePath;
        }
        public async void addMyClass()
        {
            MyClass myClass = new MyClass();
            myClass.testNum = 4;
            await db.myClassInsertUpdateData(myClass, getFileDir());
        }
        public async void getClassNumber()
        {
            int number = await db.getNumberFromMyClass(getFileDir());
            numberText.Text = string.Format("The number is {0}!", number); ;
        }
        public async void removeMyClass()
        {
            MyClass myClass = new MyClass();
            myClass.Id = 1;
            int number = await db.removeMyClass(myClass, getFileDir());
            numberText.Text = string.Format("Deleted!"); ;
        }
    }
}


