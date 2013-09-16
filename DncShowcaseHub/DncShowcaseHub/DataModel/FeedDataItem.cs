using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DncShowcaseHub.DataModel
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class FeedDataItem
    {
        public FeedDataItem(String uniqueId, String title, String subtitle, String contentPath, String description, String content)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ContentPath = contentPath;
            this.Content = content;
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }

        public string ContentPath { get; set; }
        public string Content { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

}
