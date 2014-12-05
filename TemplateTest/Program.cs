using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TemplateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const string siteJsonOverride = "{'MyBaseInfo':{'ContestName':'My Site', 'ContestId':'3'},'MySocialInfo':{}}";
            const string marketJsonOverride = "{'MyBaseInfo':{'ContestName':'My Market', 'IsActive':false},'MySocialInfo':{}}";
            const string broadcasterJsonOverride = "{'MyBaseInfo':{'ContestName':'My Broadcaster','ContestId':'2'},'MySocialInfo':{}}";

            var siteTemplateOverride = JsonConvert.DeserializeObject<MyTemplate>(siteJsonOverride);
            var marketTemplateOverride = JsonConvert.DeserializeObject<MyTemplate>(marketJsonOverride);
            var broadcasterTemplateOverride = JsonConvert.DeserializeObject<MyTemplate>(broadcasterJsonOverride);

            var myTemplate = new MyTemplate()
            {
                MyBaseInfo = new TemplateBaseInfo()
                {
                    ContestName = "Name1",
                    ContestId = 1,
                    IsActive = true
                },
                MySocialInfo = new TemplateSocialInfo()
                {
                    FacebookPostToWallText = "PostToWallText1"
                }
            };

            MyMapper(myTemplate, broadcasterTemplateOverride);
            MyMapper(myTemplate, marketTemplateOverride);
            MyMapper(myTemplate, siteTemplateOverride);
        }

        public static void MyMapper(MyTemplate myTemplate, MyTemplate myOverride)
        {
            #region BaseInfo
            myTemplate.MyBaseInfo.ContestName = myOverride.MyBaseInfo.ContestName ?? myTemplate.MyBaseInfo.ContestName;
            myTemplate.MyBaseInfo.ContestId = myOverride.MyBaseInfo.ContestId ?? myTemplate.MyBaseInfo.ContestId;
            myTemplate.MyBaseInfo.IsActive = myOverride.MyBaseInfo.IsActive ?? myTemplate.MyBaseInfo.IsActive;
            #endregion

            #region SocialInfo
            myTemplate.MySocialInfo.FacebookPostToWallText = myOverride.MySocialInfo.FacebookPostToWallText ??
                                                             myTemplate.MySocialInfo.FacebookPostToWallText;
            #endregion
        }

        [Serializable]
        public class MyTemplate 
        {
            public TemplateBaseInfo MyBaseInfo { get; set; }
            public TemplateSocialInfo MySocialInfo { get; set; }
        }

        [Serializable]
        public class TemplateBaseInfo
        {
            public string ContestName { get; set; }
            public int? ContestId { get; set; }
            public bool? IsActive { get; set; }
        }

        [Serializable]
        public class TemplateSocialInfo
        {
            public string FacebookPostToWallText { get; set; }
        }
    }
}
