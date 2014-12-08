using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TemplateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.CreateMap<TemplateBaseInfo, OrgTemplateBaseInfo>().ForMember(x => x.RegFields, config => config.Ignore());
            Mapper.CreateMap<TemplateSocialInfo, OrgTemplateSocialInfo>();

            const string siteJsonOverride = "{'MyBaseInfo':{'ContestName':'My Site', 'ContestId':'3', 'RegFields':[{'FieldId':1,'FieldValue':'reg111'}]},'MySocialInfo':{}}";
            const string marketJsonOverride = "{'MyBaseInfo':{'ContestName':'My Market', 'IsActive':false, 'RegFields':[{'FieldId':1,'FieldValue':'reg111reg111'}, {'FieldId':2,'FieldValue':'reg222'}]},'MySocialInfo':{}}";
            const string broadcasterJsonOverride = "{'MyBaseInfo':{'ContestName':'My Broadcaster','ContestId':'2'},'MySocialInfo':{}}";

            //var siteTemplateOverride = JsonConvert.DeserializeObject<MyTemplate>(siteJsonOverride);
            //var marketTemplateOverride = JsonConvert.DeserializeObject<MyTemplate>(marketJsonOverride);
            var broadcasterTemplateOverride = JsonConvert.DeserializeObject<MyTemplate>(broadcasterJsonOverride);

            var orgTemplate = new OrgTemplate()
            {
                OrgBaseInfo = new OrgTemplateBaseInfo()
                {
                    ContestName = "Name1",
                    ContestId = 1,
                    IsActive = true,
                    RegFields = new List<RegField>()
                },
                OrgSocialInfo = new OrgTemplateSocialInfo()
                {
                    FacebookPostToWallText = "PostToWallText1"
                }
            };

            orgTemplate.OrgBaseInfo.RegFields.Add(new RegField(){FieldId = 1, FieldValue = "r1"});

            Mapper.Map(broadcasterTemplateOverride.MyBaseInfo, orgTemplate.OrgBaseInfo);
            Mapper.Map(broadcasterTemplateOverride.MySocialInfo, orgTemplate.OrgSocialInfo);

            //MyExtensions.MergeWith(orgTemplate.OrgBaseInfo, broadcasterTemplateOverride.MyBaseInfo);

            //var mergeSettings = new JsonMergeSettings
            //{
            //    MergeArrayHandling = MergeArrayHandling.Merge
            //};

            //var json1 = JsonConvert.SerializeObject(myTemplate);
            //var o1 = JObject.Parse(json1);
            //(o1).Merge(JObject.Parse(marketJsonOverride), mergeSettings);
            //(o1).Merge(JObject.Parse(siteJsonOverride), mergeSettings);


            //MyMapper(myTemplate, broadcasterTemplateOverride);
            //MyMapper(myTemplate, marketTemplateOverride);
            //MyMapper(myTemplate, siteTemplateOverride);
        }

        //public static class MyExtensions
        //{
        //    public static void MergeWith(object instance, object source)
        //    {
        //        //don't do any processing if source is null, just return the instance
        //        if (source != null)
        //        {
        //            foreach (var property in instance.GetType().GetProperties())
        //            {
        //                //Get the values of the instance and source
        //                var instanceValue = property.GetValue(instance);
        //                var sourceValue = property.GetValue(source);

        //                if (instanceValue == null || instanceValue.GetType().IsValueType)
        //                {
        //                    property.SetValue(instance, new[] { sourceValue });
        //                }

        //            }

        //        }
        //    }
        //}

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

        public class OrgTemplate
        {
            public OrgTemplateBaseInfo OrgBaseInfo { get; set; }
            public OrgTemplateSocialInfo OrgSocialInfo { get; set; }          
        }

        [Serializable]
        public class OrgTemplateBaseInfo
        {
            public string ContestName { get; set; }
            public int ContestId { get; set; }
            public bool IsActive { get; set; }
            public List<RegField> RegFields { get; set; }
        }

        [Serializable]
        public class OrgTemplateSocialInfo
        {
            public string FacebookPostToWallText { get; set; }
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
            //public List<RegField> RegFields { get; set; }
        }

        [Serializable]
        public class TemplateSocialInfo
        {
            public string FacebookPostToWallText { get; set; }
        }

        [Serializable]
        public class RegField
        {
            public int FieldId { get; set; }
            public string FieldValue { get; set; }
        }
    }
}
