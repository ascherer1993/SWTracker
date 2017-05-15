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
using Android.Views.InputMethods;

namespace SWTracker.Droid.Activities
{
    [Activity(Label = "SummonSessionActivity")]
    public class SummonSessionActivity : Activity
    {
        public SummonSession summonSession;
        DBConnection db = new DBConnection();

        string summonSessionID;


        #region UIElements
        EditText monsterNameEditText;
        Button saveSummonButton;
        RadioGroup scrollTypeRadioGroup;
        RadioGroup starNumberRadioGroup;
        TextView totalSummonsTextView;
        TextView threeStarSummonRateTextView;
        TextView fourStarSummonRateTextView;
        TextView fiveStarSummonRateTextView;

        RadioButton mysticScrollRadioButton;
        RadioButton summoningStonesRadioButton;
        RadioButton lightDarkScrollRadioButton;
        RadioButton lengendaryScrollRadioButton;
        RadioButton otherRadioButton;

        RadioButton oneStarRadioButton;
        RadioButton twoStarRadioButton;
        RadioButton threeStarRadioButton;
        RadioButton fourStarRadioButton;
        RadioButton fiveStarRadioButton;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SummonSession);

            monsterNameEditText = FindViewById<EditText>(Resource.Id.monsterNameET);
            saveSummonButton = FindViewById<Button>(Resource.Id.saveSummonB);

            scrollTypeRadioGroup = FindViewById<RadioGroup>(Resource.Id.scrollTypeRG);
            starNumberRadioGroup = FindViewById<RadioGroup>(Resource.Id.starNumberRG);

            totalSummonsTextView = FindViewById<TextView>(Resource.Id.totalSummonsTV);
            threeStarSummonRateTextView = FindViewById<TextView>(Resource.Id.threeStarSummonRateTV);
            fourStarSummonRateTextView = FindViewById<TextView>(Resource.Id.fourStarSummonRateTV);
            fiveStarSummonRateTextView = FindViewById<TextView>(Resource.Id.fiveStarSummonRateTV);

            mysticScrollRadioButton = FindViewById<RadioButton>(Resource.Id.mysticScrollRB);
            summoningStonesRadioButton = FindViewById<RadioButton>(Resource.Id.summoningStonesRB);
            lightDarkScrollRadioButton = FindViewById<RadioButton>(Resource.Id.LightDarkScrollRB);
            lengendaryScrollRadioButton = FindViewById<RadioButton>(Resource.Id.legendaryScrollRB);
            otherRadioButton = FindViewById<RadioButton>(Resource.Id.otherRB);

            oneStarRadioButton = FindViewById<RadioButton>(Resource.Id.oneStarRB);
            twoStarRadioButton = FindViewById<RadioButton>(Resource.Id.twoStarRB);
            threeStarRadioButton = FindViewById<RadioButton>(Resource.Id.threeStarRB);
            fourStarRadioButton = FindViewById<RadioButton>(Resource.Id.fourStarRB);
            fiveStarRadioButton = FindViewById<RadioButton>(Resource.Id.fiveStarRB);

            summonSessionID = Intent.GetStringExtra("ID");


            saveSummonButton.Click += delegate
            {
                AddSummonToSummonSession();
            };

            // Create your application here
            //View view = this.CurrentFocus;
            //if (view != null)
            //{
            //    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            //    imm.HideSoftInputFromWindow(view.WindowToken, 0);
            //}

        }

        public async void AddSummonToSummonSession()
        {
            if (summonSession != null)
            {
                Summon summon = new Summon();

                //SessionID
                summon.SummonSessionID = summonSession.ID;

                //Scroll Type
                #region scrollType
                //I used this method instead of just getting the index and assigning
                //that because I thought this was a lot more clear. But it is a lot more
                //effort. Also, if I ever added another option above other, that
                //wouldn't work.
                int scrollRadioButtonID = scrollTypeRadioGroup.CheckedRadioButtonId;

                if (scrollRadioButtonID == mysticScrollRadioButton.Id)
                {
                    summon.SummonTypeID = 1;
                }
                else if (scrollRadioButtonID == summoningStonesRadioButton.Id)
                {
                    summon.SummonTypeID = 2;
                }
                else if (scrollRadioButtonID == lightDarkScrollRadioButton.Id)
                {
                    summon.SummonTypeID = 3;
                }
                else if (scrollRadioButtonID == lengendaryScrollRadioButton.Id)
                {
                    summon.SummonTypeID = 4;
                }
                //Other
                else
                {
                    summon.SummonTypeID = 5;
                }
                #endregion
                //Star Number
                #region starNumber
                //Unlike above, this is immutable, so I thought an index approach
                //was acceptable
                int starRadioButtonID = scrollTypeRadioGroup.CheckedRadioButtonId;
                View selectedStarRadioButton = starNumberRadioGroup.FindViewById(starRadioButtonID);
                summon.Stars = starNumberRadioGroup.IndexOfChild(selectedStarRadioButton) + 1;
                #endregion

                //Name
                if (!String.IsNullOrEmpty(monsterNameEditText.Text))
                {
                    summon.Name = monsterNameEditText.Text;
                }

                //Insert
                await db.insertUpdateData<Summon>(summon, this.GetDatabasePath("Summons.db").AbsolutePath);

                //Calculate Summon Rates
                calculateRates();
            }
            else
            {
                bool success;
                int id;
                if (summonSessionID != null)
                {
                    success = Int32.TryParse(summonSessionID, out id);
                    if (success)
                    {
                        summonSession = await db.getSummonSession(this.GetDatabasePath("Summons.db").AbsolutePath, id);
                        AddSummonToSummonSession();
                    }
                    else
                    {
                        Toast.MakeText(this, "Invalid ID", ToastLength.Long);
                    }
                }
                else
                {
                    SummonSession newSummonSession = new SummonSession();
                    await db.insertUpdateData(newSummonSession, this.GetDatabasePath("Summons.db").AbsolutePath);
                    summonSession = newSummonSession;
                    AddSummonToSummonSession();
                }
                
            }
        }
        public void calculateRates()
        {
            fourStarSummonRateTextView.Text = "changed";
        }
    }
}