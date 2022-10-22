using DressManagement.WPF.Models;
using DressManagement.WPF.Models.Auth;
using DressManagement.WPF.Models.Common;
using DressManagement.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DressManagement.WPF
{
    public class MainWindowVM : PropertyChangeBaseModel
    {
        public MainWindowVM()
        {
            Activepagenumber = 1;
            WeatherStatus();
            GetPublishedVersion();
            CheckTemplateDirectory();
        }

        private int activepagenumber;
        private string weatherstatus;
        private string appversion;
        private string pagetitle;
        private string activeuser;

        public int Activepagenumber { get => activepagenumber; set { activepagenumber = value; RaisePropertyChanged("Activepagenumber"); } }
        public string Weatherstatus { get => weatherstatus; set { weatherstatus = value; RaisePropertyChanged("Weatherstatus"); } }
        public string Appversion { get => appversion; set { appversion = value; RaisePropertyChanged("Appversion"); } }
        public string Pagetitle { get => pagetitle; set { pagetitle = value; RaisePropertyChanged("Pagetitle"); } }
        public string Activeuser { get => activeuser; set { activeuser = value; RaisePropertyChanged("Activeuser"); } }

        #region Methods

        private async void WeatherStatus()
        {

            try
            {
                string lokasyon = ConfigurationManager.AppSettings["lokasyon"].ToString();
                string CurrentURL = "http://api.openweathermap.org/data/2.5/weather?q=" + lokasyon + "&lang=tr&mode=json&units=metric&APPID=d739b3c8f4d94c7da59c1601bcbf9132";

                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    string xmlContent = await client.DownloadStringTaskAsync(CurrentURL);

                    var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherModel.RootObject>(xmlContent);
                    Weatherstatus = lokasyon + " Hava Sıcaklık: " + obj.main.temp.ToString() + " °C" + " Hissedilen Sıcaklık: " + obj.main.feels_like.ToString() + " °C" + " Nem: %" + obj.main.humidity.ToString() + " Durum: " + obj.weather[0].description.ToString();
                }
                // notifier.ShowInformation("Hava Durumu Bilgisi Alındı");
            }
            catch (Exception ex)
            {
                Weatherstatus = "--";
                // notifier.ShowError("Hava Durumu Bilgisi Alınamadı");
                //MailGonder.SendMail("hava durumu cekilirken hata",ex.ToString());
            }
        }
        private void GetPublishedVersion()
        {
            Appversion = System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed
                ? "Version V1" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                : "Version V1.0.0.1";
        }
        public void GetActivePage(int pageTag)
        {
            switch (pageTag)
            {
                case (int)AppPages.BodysizePage:
                    Activepagenumber = ActiveUser.Authories.Contains(UserAuthory.BodySizeScreen) ? pageTag : -1;
                    Pagetitle = "Vücut Ölçüleri";
                    break;
                case (int)AppPages.CasePage:
                    Activepagenumber = ActiveUser.Authories.Contains(UserAuthory.CaseScreen) ? pageTag : -1;
                    Pagetitle = "Durumlar";
                    break;
                case (int)AppPages.CategoriesPages:
                    Activepagenumber = ActiveUser.Authories.Contains(UserAuthory.CategoriesScreen) ? pageTag : -1;
                    Pagetitle = "Categoriler";
                    break;
                case (int)AppPages.CompaniesPage:
                    Activepagenumber = ActiveUser.Authories.Contains(UserAuthory.CompaniesScreen) ? pageTag : -1;
                    Pagetitle = "Tanımlı Firmalar";
                    break;
                case (int)AppPages.CostumersPages:
                    Activepagenumber = ActiveUser.Authories.Contains(UserAuthory.CostumersScreen) ? pageTag : -1;
                    Pagetitle = "Tanımlı Müşteirler";
                    break;
                case (int)AppPages.PaymenttypesPages:
                    Activepagenumber = ActiveUser.Authories.Contains(UserAuthory.PaymenttypesScreen) ? pageTag : -1;
                    Pagetitle = "Ödeme Yöntemleri";
                    break;
                case (int)AppPages.SubcategoriesPages:
                    Activepagenumber = ActiveUser.Authories.Contains(UserAuthory.SubcategoriesScreen) ? pageTag : -1;
                    Pagetitle = "Alt Kategoriler";
                    break;
                case (int)AppPages.UnitsPages:
                    Activepagenumber = ActiveUser.Authories.Contains(UserAuthory.UnitsScreen) ? pageTag : -1;
                    Pagetitle = "Birimler";
                    break;
                default:
                    break;
            }
        }
        private void CheckTemplateDirectory()
        {

            DirectoryInfo fi1 = new DirectoryInfo("C:\\DressManagement");
            if (!fi1.Exists)
            {
                fi1.Create();
            }

            DirectoryInfo fi2 = new DirectoryInfo("C:\\DressManagement\\Templates");
            if (!fi2.Exists)
            {
                fi2.Create();
            }
        }
        #endregion
    }
}
