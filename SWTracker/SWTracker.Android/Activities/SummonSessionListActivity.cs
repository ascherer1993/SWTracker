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
using System.Threading.Tasks;
using SWTracker.Classes;
using SWTracker.Utilities;

namespace SWTracker.Droid.Activities
{
    [Activity(Label = "SummonSessionListActivity")]
    public class SummonSessionListActivity : ListActivity
    {
        DBConnection db = new DBConnection();
        List<SummonSession> summonSessions;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            fetchSummonSessions();
            // Create your application here
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            fetchSummonSessions();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.AddMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public async Task fetchSummonSessions()
        {
            summonSessions = await db.getSummonSessionList(this.GetDatabasePath("Summons.db").AbsolutePath);

            ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, summonSessions.Select(x => x.ID + ": " + x.Date.ToString()).ToList());
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            Intent summonSessionIntent = new Intent(this, typeof(SummonSessionActivity));
            summonSessionIntent.PutExtra("summonSessionID", summonSessions[position].ID.ToString());
            StartActivity(summonSessionIntent);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //Open new contact activity
            switch (item.ItemId)
            {
                case Resource.Id.menu_add:
                    //Open Session
                    StartActivity(typeof(SummonSessionActivity));
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}