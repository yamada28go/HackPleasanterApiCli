using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.Library.Models.DB
{
    /// <summary>
    /// DB上のSiteテーブルのマッピング用構造体
    /// </summary>
    [Serializable]
    public class SiteModel //: BaseItemModel
    {
        public string Title = string.Empty;

        public long SiteId = 0;

        //public SiteSettings SiteSettings;
        public int TenantId = 0;
        public string GridGuide = string.Empty;
        public string EditorGuide = string.Empty;
        public string ReferenceType = "Sites";
        public long ParentId = 0;
        public long InheritPermission = 0;
        public bool Publish = false;
        // public Time LockedTime = new Time();
        // public User LockedUser = new User();
        // public SiteCollection Ancestors = null;
        public int SiteMenu = 0;
        public List<string> MonitorChangesColumns = null;
        public List<string> TitleColumns = null;
        //   public Export Export = null;
        //public DateTime ApiCountDate = 0.ToDateTime();
        public int ApiCount = 0;

        public string SiteSettings = string.Empty;
    }
}
