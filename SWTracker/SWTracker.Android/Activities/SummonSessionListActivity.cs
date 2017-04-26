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

namespace SWTracker.Droid.Activities
{
    [Activity(Label = "SummonSessionListActivity")]
    public class SummonSessionListActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.AddMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public async Task fetchContacts()
        {
            //contacts = await service.fetchContacts();

            //ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, contacts.Select(x => x.FirstName + " " + x.LastName).ToList());
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            //Intent modify = new Intent(this, typeof(ModifyContactActivity));
            //modify.PutExtra("firstName", contacts[position].FirstName);
            //modify.PutExtra("lastName", contacts[position].LastName);
            //if (contacts[position].Email != null)
            //{
            //    modify.PutExtra("email", contacts[position].Email);
            //}
            //if (contacts[position].PhoneNumber != null)
            //{
            //    modify.PutExtra("phoneNumber", contacts[position].PhoneNumber);
            //}
            //if (contacts[position].ContactID != null)
            //{
            //    modify.PutExtra("contactId", contacts[position].ContactID ?? -1);
            //}
            //StartActivity(modify);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //Open new contact activity
            switch (item.ItemId)
            {
                //case Resource.Id.menu_add:
                //    //Open Session
                //    //StartActivity(typeof(ModifyContactActivity));
                //    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}