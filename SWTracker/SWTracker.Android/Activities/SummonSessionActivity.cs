using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SWTracker.Classes;
using SWTracker.Utilities;

namespace SWTracker.Droid.Activities
{
    [Activity(Label = "SummonSessionActivity")]
    public class SummonSessionActivity : Activity
    {
        public SummonSession summonSession;
        DBConnection db = new DBConnection();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        public async void AddSummonToSummonSession()
        {
            Summon summon = new Summon();
            summon.SummonSessionID = summonSession.ID;
            await db.insertUpdateData<Summon>(summon, this.GetDatabasePath("Summons.db").AbsolutePath);
        }
    }
}