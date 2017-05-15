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

            #region UIElements
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
            #endregion
            summonSessionID = Intent.GetStringExtra("summonSessionID");
            if (summonSessionID != null)
            {
                getSummonSessionFromDB();
            }

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

                if (mysticScrollRadioButton.Checked)
                {
                    summon.SummonTypeID = 1;
                }
                else if (summoningStonesRadioButton.Checked)
                {
                    summon.SummonTypeID = 2;
                }
                else if (lightDarkScrollRadioButton.Checked)
                {
                    summon.SummonTypeID = 3;
                }
                else if (lengendaryScrollRadioButton.Checked)
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
                int starRadioButtonID = starNumberRadioGroup.CheckedRadioButtonId;
                View selectedStarRadioButton = starNumberRadioGroup.FindViewById(starRadioButtonID);

                summon.Stars = starNumberRadioGroup.IndexOfChild(selectedStarRadioButton);
                #endregion

                //Name
                if (!String.IsNullOrEmpty(monsterNameEditText.Text))
                {
                    summon.Name = monsterNameEditText.Text;
                }
                else
                {
                    summon.Name = "";
                }

                //Insert
                await db.insertUpdateData<Summon>(summon, this.GetDatabasePath("Summons.db").AbsolutePath);

                //Calculate Summon Rates
                calculateRates();
            }
            else
            {
                if (summonSessionID != null)
                {
                    getSummonSessionFromDB();
                    AddSummonToSummonSession();
                }
                else
                {
                    SummonSession newSummonSession = new SummonSession();
                    newSummonSession.Date = DateTime.Now;
                    await db.insertUpdateData(newSummonSession, this.GetDatabasePath("Summons.db").AbsolutePath);
                    summonSession = newSummonSession;
                    AddSummonToSummonSession();
                }
                
            }
        }
        public async void getSummonSessionFromDB()
        {
            int id;
            bool success = Int32.TryParse(summonSessionID, out id);
            if (success)
            {
                summonSession = await db.getSummonSession(this.GetDatabasePath("Summons.db").AbsolutePath, id);
                calculateRates();
            }
            else
            {
                Toast.MakeText(this, "Invalid ID", ToastLength.Long);
            }
        }
        public async void calculateRates()
        {
            int id;
            int totalNumberOfSummons;
            int totalThreeStars;
            int totalFourStars;
            int totalFiveStars;
            double threeStarRate;
            double fourStarRate;
            double fiveStarRate;


            id = summonSession.ID;
            
            totalNumberOfSummons = await db.getNumOfSummons(this.GetDatabasePath("Summons.db").AbsolutePath, id, null);
            totalThreeStars = await db.getNumOfSummons(this.GetDatabasePath("Summons.db").AbsolutePath, id, 3);
            totalFourStars = await db.getNumOfSummons(this.GetDatabasePath("Summons.db").AbsolutePath, id, 4);
            totalFiveStars = await db.getNumOfSummons(this.GetDatabasePath("Summons.db").AbsolutePath, id, 5);

            threeStarRate = Math.Round((double)(totalThreeStars * 100) / totalNumberOfSummons, 2);
            fourStarRate = Math.Round((double)(totalFourStars * 100) / totalNumberOfSummons, 2);
            fiveStarRate = Math.Round((double)(totalFiveStars * 100) / totalNumberOfSummons, 2);

            totalSummonsTextView.Text = totalNumberOfSummons.ToString();
            threeStarSummonRateTextView.Text = threeStarRate + "%";
            fourStarSummonRateTextView.Text = fourStarRate + "%";
            fiveStarSummonRateTextView.Text = fiveStarRate + "%";
        }
    }
}