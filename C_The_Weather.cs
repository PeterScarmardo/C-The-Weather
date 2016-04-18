using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebService

{
    
    public partial class C_The_Weather : Form

    {
        
        public C_The_Weather()

        {

            InitializeComponent();

        }

        private void getWeatherButton_Click(object sender, EventArgs e)

        {

            string zipCode = zipCodeTextBox.Text;

            String conditionsUrl = "http://api.wunderground.com/api/b612a06d17e8eb1d/conditions/q/" + zipCode + ".json";
            WebRequest conditionsWebRequest = WebRequest.Create(conditionsUrl);
            WebResponse conditionsResponse = conditionsWebRequest.GetResponse();
            StreamReader conditionsStreamReader = new StreamReader(conditionsResponse.GetResponseStream());
            String conditionsResponseData = conditionsStreamReader.ReadToEnd();

            var conditionsOutObject = JsonConvert.DeserializeObject<Conditions.RootObject>(conditionsResponseData);

            String forecastUrl = "http://api.wunderground.com/api/b612a06d17e8eb1d/forecast/q/" + zipCode + ".json";
            WebRequest forecastWebRequest = WebRequest.Create(forecastUrl);
            WebResponse forecastResponse = forecastWebRequest.GetResponse();
            StreamReader forecastStreamReader = new StreamReader(forecastResponse.GetResponseStream());
            String forecastResponseData = forecastStreamReader.ReadToEnd();

            var forecastOutObject = JsonConvert.DeserializeObject<WeeklyForecast.RootObject>(forecastResponseData);

            cityTextBox.Text = conditionsOutObject.current_observation.display_location.city;
            stateTextBox.Text = conditionsOutObject.current_observation.display_location.state;
            countryTextBox.Text = conditionsOutObject.current_observation.display_location.country;
            latitudeTextBox.Text = conditionsOutObject.current_observation.display_location.latitude;
            longitudeTextBox.Text = conditionsOutObject.current_observation.display_location.longitude;
            elevationTextBox.Text = conditionsOutObject.current_observation.display_location.elevation + " ft";
            localTimeTextBox.Text = conditionsOutObject.current_observation.local_time_rfc822;

            temperatureTextBox.Text = System.Convert.ToString(conditionsOutObject.current_observation.temp_f + " F");
            feelsLikeTextBox.Text = conditionsOutObject.current_observation.feelslike_f + " F";
            dewPointTextBox.Text = System.Convert.ToString(conditionsOutObject.current_observation.dewpoint_f + " F");
            windTextBox.Text = conditionsOutObject.current_observation.wind_dir + " " + conditionsOutObject.current_observation.wind_mph + " MPH";
            precipitationTextBox.Text = conditionsOutObject.current_observation.precip_1hr_in;
            pressureTextBox.Text = conditionsOutObject.current_observation.pressure_trend + " " + conditionsOutObject.current_observation.pressure_in;
            visibilityTextBox.Text = conditionsOutObject.current_observation.visibility_mi + " Miles";
            conditionsFullNameLabel.Text = conditionsOutObject.current_observation.display_location.full;

            String iconUrl = conditionsOutObject.current_observation.icon_url;
            pictureBox2.ImageLocation = iconUrl.ToString();
            pictureBox2.Load();

            tabControl1.SelectedIndex = 1;

            periodZeroLabel.Text = forecastOutObject.forecast.txt_forecast.forecastday[0].title;
            String periodZeroUrl = forecastOutObject.forecast.txt_forecast.forecastday[0].icon_url;
            periodZeroPictureBox.ImageLocation = periodZeroUrl.ToString();
            periodZeroPictureBox.Load();
            periodZeroTextBox.Text = forecastOutObject.forecast.txt_forecast.forecastday[0].fcttext;

            periodOneLabel.Text = forecastOutObject.forecast.txt_forecast.forecastday[1].title;
            String periodOneUrl = forecastOutObject.forecast.txt_forecast.forecastday[1].icon_url;
            periodOnePictureBox.ImageLocation = periodOneUrl.ToString();
            periodOnePictureBox.Load();
            periodOneTextBox.Text = forecastOutObject.forecast.txt_forecast.forecastday[1].fcttext;

            periodTwoLabel.Text = forecastOutObject.forecast.txt_forecast.forecastday[2].title;
            String periodTwoUrl = forecastOutObject.forecast.txt_forecast.forecastday[2].icon_url;
            periodTwoPictureBox.ImageLocation = periodTwoUrl.ToString();
            periodTwoPictureBox.Load();
            periodTwoTextBox.Text = forecastOutObject.forecast.txt_forecast.forecastday[2].fcttext;

            periodThreeLabel.Text = forecastOutObject.forecast.txt_forecast.forecastday[3].title;
            String periodThreeUrl = forecastOutObject.forecast.txt_forecast.forecastday[3].icon_url;
            periodThreePictureBox.ImageLocation = periodThreeUrl.ToString();
            periodThreePictureBox.Load();
            periodThreeTextBox.Text = forecastOutObject.forecast.txt_forecast.forecastday[3].fcttext;

            periodFourLabel.Text = forecastOutObject.forecast.txt_forecast.forecastday[4].title;
            String periodFourUrl = forecastOutObject.forecast.txt_forecast.forecastday[4].icon_url;
            periodFourPictureBox.ImageLocation = periodFourUrl.ToString();
            periodFourPictureBox.Load();
            periodFourTextBox.Text = forecastOutObject.forecast.txt_forecast.forecastday[4].fcttext;

            periodFiveLabel.Text = forecastOutObject.forecast.txt_forecast.forecastday[5].title;
            String periodFiveUrl = forecastOutObject.forecast.txt_forecast.forecastday[5].icon_url;
            periodFivePictureBox.ImageLocation = periodFiveUrl.ToString();
            periodFivePictureBox.Load();
            periodFiveTextBox.Text = forecastOutObject.forecast.txt_forecast.forecastday[5].fcttext;

            periodSixLabel.Text = forecastOutObject.forecast.txt_forecast.forecastday[6].title;
            String periodSixUrl = forecastOutObject.forecast.txt_forecast.forecastday[6].icon_url;
            periodSixPictureBox.ImageLocation = periodSixUrl.ToString();
            periodSixPictureBox.Load();
            periodSixTextBox.Text = forecastOutObject.forecast.txt_forecast.forecastday[6].fcttext;

        }

        private void zipCodeTextBox_Leave(object sender, EventArgs e)

        {

            if (!Regex.Match(zipCodeTextBox.Text, "^[0-9]{5}(?:-[0-9]{4})?$", RegexOptions.IgnoreCase).Success)

            {

                MessageBox.Show("You must enter a valid zip. A zip code can be entered in the format of 12345 or 12345-6789.");
                zipCodeTextBox.Clear();
                zipCodeTextBox.Focus();

            }
        }

        private void closeButton_Click(object sender, EventArgs e)

        {

            Close();

        }
    }
}
